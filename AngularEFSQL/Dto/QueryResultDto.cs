using System.Collections.Generic;

namespace AngularEFSQL.Dto
{
    public class QueryResultDto<T>
    {
        public int TotalItems { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
