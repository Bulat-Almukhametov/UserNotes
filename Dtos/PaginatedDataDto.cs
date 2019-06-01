using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserNotes.Dtos
{
    public class PaginatedDataDto<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalItems { get; set; }
    }
}
