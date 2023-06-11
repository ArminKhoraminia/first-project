using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.User;

namespace TopLearn.DataLayer.Entities.Order
{
    public class Order
    {
        public Order()
        {
            
        }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int OrderPrice { get; set; }

        public bool IsFinaly { get; set; }

        public DateTime CreateDate { get; set; }

        #region Relations
        public virtual User.User User { get; set; }
        public virtual List<OrderDetail> OrderDetails { get; set; }


        #endregion
    }
}
