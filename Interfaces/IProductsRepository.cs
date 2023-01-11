using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
    public interface IProductsRepository
    {
        void Update(ProductsModel model);
        void Create(ProductsModel model);
        ProductsModel Get(int id);
        IList<ProductsModel> GetList();
        void Delete(int id);
    }
}
