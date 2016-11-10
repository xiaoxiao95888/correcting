using Correcting.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Correcting.Controllers.api
{
    public class LocationController : BaseApiController
    {
        public object Get()
        {
            return ApiHelper.GetLocationSubs();
        }
        /// <summary>
        /// 获取子集
        /// </summary>
        /// <param name="id">parentId</param>
        /// <returns></returns>
        public object Get(string parentId)
        {
            return ApiHelper.GetLocation(parentId);
        }
    }
}
