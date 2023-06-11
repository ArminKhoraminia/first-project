using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Areas.UserPanel.Controllers
{
    [Area("UserPanel")]
    [Authorize]
    public class WalletController : Controller
    {
        
        #region Create Context
        private IWalletService _dbwalletcontext;
        public WalletController(IWalletService dbwalletcontext)
        {
            _dbwalletcontext = dbwalletcontext;
        }
        #endregion

        #region Show Wallet

        [Route("UserPanel/Wallet")]
        public IActionResult Wallet()
        {
            var userName = User.Identity.Name;
            ViewBag.AllWallet = _dbwalletcontext.GetAllWalletUser(userName);
            ViewBag.currentAmount = _dbwalletcontext.GetWalletPriceUserName(userName);
            return View();
        }

        [Route("UserPanel/WalletSucces")]
        public IActionResult WalletSucces()
        {
            return View();
        }

        #endregion

        #region Charge Wallet

        [HttpPost]
        [Route("UserPanel/Wallet")]
        public IActionResult Wallet(GetWalletDataViewModel wallet)
        {
            var userName = User.Identity.Name;
            if (!ModelState.IsValid)
            {
                ViewBag.AllWallet = _dbwalletcontext.GetAllWalletUser(userName);
                ViewBag.currentAmount = _dbwalletcontext.GetWalletPriceUserName(userName);
                return View(wallet);
            }
            if (wallet.Amount <= 0) 
            {
                ViewBag.AllWallet = _dbwalletcontext.GetAllWalletUser(userName);
                ViewBag.currentAmount = _dbwalletcontext.GetWalletPriceUserName(userName);
                ModelState.AddModelError("Amount","مبلغ وارد شده باید بزرگتر از صفر تومان باشد");
                return View(wallet);
            }

            bool isSucces = _dbwalletcontext.ChargeWalletUserName(userName, "شارژ کیف پول", wallet.Amount);

            if (isSucces)
            {
                return Redirect("/UserPanel/WalletSucces?Amount=" + wallet.Amount);
            }
            else
            {
                ViewBag.AllWallet = _dbwalletcontext.GetAllWalletUser(userName);
                ViewBag.currentAmount = _dbwalletcontext.GetWalletPriceUserName(userName);
                return View(wallet);
            }

        }

        #endregion
    }
}
