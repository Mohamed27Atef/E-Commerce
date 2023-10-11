namespace E_Commerce.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        T getById(int id);
      
        List<T> getAll(string include = "");

        int add(T entity);

        int update(T entity);

        int delete(int id);
    }
}
