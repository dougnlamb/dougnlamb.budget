using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class CurrencySelectionModel : ICurrencySelectionModel {
        private ISecurityContext mSecurityContext;
        public CurrencySelectionModel() { }
        public CurrencySelectionModel(ISecurityContext securityContext, ICurrency currency) {
            this.mSecurityContext = securityContext;
            SelectedItem = currency?.View(securityContext) ?? null;
        }

        public IList<ICurrencyViewModel> Currencies {
            get {
                throw new NotImplementedException();
            }
        }

        public ICurrencyViewModel SelectedItem { get; set; }
        public ICurrency SelectedCurrency {
            get {
                if (SelectedItem == null) {
                    return null;
                }
                else {
                    return new Currency(mSecurityContext, SelectedItem.oid);
                }
            }
        }
    }
}