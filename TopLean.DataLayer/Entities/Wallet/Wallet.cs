using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.DataLayer.Entities.Wallet
{
    public class Wallet
    {
        public Wallet()
        {

        }

        [Key]
        public int WalletId { get; set; }

        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [ForeignKey("WalletType")]
        public int TypeId { get; set; }

        [Display(Name = "یوزر تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [ForeignKey("User")]
        public int UserId { get; set; }

        [Display(Name = "مبلغ تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int Amount { get; set; }

        [Display(Name = "توضیحات تراکنش")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string Description { get; set; }

        [Display(Name = "تایید تراکنش")]
        [MaxLength(200, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public bool IsPayed { get; set; }

        [Display(Name = "تاریخ تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public DateTime DateTime { get; set; }

        #region Relation
        public virtual User.User User { get; set; }
        public virtual WalletType WalletType { get; set; }
        #endregion
    }
}
