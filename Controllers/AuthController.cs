using AspnetCoreMvcFull.Data;
using AspnetCoreMvcFull.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AspnetCoreMvcFull.Controllers;

public class AuthController : Controller
{
  private readonly AppDbContext _context;
  public AuthController(AppDbContext context)
  {
    _context = context;
  }
  public IActionResult ForgotPasswordBasic() => View();
  public IActionResult LoginBasic() => View();
  public IActionResult RegisterBasic() => View();

  [HttpPost]
  public IActionResult RegisterBasic(RegisterModel model)
  {
    var user = new User
    {
      Username = model.Username,
      Email = model.Email,
      PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password)
    };
    _context.Users.Add(user);
    _context.SaveChanges();

    return RedirectToAction("LoginBasic");
  }
}
