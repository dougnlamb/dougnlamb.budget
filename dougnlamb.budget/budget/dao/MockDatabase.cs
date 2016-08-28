using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.dao {
    public class MockDatabase {
        private static Dictionary<int, IAccount> dbAccounts = new Dictionary<int, IAccount>() {
            { 1000, new Account() {oid = 1000,Name="Bubba's account" } },
            { 1001, new Account() {oid = 1001,Name="Gump's account" } }
        };

        public static IAccount RetrieveAccount(int oid) {
            return dbAccounts[oid];
        }

        public static int InsertAccount(IAccount account) {
            int oid = GetNextAccountOid();
            dbAccounts.Add(oid, account);
            return oid;
        }

        public static void UpdateAccount(IAccount account) {
            dbAccounts[account.oid] = account;
        }

        private static int GetNextAccountOid() {
            int oid = 1;
            foreach (int id in dbAccounts.Keys) {
                if (id + 1 > oid) {
                    oid = id + 1;
                }
            }

            return oid;
        }
    }
}
