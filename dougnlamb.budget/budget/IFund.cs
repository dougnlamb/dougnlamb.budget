using dougnlamb.core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace dougnlamb.budget {
    public interface IFund : IBaseObject {
        int oid { get; }
        string Name { get; }

        IMoney Balance { get; }
        IMoney Goal { get; }

        // TODO: Figure out the relationship between Allocations in IBudgetItem and IFund.
        IObservable<IAllocation> Allocations { get; }

        DateTime TargetDate { get; set; }
    }
}