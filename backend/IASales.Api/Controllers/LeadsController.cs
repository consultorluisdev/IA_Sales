using Microsoft.AspNetCore.Mvc;
using IASales.Api.Data;
using IASales.Api.Entities;

namespace IASales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LeadsController : ControllerBase
{
  private readonly AppDbContext _context;

  public LeadsController(AppDbContext context)
  {
    _context = context;
  }

  [HttpGet]
  public IActionResult GetAll()
  {
    return Ok(_context.Leads.ToList());
  }

  [HttpPost]
  public IActionResult Create(Lead lead)
  {
    _context.Leads.Add(lead);
    _context.SaveChanges();

    return Ok(lead);
  }
}