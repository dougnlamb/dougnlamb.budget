using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    public class PagedList<T> : IPagedList<T> {
        private List<T> mList;

        public PagedList(IList<T> list) {
            mList = new List<T>(list);
        }

        public int PageCount {
            get {
                return (mList.Count + (PageSize - 1)) / PageSize;
            }
        }

        public int PageSize { get; set; }

        public int TotalItemCount {
            get {
                return mList.Count;
            }
        }

        private IReadOnlyList<T> mAllItems = null;
        public IReadOnlyList<T> AllItems {
            get {
                if (mAllItems == null) {
                    mAllItems = new AllPagedItemsList<T>(this);
                }
                return mAllItems;
            }
        }

        public IReadOnlyList<T> this[int pageIndex] {
            get {
                return new List<T>(mList.Skip<T>((pageIndex) * PageSize).Take(PageSize));
            }
        }
    }
}
