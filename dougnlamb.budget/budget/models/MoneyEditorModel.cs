using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class MoneyEditorModel : IMoneyEditorModel {
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
            set {
                CurrencySelector.SelectedCurrencyCode = value?.oid ?? 0;
            }
        }

        public ICurrencySelectionModel CurrencySelector { get; set; }
    }
}