using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public interface ICurrencySelectionModel {
        int SelectedCurrencyId { get; set; }
        ICurrencyViewModel SelectedCurrency { get;  }
        IList<ICurrencyViewModel> Currencies { get; }
    }
}
