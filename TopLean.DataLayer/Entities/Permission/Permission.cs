using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.DataLayer.Entities.Permission
{
    public class Permission
    {
        public Permission()
        {
            
        }

        [Key]
        public int PermissionId { get; set; }

        [Display(Name = "شرح دسترسی")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string PermissionTitle { get; set; } 

        public int? ParentId { get; set; }

        #region Relations

        [ForeignKey("ParentId")]
        public List<Permission> Permissions { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

        #endregion

    }
}
