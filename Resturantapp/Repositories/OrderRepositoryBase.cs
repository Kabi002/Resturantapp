using WebAppRestaurantApp.ViewModel;

namespace WebAppRestaurantApp.Repositories
{
    public abstract class OrderRepositoryBase
    {
        public abstract bool AddOrder(OrderViewModel objOrderViewModel);
    }
}