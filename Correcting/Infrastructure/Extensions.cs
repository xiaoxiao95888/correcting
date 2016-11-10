using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Infrastructure
{
    public static class Extensions
    {
        public static string ToChineseNumerals(this string number)
        {
            int num;
            if (int.TryParse(number,out num))
            {
                switch (num)
                {
                    case 1:
                        return "一";
                    case 2:
                        return "二";
                    case 3:
                        return "三";
                    default:
                        return "";
                }
            }
            return null;
           
        }
    }
}