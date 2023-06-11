using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Permission;

namespace TopLearn.DataLayer.Entities.User
{
    public class Role
    {
        public Role()
        {

        }

        [Key]
        public int RoleId { get; set; }

        [Display(Name = "شرح نقش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string RoleTitle { get; set; }
        public bool IsDelete { get; set; }

        #region Relation
        public virtual List<UserRole> UserRole { get; set; }
        public virtual List<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
