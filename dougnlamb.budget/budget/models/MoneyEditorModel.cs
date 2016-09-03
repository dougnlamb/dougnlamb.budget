using System;
using System.Collections.Generic;

namespace dougnlamb.budget.models {
    public class MoneyEditorModel : IMoneyEditorModel {
        public MoneyEditorModel(decimal amount, ICurrency currency) {
            Amount = amount;
            CurrencyId = currency.oid;
        }

        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }

        public ICurrencyViewModel Currency {
            get {
                throw new NotImplementedException();
            }
        }

        public IList<ICurrencyViewModel> Currencies {
            get {
                throw new NotImplementedException();
            }
        }

    }
}