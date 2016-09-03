using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.budget.models;
using dougnlamb.core.security;
using dougnlamb.budget.dao;

namespace dougnlamb.budget {
    public class Currency : ICurrency {
        public int oid { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public IMoney Convert(IMoney money) {
            throw new NotImplementedException();
        }

        public ICurrencyViewModel View(ISecurityContext securityContext) {
            return new CurrencyViewModel(this);
        }

        public static ICurrencyDao GetDao() {
            return new CurrencyDao();
        }
    }
}
