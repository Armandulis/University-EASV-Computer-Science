namespace CustomerAPI.Data
{
    public interface IDbInitializer
    {
        void Initializer(CustomerAPIContext context);
    }
}
