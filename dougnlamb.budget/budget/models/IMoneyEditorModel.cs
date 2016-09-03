using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public interface IMoneyEditorModel {
        decimal Amount { get; set; }

        int CurrencyId { get; set; }
        ICurrencyViewModel Currency { get;  }
        IList<ICurrencyViewModel> Currencies { get; }
    }
}