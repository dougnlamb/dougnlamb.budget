using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class CurrencyViewModel : ICurrencyViewModel {
        public CurrencyViewModel(ICurrency currency) {
            this.oid = currency?.oid ?? 0;
            this.Code = currency?.Code ?? "";
            this.Description = currency?.Description ?? "";
        }

        public int oid { get; internal set; }
        public string Code { get; internal set; }
        public string Description { get; internal set; }
    }
}
