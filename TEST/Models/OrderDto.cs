namespace Proxy_shopping.Models
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string? Img { get; set; }
        public string? ProductType { get; set; }
        public decimal Price { get; set; }
        public string? Category { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? Title { get; set; }              // 產品名稱
        public string? Location { get; set; }
        public string? ShoppingAddress { get; set; }
    }
}