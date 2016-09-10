using dougnlamb.core.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.models {
    public class AccountViewModel : IAccountViewModel {

        public AccountViewModel(ISecurityContext securityContext, IAccount account) {
            oid = account?.oid ?? 0;
            DefaultCurrency = account?.DefaultCurrency?.View(securityContext) ?? new CurrencyViewModel(securityContext, null);
            Name = account?.Name ?? "";
            Owner = account?.Owner?.View(securityContext) ?? new UserViewModel(securityContext, null);
        }
        public ICurrencyViewModel DefaultCurrency { get; internal set; }
        public string Name { get; internal set;}
        public int oid { get; internal set;}
        public IUserViewModel Owner { get; internal set;}
    }
}
