using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;
using TopLearn.DataLayer.Context;
using TopLearn.DataLayer.Entities.User;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services
{

    public class WalletService : IWalletService
    {
        #region Context

        TopLearnContext _dbcontext;
        public WalletService(TopLearnContext context)
        {
            _dbcontext = context;
        }

        #endregion

        #region Get Information

        public int GetWalletPriceUserName(string username)
        {
            int userid = new UserService(_dbcontext).GetIdByUserName(username);
            var Recive = _dbcontext.Wallet
                .Where(w => w.UserId == userid && w.IsPayed == true && w.TypeId == 1)
                .Select(w => w.Amount).ToList();
            var payed = _dbcontext.Wallet
                .Where(w => w.UserId == userid && w.IsPayed == true && w.TypeId == 2)
                .Select(w => w.Amount).ToList();
            return (Recive.Sum() - payed.Sum());
        }

        public List<GetAllWalletUserViewModel> GetAllWalletUser(string username)
        {
            int userid = new UserService(_dbcontext).GetIdByUserName(username);
            return _dbcontext.Wallet.Where(w => w.UserId == userid && w.IsPayed == true)
                .Select(w => new GetAllWalletUserViewModel
                { 
                    TypeId = w.TypeId,
                    Amount= w.Amount ,
                    Description = w.Description ,
                    IsPayed = w.IsPayed ,
                    DateTime = w.DateTime
                }).ToList();
        }

        #endregion

        #region Add Wallet

        public bool ChargeWalletUserName(string username,string description,int amount)
        {
            int userid = new UserService(_dbcontext).GetIdByUserName(username);
            Wallet wallet = new Wallet()
            {
                TypeId = 1,
                UserId= userid,
                Amount= amount,
                Description= description ,
                IsPayed=true,
                DateTime = DateTime.Now,
            };
            _dbcontext.Wallet.Add(wallet);
            _dbcontext.SaveChanges();
            return true;
        }

        #endregion
    }
}
