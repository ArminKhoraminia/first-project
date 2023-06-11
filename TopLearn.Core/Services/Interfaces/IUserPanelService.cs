using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.Core.DTOs;

namespace TopLearn.Core.Services.Interfaces
{
    public interface IUserPanelService
    {
        DetailUsePanelViewModel GetDetailUserPanell(string username);
        SideBarUserPanelViewModel GetSideBarUserPanel(string username);
        EditeUserPanelViewModel GetEditeUserPanel(string username);
        bool EditeUserPanel(string oldusername, EditeUserPanelViewModel UserPanel);
    }
}
