namespace Todo.Models.TodoLists
{
    public class TodoListGravatarViewmodel
    {
        public string Email { get; set; }

        public TodoListGravatarViewmodel(string email)
        {
            Email = email;
        }
    }
}