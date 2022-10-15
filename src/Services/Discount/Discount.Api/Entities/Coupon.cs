namespace Discount.Api.Entities
{
    public class Coupon
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string Desription { get; set; }
        public double Amount { get; set; }
    }
}