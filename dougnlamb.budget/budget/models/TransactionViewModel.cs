using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class TransactionViewModel : ITransactionViewModel {

        public TransactionViewModel(ISecurityContext securityContext, ITransaction transaction) {
            Note = transaction?.Note ?? "";
            oid = transaction?.oid ?? 0;
            TransactionAmount = transaction?.TransactionAmount?.View(securityContext) ?? new MoneyViewModel(securityContext, null);
            TransactionDate = transaction?.TransactionDate ?? new DateTime();
        }
        public int oid { get; internal set; }
        public string Note { get; internal set; }
        public IMoneyViewModel TransactionAmount { get; internal set; }
        public DateTime TransactionDate { get; internal set; }
    }
}
