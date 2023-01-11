using Dapper;
using MySql.Data.MySqlClient;
using System;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
    public class ProductsRepository : BaseRepository, IProductsRepository
    {
        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
DELETE FROM kp_belyshev.products
WHERE ID=@id;", new { id = id });
            }
        }
           
        public void Update(ProductsModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
UPDATE products
SET Type=@type, Name=@name, Description=@description, Price=@price, Count=@count, DataEdit=@dEdit, VendorID=@vendor, CategoryID=@category, CountriesID=@countries
WHERE ID=@id;
;", new {
                    id = model.ID,
                    type = model.Type,
                    name = model.Name,
                    description = model.Description,
                    price = model.Price,
                    count = model.Count,
                    dEdit = DateTime.UtcNow,
                    vendor = model.VendorID,
                    category = model.CategoryID,
                    countries = model.CountriesID
                });
            }
        }

        public void Create(ProductsModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
INSERT INTO products
(`Type`, Name, Description, Price, Count, DataEdit, VendorID, CategoryID, CountriesID)
VALUES(@type, @name, @description, @price, @count, @dEdit, @vendor, @category, @countries);
;", new {
                    type = model.Type,
                    name = model.Name,
                    description = model.Description,
                    price = model.Price,
                    count = model.Count,
                    dEdit = DateTime.UtcNow,
                    vendor = model.VendorID,
                    category = model.CategoryID,
                    countries = model.CountriesID
                });
            }
        }

        public ProductsModel Get(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QueryFirstOrDefault<ProductsModel>(@"
SELECT ID, `Type`, Name, Description, Price, Count, DataEdit, VendorID, CategoryID, CountriesID
FROM products
WHERE ID=@id
;", new { id = id });
                return ret;
            }
        }

        public IList<ProductsModel> GetList()
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<ProductsModel>(@"
SELECT p.ID, p.Type, p.Name, p.Description, p.Price, p.Count, p.DataEdit, p.VendorID, v.NAME as 'VendorName', p.CategoryID, cat.NAME as 'CategoryName', p.CountriesID, c.NAME as 'CountriesName'
FROM products p
left join vendor v
on p.VendorID = v.ID
left join cat_product cat
on p.CategoryID = cat.ID
left join countries c
on p.CountriesID = c.ID
;");
                return ret.AsList();
            }
        }
    }
}
