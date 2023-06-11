using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Convertors
{
    public class FixedText
    {
        public static string TrimeAndLowerText(string str)
        {
            return str.Trim().ToLower();
        }
    }
}
