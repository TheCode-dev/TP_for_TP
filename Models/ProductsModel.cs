namespace TP_KP_Belyshev.Models
{
    public class ProductsModel
    {
        public int ID { get; set; }
        public string? Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public string Count { get; set; }
        public string DataEdit { get; set; }
        public string VendorID { get; set; }
        public string CategoryID { get; set; }
        public string? CountriesID { get; set; }
        public string? VendorName { get; set; }
        public string? CategoryName { get; set; }
        public string? CountriesName { get; set; }
    }
}
