using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public class Pagination
    {
        public const string Ascending = "ASC";
        public const string Descending = "DESC";
        public int Page { get; set; }
        public int PageSize { get; set; }

        public string SortDirection { get; set; } = Ascending;
        public string SortBy { get; set; } = String.Empty;

        public Pagination(int page, int pageSize, string sortBy, string sortDir)
        {
            Page = page;
            PageSize = pageSize;
            SortDirection = sortDir;
            SortBy = sortBy;
        }

        public Pagination()
        {
            Page = 1;
            PageSize = 50;
            SortDirection = Ascending;
            SortBy = String.Empty;
        }
    }
}
