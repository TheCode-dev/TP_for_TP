using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
	public interface IApplicationUserRepository
	{
		ApplicationUserEntity GetByLoginAndPassword(string login, string password);
		ApplicationUserEntity Get(int userId);

    }
}
