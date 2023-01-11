using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
    public interface IUsersRepository
    {
        void Update(UsersModel model);
        void Create(UsersModel model);
        UsersModel Get(int id);
        IList<UsersModel> GetList();
        IList<UsersModel> GetManagers();
        void Delete(int id);
    }
}
