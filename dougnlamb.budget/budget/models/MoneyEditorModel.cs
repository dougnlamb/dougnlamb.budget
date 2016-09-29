using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class MoneyEditorModel : IMoneyEditorModel {
        public MoneyEditorModel() {
        }

        public MoneyEditorModel(decimal amount, ICurrency currency) {
            CurrencySelector = new CurrencySelectionModel();
            Currency = currency;
            Amount = amount;
        }

        public MoneyEditorModel(IMoney money) {
            CurrencySelector = new CurrencySelectionModel();
            Currency = money?.Currency ?? null;
            Amount = money?.Value ?? 0;
        }

        public decimal Amount { get; set; }

        public ICurrency Currency {
            get {
                return CurrencySelector.SelectedCurrency;
            }
            internal set {
                CurrencySelector.SelectedCurrencyCode = value?.oid ?? 0;
            }
        }

        public CurrencySelectionModel CurrencySelector { get; set; }
    }
}