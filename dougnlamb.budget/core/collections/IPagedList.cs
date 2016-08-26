using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    public interface IPagedList<T> {

        int PageSize { get; set; }
        int PageCount { get; }
        int TotalItemCount { get; }

        IReadOnlyList<T> this[int pageIndex] { get; }
        IReadOnlyList<T> AllItems { get; }
    }
}
