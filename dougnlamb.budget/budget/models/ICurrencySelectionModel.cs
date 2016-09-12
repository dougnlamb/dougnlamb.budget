using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public interface ICurrencySelectionModel {
        ICurrency SelectedCurrency { get; }

        ICurrencyViewModel SelectedItem { get; set; }
        IList<ICurrencyViewModel> Currencies { get; }
    }
}
