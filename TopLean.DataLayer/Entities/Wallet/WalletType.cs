using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TopLearn.DataLayer.Entities.Wallet
{
    public class WalletType
    {
        public WalletType()
        {
            
        }

        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [MaxLength(150, ErrorMessage = "طول {0} نمی تواند بیشتر از {1} باشد")]
        public string Description { get; set; }


        #region Relation
        public virtual List<Wallet> Wallets { get; set; }
        #endregion
    }
}
