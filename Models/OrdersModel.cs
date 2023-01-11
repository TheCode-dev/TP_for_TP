using Microsoft.AspNetCore.Mvc.Rendering;

namespace TP_KP_Belyshev.Models
{
    public class OrdersModel
    {
        public IEnumerable<SelectListItem> ProductsModel { get; set; }
        public IEnumerable<SelectListItem> UsersModel { get; set; }
        public IEnumerable<SelectListItem> ManagerModel { get; set; }

        public int ID { get; set; }
        public string Date { get; set; }
        public string ProductID { get; set; }
        public string UserID { get; set; }
        public string Quantity { get; set; }
        public string Price { get; set; }
        public string ManagerID { get; set; }
        public string DiscountID { get; set; }
        public string Bonus { get; set; }
        public string OrderStatus { get; set; }
        public string ProductName { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerLastName { get; set; }
        public string DiscountName { get; set; }
    }
}
