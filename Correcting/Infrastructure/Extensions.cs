using Correcting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
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
        public static EmployeeModel GetEmoloyee(this IIdentity identity)
        {
            var model = new EmployeeModel();
            var claimsIdentity = (ClaimsIdentity)identity;
            IEnumerable<Claim> claims = claimsIdentity.Claims;
            model.HeadImgUrl = claims.Where(n => n.Type == "HeadImgUrl").Select(n=>n.Value).FirstOrDefault();
            model.Title = claims.Where(n => n.Type == "Title").Select(n => n.Value).FirstOrDefault();
            model.Name = claims.Where(n => n.Type == "DisplayName").Select(n => n.Value).FirstOrDefault();
            model.Code = claims.Where(n => n.Type == "Code").Select(n => n.Value).FirstOrDefault();
            model.Id = claims.Where(n => n.Type == "Id").Select(n => new Guid(n.Value)).FirstOrDefault();
            return model; 
        }
    }
}