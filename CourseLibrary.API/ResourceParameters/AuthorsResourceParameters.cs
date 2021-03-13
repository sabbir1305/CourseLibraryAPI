using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseLibrary.API.ResourceParameters
{
    public class AuthorsResourceParameters
    {
        const int MaxSize = 20;
        public string MainCategory { get; set; }
        public string SearchQuery { get; set; }

        public int PageNumber { get; set; } = 1;
        public int PageSize {
            get => _pageSize;
            set =>_pageSize= (value > MaxSize) ? MaxSize : value;
        }
        private int _pageSize=10;

        public string OrderBy { get; set; } = "Name";
    }
}
