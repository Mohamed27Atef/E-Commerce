namespace E_Commerce.Repository
{
    public interface IGenericRepository<T> where T : class
    {

        T getById(int id);
      
        List<T> getAll(string include = "");

        void add(T entity);

        void update(T entity);

        void delete(int id);

        public int SaveChanges();

    }
}
