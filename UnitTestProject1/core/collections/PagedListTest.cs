using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using dougnlamb.core.collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace test.core.collections {
    [TestClass]
    public class PagedListTest {
        [TestMethod]
        public void Test() {
            TestPagedList list = new TestPagedList(27);

            foreach(int x in list.AllItems) {
                Trace.WriteLine(x);
            }
            list.PageSize = 10;
            Assert.AreEqual(3, list.PageCount);
            testPageContents(list);

            list = new TestPagedList(2);
            list.PageSize = 10;
            Assert.AreEqual(1, list.PageCount);
            testPageContents(list);

            list = new TestPagedList(10);
            list.PageSize = 10;
            Assert.AreEqual(1, list.PageCount);
            testPageContents(list);

            list = new TestPagedList(0);
            list.PageSize = 10;
            Assert.AreEqual(0, list.PageCount);
            testPageContents(list);

            PagedList<string> strList = new PagedList<string>(new List<string> {
                "one",
                "two",
                "three",
                "four",
                "five"
            });
            strList.PageSize = 2;
            Assert.AreEqual(3, strList.PageCount);

            for (int page = 0; page < strList.PageCount; page++) {
                IReadOnlyList<string> currentPage = strList[page];
                for (int idx = 0; idx < currentPage.Count; idx++) {
                    Trace.WriteLine(currentPage[idx]);
                }
            }
        }
        private void testPageContents(TestPagedList list) { 
            for (int page = 0; page < list.PageCount; page++) {
                IReadOnlyList<int> currentPage = list[page];
                for (int idx = 0; idx < currentPage.Count; idx++) {
                    Assert.AreEqual(idx + (page * list.PageSize) + 1, currentPage[idx]);
                    Trace.WriteLine(currentPage[idx]);
                }
            }
        }
    }
}
