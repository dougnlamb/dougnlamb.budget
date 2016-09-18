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
        private ISecurityContext mSecurityContext;
        private bool mIsLoaded;

        public Currency(ISecurityContext securityContext) {
            this.mSecurityContext = securityContext;
            this.mIsLoaded = true;
        }
        public Currency(ISecurityContext securityContext, int oid) : this(securityContext) {
            this.oid = oid;
            this.mIsLoaded = false;
        }

        public int oid { get; set; }

        private string mCode;
        public string Code {
            get {
                Load();
                return mCode;
            }
            set {
                mCode = value;
            }
        }

        private string mDescription;
        public string Description {
            get {
                Load();
                return mDescription;
            }
            set {
                mDescription = value;
            }
        }

        public IMoney Convert(IMoney money) {
            decimal value = money.Value * GetConversionFactor(money.Currency);
            return new Money() { Currency = this, Value = value };
        }

        public ICurrencyViewModel View(ISecurityContext securityContext) {
            return new CurrencyViewModel(securityContext, this);
        }

        public decimal GetConversionFactor(ICurrency currency) {
            return 1;
        }

        public static ICurrencyDao GetDao() {
            return new CurrencyDao();
        }

        protected void Load() {
            if (!mIsLoaded) {
                Refresh();
                mIsLoaded = true;
            }
        }

        public void Refresh() {
            ICurrency curr = GetDao().Retrieve(mSecurityContext, oid);
            this.Code = curr.Code;
            this.Description = curr.Description;
        }
    }
}
