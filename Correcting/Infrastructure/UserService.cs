using Correcting.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace Correcting.Infrastructure
{
    public class UserService
    {
        public static ClaimsIdentity CreateIdentity(EmployeeModel user, string authenticationType)
        {
            ClaimsIdentity _identity = new ClaimsIdentity(DefaultAuthenticationTypes.ApplicationCookie);
            _identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
            _identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Code));
            _identity.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider", "ASP.NET Identity"));
            _identity.AddClaim(new Claim("DisplayName", user.Name));
            _identity.AddClaim(new Claim("Code", user.Code));
            _identity.AddClaim(new Claim("HeadImgUrl", user.HeadImgUrl==null?"": user.HeadImgUrl));
            _identity.AddClaim(new Claim("Title", user.Title==null?"": user.Title));
            _identity.AddClaim(new Claim("Id", user.Id.ToString()));
            return _identity;
        }
    }
}