namespace Services
{
    public interface IService<T>
    {
        IQueryable<T> GetAll();
        Task<T?> GetOneAsync(int id);
        Task<bool> AddAsync(T entity);
        Task<bool> EditAsync(T entity);
        Task<bool> RemoveAsync(T entity);
        bool DoesExist(int id);
    }
}
