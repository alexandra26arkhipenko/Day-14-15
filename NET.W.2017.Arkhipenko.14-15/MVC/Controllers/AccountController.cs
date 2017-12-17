using System;
using System.Web.Mvc;
using BusinessLogic.Interfaces.Interfaces;
using DepandencyResolver;
using MVC.Models;
using Ninject;

namespace MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IEmailService _mailService;

        public AccountController()
        {
            var ninjectKernel = new StandardKernel();
            ninjectKernel.ConfigurateResolver();

            _accountService = ninjectKernel.Get<IAccountService>();
           _mailService = ninjectKernel.Get<IEmailService>();

            
        }

        #region Create account

        [HttpGet]
        public ActionResult CreateAccount() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult CreateAccount(ViewAccountModel account)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var createAccount = _accountService.CreateAccount(account.Type, account.FirstName, account.SecondName, account.Amount, account.Email);
            string createdAccountId = createAccount.Id;
            string subject = "Your account!";
            string message = $"Your account was created!<br />Your account ID: {createdAccountId}";
            SendMail(account.Email, subject, message);

            Session["accountCreated"] = true;
            return RedirectToAction("AccountIsCreated");
        }

        [HttpGet]
        public ActionResult AccountIsCreated()
        {
            if (!(Session["accountIsCreated"] is bool accountCreated) || !accountCreated)
            {
                return RedirectToAction("CreateAccount");
            }

            Session["accountIsCreated"] = false;
            return View();
        }

        #endregion 

        #region account operations

        [HttpGet]
        public ActionResult AccountOperations()
        {
            ViewBag.Error = false;
            ViewBag.IsAccountStatus = false;
            return View();
        }
        
        [HttpPost]
        public ActionResult DivMoney()
        {
            ViewBag.Error = false;
            ViewBag.Operation = nameof(AddMoney);
            return PartialView("_AddDivMoney");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult AddMoney(AccountOperationModel data)
        {
            var result = AddDivOperation(data, (s, d) => s.DivMoney(d.AccountId, d.Amount), out var isError);

            if (isError)
            {
                return result;
            }

            string accountOwnerEmail = GetAccountEmail(data.AccountId);
            string subject = "Account operation";
            string message = $"Amount={data.Amount} was paid to account id={data.AccountId}";
            SendMail(accountOwnerEmail, subject, message);

            return result;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult DivMoney(AccountOperationModel data)
        {
            var result = AddDivOperation(data, (s, d) => s.DivMoney(d.AccountId, d.Amount), out var isError);

            if (isError)
            {
                return result;
            }

            string accountOwnerEmail = GetAccountEmail(data.AccountId);
            string subject = "Account operation";
            string message = $"From account with id={data.AccountId} amount={data.Amount} were withdrawn.";
            SendMail(accountOwnerEmail, subject, message);

            return result;
        }

      
        [HttpPost]
        public ActionResult GetAccountStatus()
        {
            ViewBag.Operation = nameof(GetAccountStatus);
            ViewBag.Error = false;
            return PartialView("_GetStatusClose");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult GetAccountStatus(AccountIdModel accountId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("_GetStatusClose");
            }

            try
            {
                var accountInfo = _accountService.GetAccount(accountId.AccountId);
                ViewBag.Error = false;
                ViewBag.IsAccountStatus = true;
                return View("AccountOperations", GetAccountStatus(accountInfo));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountId), e.Message);
                ViewBag.Error = true;
                return View("_GetStatusClose");
            }
        }
        
        [HttpPost]
        public ActionResult TransferAmount()
        {
            ViewBag.Error = false;
            return PartialView("_TransferAmount");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult TransferAmount(TransferAmount transferData)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("_TransferAmount");
            }

            try
            {
                Transfer(transferData);

                string accountOwnerEmail = GetAccountEmail(transferData.FromAccountId);
                string subject = "Account operation";
                string message = $"From account with id={transferData.FromAccountId} sum={transferData.Amount} were withdrawn.";
                SendMail(accountOwnerEmail, subject, message);

                accountOwnerEmail = GetAccountEmail(transferData.ToAccountId);
                subject = "Account operation";
                message = $"Amount={transferData.Amount} was paid to account id={transferData.ToAccountId}";
                SendMail(accountOwnerEmail, subject, message);

                ViewBag.Error = false;
                return RedirectToAction("AccountOperations");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                ViewBag.Error = true;
                return View("_TransferAmount");
            }
        }
        
        [HttpPost]
        public ActionResult CloseAccount()
        {
            ViewBag.Operation = nameof(CloseAccountOperation);
            ViewBag.Error = false;
            return PartialView("_GetStatusClose");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandleError(ExceptionType = typeof(Exception))]
        public ActionResult CloseAccountOperation(AccountIdModel accountId)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                return View("_GetStatusClose");
            }

            try
            {
                var result = RedirectToAction("AccountOperations");

                string accountOwnerEmail = GetAccountEmail(accountId.AccountId);
                string subject = "Account closed";
                string message = $"account with id={accountId.AccountId} is closed.";

                _accountService.CloseAccout(accountId.AccountId);

                SendMail(accountOwnerEmail, subject, message);

                ViewBag.Error = false;
                return result;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(accountId.AccountId), e.Message);
                ViewBag.Error = true;
                return View("_GetStatusClose");
            }
        }

        #endregion 

        #region private

        private static AccountStatusModel GetAccountStatus(string data)
        {
            var accountInfo = data.Split(' ');
            int i = 2;
            return new AccountStatusModel
            {
                Type = accountInfo[0],
                AccountId = accountInfo[i++],
                FirstName = accountInfo[i++],
                SecondName = accountInfo[i++],
                Amount = accountInfo[i++],
                BonusPoints = accountInfo[i++],
                Email = accountInfo[i]
            };
        }

        private void SendMail(string to, string subject, string message)
        {
            var mailData = new MailData
            {
                To = to,
                From = "IAmYourBank@gmail.com",
                FromPassword = "your_email_password",
                Subject = subject,
                Message = message
            };
            _mailService.SendMail(mailData);
        }

        private ActionResult AddDivOperation(AccountOperationModel data, Action<IAccountService, AccountOperationModel> operation,
            out bool isError)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = true;
                isError = true;
                return View("_AddDivMoney");
            }

            try
            {
                operation(_accountService, data);
                ViewBag.Error = false;
            }
            catch (Exception e)
            {
                ModelState.AddModelError(nameof(data.AccountId), e.Message);
                ViewBag.Error = true;
                isError = true;
                return View("_AddDivMoney");
            }

            isError = false;
            return RedirectToAction("AccountOperations");
        }

        private string GetAccountEmail(string accountId)
        {
            string accountInfo = _accountService.GetAccount(accountId);
            var data = accountInfo.Split(' ');
            return data[data.Length - 1];
        }

        private void Transfer(TransferAmount transferData)
        {
            var fromAccountData = GetAccountStatus(_accountService.GetAccount(transferData.FromAccountId));
            
            if (decimal.Parse(fromAccountData.Amount) < transferData.Amount)
            {
                throw new InvalidOperationException($"There are not enough funds on your account id {transferData.FromAccountId}");
            }

            _accountService.DivMoney(transferData.FromAccountId, transferData.Amount);
            _accountService.AddMoney(transferData.ToAccountId, transferData.Amount);
        }

        #endregion // 
    }
}