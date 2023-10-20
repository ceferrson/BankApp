using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BankApp.Web.Data.Contexts;
using BankApp.Web.Data.Entities;
using BankApp.Web.Interfaces;
using BankApp.Web.Models.Dto;
using BankApp.Web.UnitOfWork;

namespace BankApp.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        public AccountController(IUow uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var allAccounts = _mapper.Map<List<AccountDto>>(_uow.GetRepository<Account>().GetAll());
            return View(allAccounts);
        }

        public IActionResult Create(int id)
        { 
            var userInfo = _mapper.Map<UserDto>(_uow.GetRepository<User>().GetById(id));
            return View(userInfo);
        }

        [HttpPost]
        public IActionResult Create(AccountDto account)
        {
            //set 0 the account id because we don't use id when we add
            account.Id = 0;

            //Check that account already exists or not
            var existedAccount =_uow.GetRepository<Account>().GetAll().Where(a => a.AccountNumber == account.AccountNumber).FirstOrDefault();

            if(existedAccount != null)
            {
                ModelState.AddModelError("", "Account Already exists");
                return RedirectToAction();
            }

            var createdAccount = _mapper.Map<Account>(account);

            if (!_uow.GetRepository<Account>().Create(createdAccount))
            {
                ModelState.AddModelError("", "Something went wrong, Creating procces failed!");
                return View();
            }
            //Save Changes
            _uow.Save();
            return RedirectToAction("List", "User");
        }

        public IActionResult Remove(int accountId)
        {
            var account = _uow.GetRepository<Account>().GetById(accountId);
            if(!_uow.GetRepository<Account>().Remove(account))
            {
                ModelState.AddModelError("", "Something went wrong while removing account");
                return RedirectToAction("List", "User");
            }

            //Save Changes
            _uow.Save();
            return RedirectToAction("List", "User");
        }

        public IActionResult SendMoney(int accountId)
        {
            //sender id:
            ViewBag.SenderId = accountId;  
            var query = _uow.GetRepository<Account>().GetQueryable();
            var accounts = _mapper.Map<List<AccountDto>>(query.Where(a => a.Id !=  accountId).ToList());
            return View(new SelectList(accounts,"Id", "AccountNumber"));
        }

        [HttpPost]
        public IActionResult SendMoney(int senderId, int recieverId, decimal amount)
        {
            var senderAccount = _uow.GetRepository<Account>().GetById(senderId);

            var recieverAccount = _uow.GetRepository<Account>().GetById(recieverId);

            if (senderAccount.Balance >= amount)
                senderAccount.Balance = senderAccount.Balance - amount;
            else
            {
                ModelState.AddModelError("", "There is no enough balance to send this amount of money!");
                return RedirectToAction();
            }

            recieverAccount.Balance = recieverAccount.Balance + amount;

            if (!_uow.GetRepository<Account>().Update(senderAccount) && !_uow.GetRepository<Account>().Update(recieverAccount))
            {
                ModelState.AddModelError("", "Something went wrong in sending procces");
                return RedirectToAction();
            }

            //SaveChanges
            _uow.Save();
            return RedirectToAction("List", "User");
        }

    }
}
