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

        private static Dictionary<int, IBudget> dbBudgets = new Dictionary<int, IBudget>() {
            { 1000, new Budget() {oid = 1000,Name="Bubba's budget" } },
            { 1001, new Budget() {oid = 1001,Name="Gump's budget" } }
        };

        private static Dictionary<int, IUser> dbUsers = new Dictionary<int, IUser>() {
            {1000, new User() { oid = 1000,
                UserId = "bubba",
                DisplayName = "Bubba Gump",
                Email = "bubba@example.com" } },
            {1001, new User() { oid = 1000,
                UserId = "gump",
                DisplayName = "Gump the Kid",
                Email = "gump@example.com" } }
        };

        public static IAccount RetrieveAccount(int oid) {
            return dbAccounts[oid];
        }

        internal static IBudget RetrieveBudget(int oid) {
            return dbBudgets[oid];
        }

        public static IUser RetrieveUser(int oid) {
            return dbUsers[oid];
        }

        public static int InsertAccount(IAccount account) {
            int oid = GetNextOid(dbAccounts.Keys.ToArray());
            dbAccounts.Add(oid, account);
            return oid;
        }

        public static void UpdateAccount(IAccount account) {
            dbAccounts[account.oid] = account;
        }

        public static int InsertBudget(IBudget budget) {
            int oid = GetNextOid(dbBudgets.Keys.ToArray());
            dbBudgets.Add(oid, budget);
            return oid;
        }

        public static void UpdateBudget(IBudget budget) {
            dbBudgets[budget.oid] = budget;
        }

        public static int InsertUser(IUser user) {
            int oid = GetNextOid(dbUsers.Keys.ToArray());
            dbUsers.Add(oid, user);
            return oid;
        }

        public static void UpdateUser(IUser user) {
            dbUsers[user.oid] = user;
        }

        private static int GetNextOid(int [] oids) {
            int oid = 1;
            foreach (int id in oids) { 
                if (id + 1 > oid) {
                    oid = id + 1;
                }
            }

            return oid;
        }
    }
}
