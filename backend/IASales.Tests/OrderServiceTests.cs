using IASales.Api.DTOs;
using IASales.Api.Entities;

namespace IASales.Tests;

public class OrderServiceTests
{
    [Fact]
    public void CreateOrderDTO_ShouldHaveCorrectProperties()
    {
        var items = new List<OrderItem>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Camisa", Quatity = 2, UnitPrice = 50.00m }
        };

        var dto = new CreateOrderDTO(
            CustomerId: null,
            Items: items,
            Channel: "whatsapp",
            Discount: 10.00m,
            Shipping: 15.00m
        );

        Assert.Single(dto.Items);
        Assert.Equal(50.00m, dto.Items[0].UnitPrice);
        Assert.Equal(2, dto.Items[0].Quatity);
        Assert.Equal(10.00m, dto.Discount);
        Assert.Equal(15.00m, dto.Shipping);
    }

    [Fact]
    public void OrderItem_ShouldCalculateSubtotal()
    {
        var item = new OrderItem
        {
            ProductId = Guid.NewGuid(),
            ProductName = "Camisa",
            Quatity = 3,
            UnitPrice = 50.00m
        };

        var subtotal = item.UnitPrice * item.Quatity;

        Assert.Equal(150.00m, subtotal);
    }

    [Fact]
    public void Order_ShouldCalculateTotalCorrectly()
    {
        var items = new List<OrderItem>
        {
            new() { ProductId = Guid.NewGuid(), ProductName = "Camisa", Quatity = 2, UnitPrice = 50.00m },
            new() { ProductId = Guid.NewGuid(), ProductName = "Calça", Quatity = 1, UnitPrice = 80.00m }
        };

        var subtotal = items.Sum(i => i.UnitPrice * i.Quatity);
        var discount = 10.00m;
        var shipping = 15.00m;
        var total = subtotal - discount + shipping;

        Assert.Equal(180.00m, subtotal);
        Assert.Equal(10.00m, discount);
        Assert.Equal(15.00m, shipping);
        Assert.Equal(185.00m, total);
    }

    [Fact]
    public void Order_DefaultValues_ShouldBeCorrect()
    {
        var order = new Order();

        Assert.NotEqual(default, order.Id);
        Assert.Equal("pending", order.Status);
        Assert.Equal("site", order.Channel);
        Assert.Equal(0, order.Subtotal);
        Assert.Equal(0, order.Total);
        Assert.Equal(0, order.Discount);
        Assert.NotNull(order.Items);
    }

    [Fact]
    public void UpdateStatusDTO_ShouldContainNewStatus()
    {
        var dto = new UpdateStatusDTO("completed");

        Assert.Equal("completed", dto.Status);
    }
}
