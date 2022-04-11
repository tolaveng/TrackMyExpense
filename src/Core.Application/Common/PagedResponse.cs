using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse()
        {
            Data = new List<T>();
            TotalCount = 0;
        }

        public PagedResponse(IEnumerable<T> data, int totalCount)
        {
            Data = data;
            TotalCount = totalCount;
        }

        public static PagedResponse<T> Result(IEnumerable<T> data, int totalCount) {
            return new PagedResponse<T>()
            {
                Data = data,
                TotalCount = totalCount
            };
        }
    }
}
