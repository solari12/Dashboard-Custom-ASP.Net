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
    if (!ModelState.IsValid)
    {
      return View(model);
    }

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

  [HttpPost]
  public IActionResult LoginBasic(LoginModel model)
  {
    if (!ModelState.IsValid)
    {
      return View(model);
    }

    var user = _context.Users
        .FirstOrDefault(u => u.Username == model.Username || u.Email == model.Username);

    if (user == null)
    {
      ModelState.AddModelError("", "Sai username hoặc password");
      return View(model);
    }

    bool isValid = BCrypt.Net.BCrypt.Verify(model.Password, user.PasswordHash);

    if (!isValid)
    {
      ModelState.AddModelError("", "Sai username hoặc password");
      return View(model);
    }

    HttpContext.Session.SetString("Username", user.Username);

    return RedirectToAction("Index", "Home");
  }

  [HttpPost]
  public IActionResult Logout() {
    HttpContext.Session.Clear();
    return RedirectToAction("LoginBasic");
  }
}
