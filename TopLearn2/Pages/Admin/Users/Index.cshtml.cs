using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.Services.Interfaces;
using TopLearn.Core.DTOs;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace TopLearn2.Web.Pages.Admin.Users
{
    public class IndexModel : PageModel
    {
        #region Create Context

        private IAdminService _dbAdminContext;
        public IndexModel(IAdminService dbAdminContext)
        {
            _dbAdminContext = dbAdminContext;
        }

        #endregion

        public GetAllUserForAdminViewModel GetAllUserForAdmin { get; set; }

        public void OnGet(int PageId = 1, string filterEmail = "", string filterUserName = "")
        {
            GetAllUserForAdmin = _dbAdminContext.GetAllUser(PageId, filterEmail, filterUserName);
        }
    }
}
