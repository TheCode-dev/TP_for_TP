using Dapper;
using MySql.Data.MySqlClient;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
    public class ApplicationUserRepository : BaseRepository, IApplicationUserRepository
    {
        public ApplicationUserEntity Get(int userId)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QuerySingleOrDefault<ApplicationUserEntity>(@"
SELECT ID, FirstName, LastName, Gender, Birthday, CreateDate, LastLogin, Status, DiscontID, Bonus, Email, Password
FROM users
WHERE ID=@id;", new { id = userId });
                return ret;
            }
        }

        public ApplicationUserEntity GetByLoginAndPassword(string login, string password)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QuerySingleOrDefault<ApplicationUserEntity>(@"
SELECT ID, FirstName, LastName, Gender, Birthday, CreateDate, LastLogin, Status, DiscontID, Bonus, Email, Password
FROM users
WHERE Email=@email AND Password=@password;", new { email = login, password = password });
                return ret;
            }
        }
    }
}
