using BackEnd_Intuit.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _context;

    public TestController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet("ping-db")]
    public IActionResult Ping()
    {
        return Ok(_context.Clientes.Count());
    }
}