using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IWalletService
    {
        int GetWalletPriceUserName(string username);
        List<GetAllWalletUserViewModel> GetAllWalletUser(string username);
        bool ChargeWalletUserName(string username, string description, int amount);
    }
}
