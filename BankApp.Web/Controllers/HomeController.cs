using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BankApp.Web.Data.Contexts;
using BankApp.Web.Data.Entities;
using BankApp.Web.Models.Dto;

namespace BankApp.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
