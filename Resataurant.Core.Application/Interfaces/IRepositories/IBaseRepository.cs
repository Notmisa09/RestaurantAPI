namespace Restaurant.Core.Application.Interfaces.IRepositories
{
    public interface IBaseRepository <T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>>GetAllAsync();
        Task<T> GetByIdAsync(int Id);
        Task RemoveAsync(T entity);
        Task<List<T>> GetAllWithInclude(List<string> properties);
        Task UpdateAsync(T entity, int Id);
    }
}
