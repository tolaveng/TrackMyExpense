using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Common
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }

        public static PaginationResponse<T> Result(IEnumerable<T> data, int totalCount) {
            return new PaginationResponse<T>()
            {
                Data = data,
                TotalCount = totalCount
            };
        }
    }
}
