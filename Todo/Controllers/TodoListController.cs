using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Todo.Data;
using Todo.Data.Entities;
using Todo.EntityModelMappers.TodoLists;
using Todo.Models.TodoLists;
using Todo.Services;
using System.Linq;

namespace Todo.Controllers
{
    [Authorize]
    public class TodoListController : Controller
    {
        private readonly IApplicationDbContext dbContext;
        private readonly IUserStore<IdentityUser> userStore;

        public TodoListController(IApplicationDbContext dbContext, IUserStore<IdentityUser> userStore)
        {
            this.dbContext = dbContext;
            this.userStore = userStore;
        }

        public IActionResult Index()
        {
            var userId = User.Id();
            var todoLists = dbContext.RelevantTodoLists(userId);
            var viewmodel = TodoListIndexViewmodelFactory.Create(todoLists);
            return View(viewmodel);
        }

        public IActionResult Detail(int todoListId, bool hideDoneItems, string sortOrder)
        {
            var todoList = dbContext.SingleTodoList(todoListId);

            switch (sortOrder)
            {
                case "rank_asc":
                    todoList.Items = todoList.Items.OrderBy(item => item.Rank).ToList();
                    break;
                default:
                    todoList.Items = todoList.Items.OrderBy(item => item.Importance).ToList();
                    break;
            }
            
            if (hideDoneItems)
            {
                todoList.Items = todoList.Items.Where(item => !item.IsDone).ToList();
            }

            var viewmodel = TodoListDetailViewmodelFactory.Create(todoList);
            return View(viewmodel);
        }

        public IActionResult GravatarData(string email)
        {
            return PartialView(new TodoListGravatarViewmodel(email));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new TodoListFields());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TodoListFields fields)
        {
            if (!ModelState.IsValid) { return View(fields); }

            var currentUser = await userStore.FindByIdAsync(User.Id(), CancellationToken.None);

            var todoList = new TodoList(currentUser, fields.Title);

            await dbContext.AddAsync(todoList);
            await dbContext.SaveChangesAsync();

            return RedirectToAction("Create", "TodoItem", new {todoList.TodoListId});
        }
    }
}