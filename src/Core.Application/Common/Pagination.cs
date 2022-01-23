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
        public string Search { get; set; }
        public string SortDirection { get; set; } = Ascending;
        public string SortBy { get; set; } = String.Empty;

        public Pagination(int page, int pageSize, string search, string sortBy, string sortDirection)
        {
            Page = page;
            PageSize = pageSize;
            Search = search;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public Pagination(int page, int pageSize, string sortBy, string sortDirection)
        {
            Page = page;
            PageSize = pageSize;
            Search = String.Empty;
            SortBy = sortBy;
            SortDirection = sortDirection;
        }

        public Pagination()
        {
            Page = 1;
            PageSize = 50;
            Search = String.Empty;
            SortBy = String.Empty;
            SortDirection = Ascending;
        }
    }
}
