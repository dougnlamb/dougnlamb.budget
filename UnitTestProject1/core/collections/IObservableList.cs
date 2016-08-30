using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test.budget.core.collections {
    public interface IObservableList<T> : INotifyCollectionChanged, IList<T> { 
    }
}
