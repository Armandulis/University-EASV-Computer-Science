namespace OrderServiceApi.Data
{
    public interface IDbInitializer
    {
        void Initialize(OrderApiContext context);
    }
}
