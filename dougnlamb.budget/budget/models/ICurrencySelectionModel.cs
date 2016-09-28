using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public interface ICurrencySelectionModel {
        int SelectedCurrencyCode { get; set; }
        ICurrency SelectedCurrency { get; }
        IList<ICurrencyViewModel> Currencies { get; }
    }
}
