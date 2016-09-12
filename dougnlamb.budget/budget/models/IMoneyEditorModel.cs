using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IMoneyEditorModel {
        decimal Amount { get; set; }

        ICurrency Currency { get;  }
    }
}