using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Order;

namespace TopLearn.DataLayer.Entities.User
{
    public class UserDiscount
    {
        public UserDiscount()
        {
            
        }

        [Key]
        public int UserDiscountId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int DiscountId { get; set; }

        #region Relations

        public virtual User User { get; set; }
        public virtual Discount Discount { get; set; }

        #endregion
    }
}
