using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Generator
{
    public class CodeGenerator
    {
        public static string GenerateUniqueCode()
        {
            // بعد از ساختن عدد به صورت رشته در آنها دش وجود دارد که باید آنها را حذف کنیم
            return Guid.NewGuid().ToString().Replace("-","");
        }
    }
}
