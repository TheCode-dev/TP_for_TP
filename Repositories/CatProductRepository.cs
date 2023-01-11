using Dapper;
using MySql.Data.MySqlClient;
using TP_KP_Belyshev.Interfaces;
using TP_KP_Belyshev.Models;

namespace TP_KP_Belyshev.Repositories
{
    public class CatProductRepository : BaseRepository, ICatProductRepository
    {
        public void Delete(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
DELETE FROM kp_belyshev.cat_product
WHERE ID=@id;", new { id = id });
            }
        }

        public void Update(CatProductModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
UPDATE cat_product
SET `Type`=@type, Name=@name, Description=@description
WHERE ID=@id;
;", new { id = model.ID, type = model.Type, name = model.Name, description = model.Description });
            }
        }

        public void Create(CatProductModel model)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                _ = connection.Execute(@"
INSERT INTO cat_product
(`Type`, Name, Description)
VALUES(@type, @name, @description);
;", new { type = model.Type, name = model.Name, description = model.Description });
            }
        }

        public CatProductModel Get(int id)
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.QueryFirstOrDefault<CatProductModel>(@"
SELECT ID, `Type`, Name, Description
FROM cat_product
WHERE ID=@id
;", new { id = id });
                return ret;
            }
        }

        public IList<CatProductModel> GetList()
        {
            using (var connection = new MySqlConnection(GetConnectionString))
            {
                var ret = connection.Query<CatProductModel>("SELECT ID, `Type`, Name, Description\r\nFROM cat_product;\r\n");
                return ret.AsList();
            }
        }
    }
}
