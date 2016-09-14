using dougnlamb.core.collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dougnlamb.budget.dao {
    public class MockDatabase {
        public static void init() {
            dbCurrencies = new Dictionary<int, ICurrency>() {
            { 1000, new Currency(null) {oid = 1000, Code="USD", Description="US Dollars" } },
            { 1001, new Currency(null) {oid = 1001,Code="CAD", Description ="Canadian Dollars" } }
        };

            dbUsers = new Dictionary<int, IUser>() {
            {1000, new User(null) { oid = 1000,
                UserId = "bubba",
                DisplayName = "Bubba Gump",
                Email = "bubba@example.com",
                DefaultCurrency = dbCurrencies[1000] } },
            {1001, new User(null) { oid = 1000,
                UserId = "gump",
                DisplayName = "Gump the Kid",
                Email = "gump@example.com",
                DefaultCurrency = dbCurrencies[1000] } }
        };

            dbAccounts = new Dictionary<int, IAccount>() {
            { 1000, new Account(null) {oid = 1000, Owner = new User(null, 1), Name="Bubba's account", DefaultCurrency = dbCurrencies[1000] } },
            { 1001, new Account(null) {oid = 1001, Owner = new User(null, 1), Name="Gump's account", DefaultCurrency = dbCurrencies[1000] } }
        };

            dbBudgets = new Dictionary<int, IBudget>() {
            { 1000, new Budget(null) {oid = 1000, Owner = new User(null, 1), Name="Bubba's budget", DefaultCurrency = dbCurrencies[1000] } },
            { 1001, new Budget(null) {oid = 1001, Owner = new User(null, 1), Name="Gump's budget", DefaultCurrency = dbCurrencies[1000] } },
            { 1002, new Budget(null) {oid = 1002, Owner = new User(null, 1), Name="Stacey's budget", DefaultCurrency = dbCurrencies[1000] } }
        };

            dbBudgetItems = new Dictionary<int, IBudgetItem>() {
            { 1000, new BudgetItem(null) {oid = 1000,
                Name ="Entertainment",
                Budget = dbBudgets[1000],
                Amount = new Money() {Amount=100, Currency = dbCurrencies[1000] },
                Balance = new Money() {Amount=100, Currency = dbCurrencies[1000] } } },
            { 1001, new BudgetItem(null) {oid = 1001,
                Name ="Electricity",
                Budget = dbBudgets[1000],
                Amount = new Money() {Amount=100, Currency = dbCurrencies[1000] } ,
                Balance = new Money() {Amount=100, Currency = dbCurrencies[1000] } } },
            { 1002, new BudgetItem(null) {oid = 1002, 
                Name ="Savings",
                Budget = dbBudgets[1000],
                Amount = new Money() {Amount=100, Currency = dbCurrencies[1000] } ,
                Balance = new Money() {Amount=100, Currency = dbCurrencies[1000] } } }
        };

            dbTransactions = new Dictionary<int, ITransaction>() {
            { 1000, new Transaction(null) {oid = 1000,
                Note = "My Transaction",
                TransactionAmount = new Money() { Amount = -25, Currency = dbCurrencies[1000] } } },
            { 1001, new Transaction(null) {oid = 1000,
                Note = "Her Transaction",
                TransactionAmount = new Money() { Amount = -30, Currency = dbCurrencies[1000] } } }
        };
            dbAllocations = new Dictionary<int, IAllocation>() {
                {1000, new Allocation(null) { oid = 1000,
                    Notes = "My Allocation",
                    BudgetItem = dbBudgetItems[1000],
                    Transaction = dbTransactions[1000],
                    Amount = new Money() {Amount = -25, Currency = dbCurrencies[1000] } } }
            };


        }

        internal static IObservableList<IAccount> RetrieveAccounts(User user) {
            IObservableList<IAccount> accounts = new ObservableList<IAccount>();
            foreach (IAccount account in dbAccounts.Values) {
                if (account.Owner.oid == user.oid) {
                    accounts.Add(account);
                }
            }
            return accounts;
        }

        internal static IObservableList<IAllocation> RetrieveAllocations(IBudgetItem budgetItem) {
            IObservableList<IAllocation> allocations = new ObservableList<IAllocation>();
            foreach (IAllocation allocation in dbAllocations.Values) {
                if (allocation.BudgetItem.oid == budgetItem.oid) {
                    allocations.Add(allocation);
                }
            }
            return allocations;
        }

        internal static IObservableList<IAllocation> RetrieveAllocations(ITransaction transaction) {
            throw new NotImplementedException();
        }

        internal static void UpdateAllocation(IAllocation allocation) {
            dbAllocations[allocation.oid] = allocation;
        }

        internal static int InsertAllocation(IAllocation allocation) {
            int oid = GetNextOid(dbAccounts.Keys.ToArray());
            dbAllocations.Add(oid, allocation);
            return oid;
        }

        internal static IAllocation RetrieveAllocation(int oid) {
            return dbAllocations[oid];
        }

        private static Dictionary<int, IAccount> dbAccounts;
        private static Dictionary<int, IBudget> dbBudgets;
        private static Dictionary<int, IBudgetItem> dbBudgetItems;

        private static Dictionary<int, IUser> dbUsers;

        private static Dictionary<int, ICurrency> dbCurrencies;

        private static Dictionary<int, ITransaction> dbTransactions;

        private static Dictionary<int, IAllocation> dbAllocations;

        public static IAccount RetrieveAccount(int oid) {
            return dbAccounts[oid];
        }

        internal static IBudget RetrieveBudget(int oid) {
            return dbBudgets[oid];
        }

        internal static IBudgetItem RetrieveBudgetItem(int oid) {
            return dbBudgetItems[oid];
        }

        internal static IObservableList<IBudgetItem> RetrieveBudgetItems(IBudget budget) {
            IObservableList<IBudgetItem> items = new ObservableList<IBudgetItem>();
            foreach (IBudgetItem itm in dbBudgetItems.Values) {
                if (itm?.Budget?.oid == budget.oid) {
                    items.Add(itm);
                }
            }

            return items;
        }

        internal static ICurrency RetrieveCurrency(int oid) {
            return dbCurrencies[oid];
        }

        public static IUser RetrieveUser(int oid) {
            return dbUsers[oid];
        }

        public static ITransaction RetrieveTransaction(int oid) {
            return dbTransactions[oid];
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

        internal static int InsertBudgetItem(IBudgetItem budgetItem) {
            int oid = GetNextOid(dbBudgetItems.Keys.ToArray());
            dbBudgetItems.Add(oid, budgetItem);
            return oid;
        }

        internal static void UpdateBudgetItem(IBudgetItem budgetItem) {
            dbBudgetItems[budgetItem.oid] = budgetItem;
        }

        public static int InsertUser(IUser user) {
            int oid = GetNextOid(dbUsers.Keys.ToArray());
            dbUsers.Add(oid, user);
            return oid;
        }

        public static int InsertTransaction(ITransaction transaction) {
            int oid = GetNextOid(dbTransactions.Keys.ToArray());
            dbTransactions.Add(oid, transaction);
            return oid;
        }

        public static void UpdateUser(IUser user) {
            dbUsers[user.oid] = user;
        }

        private static int GetNextOid(int[] oids) {
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
