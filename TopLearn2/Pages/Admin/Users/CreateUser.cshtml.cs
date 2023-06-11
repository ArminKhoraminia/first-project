using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TopLearn.Core.DTOs;
using TopLearn.Core.Services.Interfaces;

namespace TopLearn2.Web.Pages.Admin.Users
{
    public class CreateUserModel : PageModel
    {
        private IUserService _dbUserContext;
        private IAdminService _dbAdminContext;
        public CreateUserModel(IUserService dbUserContext, IAdminService dbAdminContext)
        {
            _dbUserContext = dbUserContext;
            _dbAdminContext = dbAdminContext;
        }

        [BindProperty]
        public AddUserForAdminViewModel AddUserForAdmin { get; set; }


        public void OnGet()
        {
            ViewData["Roles"] = _dbUserContext.GetAllRole();
        }

        public IActionResult OnPost(List<int> SelectedRoles)
        {
            if (!ModelState.IsValid)
            {
                ViewData["Roles"] = _dbUserContext.GetAllRole();
                return Page();
            }
            int tmpUserId;
            tmpUserId = _dbUserContext.AddUserFromADmin(AddUserForAdmin);
            _dbUserContext.AddRolesForUser(SelectedRoles, tmpUserId);
            return Redirect("/Admin/Users");
        }
    }
}
