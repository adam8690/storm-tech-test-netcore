using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.Data.Entities;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; set; }
    DbSet<TodoItem> TodoItems { get; set; }
    Task<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default) where TEntity : class;
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}