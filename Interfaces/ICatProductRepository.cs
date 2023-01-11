using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
    public interface ICatProductRepository
    {
        void Update(CatProductModel model);
        void Create(CatProductModel model);
        CatProductModel Get(int id);
        IList<CatProductModel> GetList();
        void Delete(int id);
    }
}
