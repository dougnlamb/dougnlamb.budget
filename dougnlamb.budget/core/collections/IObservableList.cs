using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.core.collections {
    public interface IObservableList<T> : INotifyCollectionChanged, IList<T> { 
    }
}
