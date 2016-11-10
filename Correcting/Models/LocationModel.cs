using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Correcting.Models
{
    public class LocationModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<LocationModel> SubModels { get; set; }
        public string ParentId { get; set; }
    }
    public class LocationJsonModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public LocationParentJsonModel Parent { get; set; }       
    }
    public class LocationParentJsonModel
    {
        public string Id { get; set; }
    }
}