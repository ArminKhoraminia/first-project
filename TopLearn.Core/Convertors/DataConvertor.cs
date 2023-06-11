using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopLearn.Core.Convertors
{
    public static class DataConvertor
    {
        public static string DateToShamsi(this DateTime date)
        {
            PersianCalendar tmpdata = new PersianCalendar();
            return tmpdata.GetYear(date) + "/" +
                tmpdata.GetMonth(date).ToString("00") + "/" +
                tmpdata.GetDayOfMonth(date).ToString("00");
        }
    }
}
