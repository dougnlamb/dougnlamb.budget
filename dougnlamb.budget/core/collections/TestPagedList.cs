﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    public class TestPagedList : IPagedList<int> {
        List<int> mInts;

        public TestPagedList(int count) {
            mInts = new List<int>();
            for (int idx = 1; idx <= count; idx++) {
                mInts.Add(idx);
            }
            PageSize = 10;
        }

        public int PageCount {
            get {
                return (mInts.Count + (PageSize - 1)) / PageSize;
            }
        }

        public int PageSize { get; set; }

        public int TotalItemCount {
            get {
                return mInts.Count;
            }
        }

        public IReadOnlyList<int> AllItems {
            get {
                return new AllPagedItemsList<int>(this);
            }
        }

        public IReadOnlyList<int> this[int pageIndex] {
            get {
                return new List<int>(mInts.Skip((pageIndex) * PageSize).Take(PageSize));
            }
        }
    }
}
