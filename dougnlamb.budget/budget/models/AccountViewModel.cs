using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;

namespace dougnlamb.budget.models {
    public class AccountViewModel : IAccountViewModel {
        private ISecurityContext mSecurityContext;
        private IAccount mAccount;

        public AccountViewModel(ISecurityContext securityContext, IAccount account) {
            mSecurityContext = securityContext;
            mAccount = account;
           
            oid = account?.oid ?? 0;
            DefaultCurrency = account?.DefaultCurrency?.View(securityContext) ?? new CurrencyViewModel(securityContext, null);
            Name = account?.Name ?? "";
            Owner = account?.Owner?.View(securityContext) ?? new UserViewModel(securityContext, null);
        }
        public ICurrencyViewModel DefaultCurrency { get; internal set; }
        public string Name { get; internal set;}
        public int oid { get; internal set;}
        public IUserViewModel Owner { get; internal set;}

        private IPagedList<ITransactionViewModel> mTransactions;
        public IPagedList<ITransactionViewModel> Transactions {
            get {
                if(mTransactions == null) {
                    IList<ITransactionViewModel> list = new List<ITransactionViewModel>();
                    foreach(ITransaction trans in mAccount.Transactions.AllItems) {
                        list.Add(trans.View(mSecurityContext));
                    }
                    mTransactions = new PagedList<ITransactionViewModel>(list);
                }
                return mTransactions;
            }
        }
    }
}
