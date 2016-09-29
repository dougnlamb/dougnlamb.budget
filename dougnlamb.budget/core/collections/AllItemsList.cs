using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    class AllPagedItemsList<T> : IReadOnlyList<T>, IEnumerator<T> {
        private IPagedList<T> mPagedList;

        public AllPagedItemsList(IPagedList<T> pagedList) {
            mPagedList = pagedList;
        }

        public T this[int index] {
            get {
                int pageId = index / mPagedList.PageSize;
                int indexOnPage = index % mPagedList.PageSize;

                return mPagedList[pageId][indexOnPage];
            }
        }

        public int Count {
            get {
                return mPagedList.TotalItemCount;
            }
        }

        private int mCurrentId = -1;
        public T Current {
            get {
                return this[mCurrentId];
            }
        }

        object IEnumerator.Current {
            get {
                return this.Current;
            }
        }

        public void Dispose() {
            mPagedList = null;
        }

        public IEnumerator<T> GetEnumerator() {
            return this;
        }

        public bool MoveNext() {
            mCurrentId += 1;
            return mCurrentId < Count;
        }

        public void Reset() {
            mCurrentId = 0;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }
    }
}
