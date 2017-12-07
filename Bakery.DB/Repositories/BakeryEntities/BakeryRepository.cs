using Bakery.DB.Interfaces;

namespace Bakery.DB.Repositories
{
    public static class BakeryRepository
    {
        public static ICakeImageRepository GetCakeImageRepository()
        {
            return new CakeImageRepository();
        }

        public static ICakeRepository GetCakeRepository()
        {
            return new CakeRepository();
        }

        public static IOrderSupplementRepository GetCakeSupplementRepository()
        {
            return new OrderSupplementRepository();
        }

        public static ICustomerRepository GetCustomerRepository()
        {
            return new CustomerRepository();
        }

        public static IImageRepository GetImageRepository()
        {
            return new ImageRepository();
        }

        public static IOrderRepository GetOrderRepository()
        {
            return new OrderRepository();
        }

        public static IOrderTypeRepository GetOrderTypeRepository()
        {
            return new OrderTypeRepository();
        }

        public static IRoleTypeRepository GetRoleTypeRepository()
        {
            return new RoleTypeRepository();
        }

        public static ISupplementRepository GetSupplementRepository()
        {
            return new SupplementRepository();
        }
    }
}
