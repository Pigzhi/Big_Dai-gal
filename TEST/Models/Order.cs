using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proxy_shopping.Models;




public partial class Order
{

    public int OrderId { get; set; }

    public int? ProductId { get; set; }

    public string? BuyerId { get; set; }

    public string? SellerId { get; set; }

    public OrderStatus OrderStatus { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? SystemFee { get; set; }

    public string? ShippingAddress { get; set; }

    public DateTime? AcceptedAt { get; set; }

    public decimal EscrowAmount { get; set; }

    public virtual User? Buyer { get; set; }

    public virtual ICollection<ChatMessage> ChatMessages { get; set; } = new List<ChatMessage>();

    public virtual Product? Product { get; set; }

    public virtual User? Seller { get; set; }
}
public enum OrderStatus
{
    pending,
    Taken,
    Completed
};