using Dapper;
using MySql.Data.MySqlClient;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
    public class OrdersRepository : BaseRepository, IOrdersRepository
    {
        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
DELETE FROM kp_belyshev.orders
WHERE ID=@id;", new { id = id });
            }
        }

        public void Update(OrdersModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
UPDATE orders
SET `Date`=@date, ProductID=@product, UserID=@user, Quantity=@quantity, Price=@price, ManagerID=@manager, DiscountID=@discount, Bonus=@bonus, OrderStatus=@status
WHERE ID=@id;
;", new { id = model.ID, date = DateTime.Now, product = model.ProductID, user = model.UserID, quantity = model.Quantity, price = model.Price, manager = model.ManagerID, discount = model.DiscountID, bonus = model.Bonus, status = "Обновлён" });
            }
        }

        public void Create(OrdersModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
INSERT INTO orders
(`Date`, ProductID, UserID, Quantity, Price, ManagerID, DiscountID, Bonus, OrderStatus)
VALUES(@date, @product, @user, @quantity, @price, @manager, @discount, @bonus, @status);
;", new { id = model.ID, date = DateTime.Now, product = model.ProductID, user = model.UserID, quantity = model.Quantity, price = model.Price, manager = model.ManagerID, discount = model.DiscountID, bonus = model.Bonus, status = "Принят" });
            }
        }

        public OrdersModel Get(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QueryFirstOrDefault<OrdersModel>(@"
SELECT ID, `Date`, ProductID, UserID, Quantity, Price, ManagerID, DiscountID, Bonus, OrderStatus
FROM orders
WHERE ID=@id
;", new { id = id });
                return ret;
            }
        }

        public IList<OrdersModel> GetList()
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<OrdersModel>(@"
SELECT o.ID, o.`Date`, o.ProductID, p.NAME as 'ProductName', o.UserID, u.FirstName as 'UserFirstName', u.LastName as 'UserLastName', 
o.Quantity, o.Price, o.ManagerID, m.FirstName as 'ManagerFirstName', m.LastName as 'ManagerLastName', o.DiscountID, d.Percent as 'DiscountName', o.Bonus, o.OrderStatus
FROM orders o
left join products p
on o.ProductID = p.ID
left join users u
on o.UserID = u.ID
left join users m
on o.ManagerID = m.ID
left join discounts d
on o.DiscountID = d.ID
;");
                return ret.AsList();
            }
        }
        public IList<OrdersModel> GetHistory(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<OrdersModel>(@"
SELECT o.ID, o.`Date`, o.ProductID, p.NAME as 'ProductName', o.UserID, u.FirstName as 'UserFirstName', u.LastName as 'UserLastName', 
o.Quantity, o.Price, o.ManagerID, m.FirstName as 'ManagerFirstName', m.LastName as 'ManagerLastName', o.DiscountID, d.Percent as 'DiscountName', o.Bonus, o.OrderStatus
FROM orders o
left join products p
on o.ProductID = p.ID
left join users u
on o.UserID = u.ID
left join users m
on o.ManagerID = m.ID
left join discounts d
on o.DiscountID = d.ID
WHERE UserID=@id
;", new { id = id });
                return ret.AsList();
            }
        }
    }
}
