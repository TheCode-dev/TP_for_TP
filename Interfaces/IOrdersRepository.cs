using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
    public interface IOrdersRepository
    {
        void Update(OrdersModel model);
        void Create(OrdersModel model);
        OrdersModel Get(int id);
        IList<OrdersModel> GetList();
        IList<OrdersModel> GetHistory(int id);
        void Delete(int id);
    }
}
