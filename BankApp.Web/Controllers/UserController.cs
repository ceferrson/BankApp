using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BankApp.Web.Data.Contexts;
using BankApp.Web.Data.Entities;
using BankApp.Web.Interfaces;
using BankApp.Web.Models.Dto;
using BankApp.Web.UnitOfWork;

namespace BankApp.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUow _uow;

        public UserController(IUow uow, IMapper mapper) 
        {
            _mapper = mapper;
           _uow = uow;
        }
        public IActionResult List()
        { 
            var users = _mapper.Map<List<UserDto>>(_uow.GetRepository<User>().GetAll());
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = _mapper.Map<UserDto>(_uow.GetRepository<User>().GetQueryable().Include(u => u.Accounts).FirstOrDefault(u => u.Id == id));

            return View(user);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(UserDto user)
        {

            var existedUser = _uow.GetRepository<User>().GetAll().Where(u => u.Name == user.Name && u.Surname == user.Surname).FirstOrDefault();

            if(existedUser != null)
            {
                ModelState.AddModelError("", "User already exist!");
                return View();
            }

            User createdUser = _mapper.Map<User>(user);
            
            if(!_uow.GetRepository<User>().Create(createdUser))
            {
                ModelState.AddModelError("", "Something went wrong while creating user!");
                return View();
            }

            _uow.Save();
            return RedirectToAction("List");
        }

        public IActionResult Remove(int userId)
        {
            var user = _uow.GetRepository<User>().GetById(userId);
            
            if(!_uow.GetRepository<User>().Remove(user))
            {
                ModelState.AddModelError("", "Something went wrong while removing user!");
                return View();
            }

            _uow.Save();
            return RedirectToAction("List"); 
        }
    }
}
