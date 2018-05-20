using System.Collections.Generic;

namespace Plain.Infrastructure.Domain
{
    public class PagedResult<T>
    {
        public int TotalItems { get; set; }

        public IList<T> PageOfResults { get; set; }
    }
}
