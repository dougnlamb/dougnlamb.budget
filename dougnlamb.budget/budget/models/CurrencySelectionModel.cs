using dougnlamb.core.security;
using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class CurrencySelectionModel : ICurrencySelectionModel {
        private ISecurityContext mSecurityContext;

        public CurrencySelectionModel() { }

        public CurrencySelectionModel(ISecurityContext securityContext, ICurrency currency) {
            this.mSecurityContext = securityContext;
            SelectedCurrencyCode = currency?.oid ?? 0;
        }

        private IList<ICurrencyViewModel> mCurrencies;
        public IList<ICurrencyViewModel> Currencies {
            get {
                if (mCurrencies == null) {
                    IList<ICurrency> currencies = Currency.GetDao().RetrieveAll(mSecurityContext);
                    mCurrencies = new List<ICurrencyViewModel>();
                    foreach (ICurrency curr in currencies) {
                        mCurrencies.Add(curr.View(mSecurityContext));
                    }
                }
                return mCurrencies;
            }
        }

        public int SelectedCurrencyCode { get; set; }

        public ICurrency SelectedCurrency {
            get {
                if (SelectedCurrencyCode == 0) {
                    return null;
                }
                else {
                    return Currency.GetDao().Retrieve(mSecurityContext, SelectedCurrencyCode);
                }
            }
        }
    }
}