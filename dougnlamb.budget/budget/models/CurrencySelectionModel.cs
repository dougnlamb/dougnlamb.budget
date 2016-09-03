using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class CurrencySelectionModel : ICurrencySelectionModel {
        public CurrencySelectionModel(int oid) {
            SelectedCurrencyId = oid;
        }

        public IList<ICurrencyViewModel> Currencies {
            get {
                throw new NotImplementedException();
            }
        }

        public int SelectedCurrencyId { get; set; }

        public ICurrencyViewModel SelectedCurrency {
            get {
                throw new NotImplementedException();
            }
        }
    }
}