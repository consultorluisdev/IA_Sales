using Microsoft.AspNetCore.Mvc;
using IASales.Api.Data;
using IASales.Api.Entities;

namespace IASales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
  private readonly AppDbContext _context;

  public ProductsController(AppDbContext context)
  {
    _context = context;
  }
  [HttpGet]
  public IActionResult GetAll()
  {
    return Ok(_context.Products.ToList());
  }
  [HttpPost]
  public IActionResult Create(Product product)
  {
    _context.Products.Add(product);
    _context.SaveChanges();

    return Ok(product);
  }
}