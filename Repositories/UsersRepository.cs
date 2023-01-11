using Dapper;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Utilities;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
DELETE FROM kp_belyshev.users
WHERE ID=@id;", new { id = id });
            }
        }

        public void Update(UsersModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
UPDATE users
SET FirstName=@fName, LastName=@lName, Gender=@gender, Birthday=@bDay, CreateData=@createData, LastLogin=@lastLogin, Status=@status, DiscontID=@discount, Bonus=@bonus, Email=@email, Password=@password
WHERE ID=@id;
;", new {
                    id = model.ID,
                    fName = model.FirstName,
                    lName = model.LastName,
                    gender = model.Gender,
                    bDay = model.Birthday,
                    createData = model.CreateDate,
                    lastLogin = model.LastLogin,
                    status = model.Status,
                    discount = model.DiscontID,
                    bonus = model.Bonus,
                    email = model.Email,
                    password = model.Password
                });
            }
        }

        public void Create(UsersModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
INSERT INTO users
(FirstName, LastName, Gender, Birthday, CreateDate, LastLogin, `Status`, DiscontID, Bonus, `Email`, `Password`)
VALUES(@fName, @lName, @gender, @bDay, @createData, @lastLogin, @status, @discount, @bonus, @email, @password);
;", new {
                    fName = model.FirstName,
                    lName = model.LastName,
                    gender = model.Gender,
                    bDay = model.Birthday,
                    createData = model.CreateDate,
                    lastLogin = model.LastLogin,
                    status = model.Status,
                    discount = model.DiscontID,
                    bonus = model.Bonus,
                    email = model.Email,
                    password = model.Password
                });
            }
        }

        public UsersModel Get(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QueryFirstOrDefault<UsersModel>(@"
SELECT ID, FirstName, LastName, Gender, Birthday, CreateDate, LastLogin, `Status`, DiscontID, Bonus, `Email`, `Password`, IsManager
FROM users
WHERE ID=@id
;", new { id = id });
                return ret;
            }
        }
        public IList<UsersModel> GetManagers()
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<UsersModel>(@"
SELECT ID, FirstName, LastName, IsManager
FROM users
WHERE IsManager=1
;");
                return ret.AsList();
            }
        }

        public IList<UsersModel> GetList()
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<UsersModel>(@"
SELECT u.ID, u.FirstName, u.LastName, u.Gender, u.Birthday, u.CreateDate, u.LastLogin, u.`Status`, u.DiscontID, d.Percent as 'DiscontPercent', u.Bonus, u.`Email`, u.`Password`, u.IsManager
FROM users u
left join discounts d
on u.DiscontID = d.ID
;");
                return ret.AsList();
            }
        }
    }
}
