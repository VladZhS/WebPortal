namespace WebPortalServer.Model.WebEnities
{
    public abstract class BaseModel<T>
    {
        public BaseModel(T entity)
        {
            
        }

        public BaseModel() { }

        public abstract T ToEntity(T entity);
    }
}
