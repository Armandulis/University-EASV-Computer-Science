namespace ProductServiceApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(ProductApiContext context);
    }
}
