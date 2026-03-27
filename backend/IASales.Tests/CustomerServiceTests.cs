using Moq;
using Microsoft.EntityFrameworkCore;
using IASales.Api.Data;
using IASales.Api.Services;
using IASales.Api.DTOs;
using IASales.Api.Entities;

namespace IASales.Tests;

public class CustomerServiceTests
{
    [Fact]
    public void CreateCustomerDTO_ShouldHaveCorrectProperties()
    {
        var dto = new CreateCustomerDTO(
            "João Silva",
            "joao@email.com",
            "11999999999",
            null,
            "website",
            new List<string> { "roupas" },
            "Cliente VIP"
        );

        Assert.Equal("João Silva", dto.Name);
        Assert.Equal("joao@email.com", dto.Email);
        Assert.Equal("11999999999", dto.Phone);
        Assert.Equal("website", dto.Source);
        Assert.Contains("roupas", dto.Interests);
    }

    [Fact]
    public void UpdateCustomerDTO_ShouldAllowPartialUpdate()
    {
        var dto = new UpdateCustomerDTO(
            Name: "Novo Nome",
            Email: null,
            Phone: null,
            Notes: null
        );

        Assert.Equal("Novo Nome", dto.Name);
        Assert.Null(dto.Email);
    }

    [Fact]
    public void Customer_DefaultValues_ShouldBeCorrect()
    {
        var customer = new Customer();

        Assert.NotEqual(default, customer.Id);
        Assert.Equal("manual", customer.Source);
        Assert.Equal(0, customer.TotalSpent);
        Assert.Equal(0, customer.OrderCount);
        Assert.NotNull(customer.Interests);
    }
}
