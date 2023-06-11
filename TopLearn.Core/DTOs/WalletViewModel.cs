using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopLearn.DataLayer.Entities.Wallet;

namespace TopLearn.Core.DTOs
{
    public class GetWalletDataViewModel
    {
        [Display(Name = "مبلغ شارژ کیف  پول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        public int Amount { get; set; }
    }

    public class GetAllWalletUserViewModel
    {
        [Display(Name = "نوع تراکنش")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید ")]
        [ForeignKey("TypeId")]
        public int TypeId { get; set; }

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
    }


}
