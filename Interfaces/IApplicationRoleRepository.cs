using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Interfaces
{
	public interface IApplicationRoleRepository
	{
		IList<ApplicationRoleEntity> GetUserRolesByUserId(int userId);
	}
}
