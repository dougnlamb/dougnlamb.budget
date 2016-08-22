using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    public class PagedList<T> : IPagedList<T> {
        IList<T> mList;

        public PagedList(IList<T> list) {
            mList = list;
        }

        public int PageCount {
            get {
                return (mList.Count + (PageSize -1)) / PageSize;
            }
        }

        public int PageSize {get; set; }

        public int TotalItemCount {
            get {
                return mList.Count;
            }
        }

        public IList<T> Items(int pageId) {
            return new List<T>(mList.Skip<T>((pageId) * PageSize).Take(PageSize));
        }
    }
}
