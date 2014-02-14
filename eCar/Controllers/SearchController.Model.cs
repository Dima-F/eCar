using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eCar.Applicaton.Models;

namespace eCar.Controllers
{
    public partial class SearchController
    {
        public class IndexModel : LayoutModel
        {
            public string QueryText { get; set; }
            public IEnumerable<SearchResultModel> Results { get; set; }
        }
        public class SearchResultModel
        {
            public int AutoId { get; set; }
            public string Name { get; set; }
            public string ShortDescription { get; set; }
        }
    }
}