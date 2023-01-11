using Dapper;
using MySql.Data.MySqlClient;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
	public class ApplicationRoleRepository : BaseRepository, IApplicationRoleRepository
	{
		public IList<ApplicationRoleEntity> GetUserRolesByUserId(int userId)
		{
			using (var connection = new MySqlConnection(GetConnectionString))
			{
				var ret = connection.Query<ApplicationRoleEntity>(@"SELECT g.ID, g.Name, g.Role
FROM `groups` g
INNER JOIN `user_group` ug ON g.ID = ug.GROUPID 
where ug.USERID = @userId;", new
				{
                    userId = userId
                });
				return ret.AsList();
			}
		}
	}
}
