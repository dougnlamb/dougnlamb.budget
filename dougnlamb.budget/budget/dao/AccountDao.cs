using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace dougnlamb.budget.dao {
    public class AccountDao : BaseDao, IAccountDao {

        public IObservableList<IAccount> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IAccount Retrieve(ISecurityContext securityContext, int oid) {
            IAccount account = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select account.oid account_oid, 
                                        account.createdBy account_createdBy, 
                                        account.createdDate account_createdDate, 
                                        account.defaultCurrency account_defaultCurrency, 
                                        account.name account_name, 
                                        account.owner account_owner, 
                                        account.updatedBy account_updatedBy, 
                                        account.updatedDate account_updatedDate,
                                        currency.oid currency_oid,
                                        currency.code currency_code,
                                        currency.description currency_description,
                                        owner.oid owner_oid, 
                                        owner.createdBy owner_createdBy, 
                                        owner.createdDate owner_createdDate, 
                                        owner.defaultCurrency owner_defaultCurrency, 
                                        owner.displayName owner_displayName, 
                                        owner.email owner_email, 
                                        owner.updatedBy owner_updatedBy, 
                                        owner.updatedDate owner_updatedDate, 
                                        owner.userId owner_userId,
                                        creator.oid creator_oid, 
                                        creator.createdBy creator_createdBy, 
                                        creator.createdDate creator_createdDate, 
                                        creator.defaultCurrency creator_defaultCurrency, 
                                        creator.displayName creator_displayName, 
                                        creator.email creator_email, 
                                        creator.updatedBy creator_updatedBy, 
                                        creator.updatedDate creator_updatedDate, 
                                        creator.userId creator_userId,
                                        updater.oid updater_oid, 
                                        updater.createdBy updater_createdBy, 
                                        updater.createdDate updater_createdDate, 
                                        updater.defaultCurrency updater_defaultCurrency, 
                                        updater.displayName updater_displayName, 
                                        updater.email updater_email, 
                                        updater.updatedBy updater_updatedBy, 
                                        updater.updatedDate updater_updatedDate, 
                                        updater.userId updater_userId
                                    from budget.dbo.account 
                                    left join budget.dbo.currency on account.defaultCurrency = currency.oid
                                    left join budget.dbo.[user] owner on account.owner = owner.oid
                                    left join budget.dbo.[user] creator on account.createdBy = creator.oid
                                    left join budget.dbo.[user] updater on account.updatedBy = updater.oid
                                        where oid = @oid";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) { 
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (account == null) {
                                account = BuildAccount(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return account;
        }

        public IObservableList<IAccount> Retrieve(ISecurityContext securityContext, IUser user) {
            IObservableList<IAccount> accounts = new ObservableList<IAccount>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select account.oid account_oid, 
                                        account.createdBy account_createdBy, 
                                        account.createdDate account_createdDate, 
                                        account.defaultCurrency account_defaultCurrency, 
                                        account.name account_name, 
                                        account.owner account_owner, 
                                        account.updatedBy account_updatedBy, 
                                        account.updatedDate account_updatedDate,
                                        currency.oid currency_oid,
                                        currency.code currency_code,
                                        currency.description currency_description,
                                        owner.oid owner_oid, 
                                        owner.createdBy owner_createdBy, 
                                        owner.createdDate owner_createdDate, 
                                        owner.defaultCurrency owner_defaultCurrency, 
                                        owner.displayName owner_displayName, 
                                        owner.email owner_email, 
                                        owner.updatedBy owner_updatedBy, 
                                        owner.updatedDate owner_updatedDate, 
                                        owner.userId owner_userId,
                                        creator.oid creator_oid, 
                                        creator.createdBy creator_createdBy, 
                                        creator.createdDate creator_createdDate, 
                                        creator.defaultCurrency creator_defaultCurrency, 
                                        creator.displayName creator_displayName, 
                                        creator.email creator_email, 
                                        creator.updatedBy creator_updatedBy, 
                                        creator.updatedDate creator_updatedDate, 
                                        creator.userId creator_userId,
                                        updater.oid updater_oid, 
                                        updater.createdBy updater_createdBy, 
                                        updater.createdDate updater_createdDate, 
                                        updater.defaultCurrency updater_defaultCurrency, 
                                        updater.displayName updater_displayName, 
                                        updater.email updater_email, 
                                        updater.updatedBy updater_updatedBy, 
                                        updater.updatedDate updater_updatedDate, 
                                        updater.userId updater_userId
                                    from budget.dbo.account 
                                    left join budget.dbo.currency on account.defaultCurrency = currency.oid
                                    left join budget.dbo.[user] owner on account.owner = owner.oid
                                    left join budget.dbo.[user] creator on account.createdBy = creator.oid
                                    left join budget.dbo.[user] updater on account.updatedBy = updater.oid
                                        order by name";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("owner", user.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            accounts.Add(BuildAccount(reader, securityContext));
                        }
                    }
                }

            }
            return accounts;
        }

        private IAccount BuildAccount(SqlDataReader reader, ISecurityContext securityContext) {
            Account account = new Account(securityContext);
            account.oid = (int)reader["account_oid"];
            account.CreatedDate = GetDateTime(reader,"account_createdDate");
            int currencyId = (int)reader["account_defaultCurrency"];
            if (currencyId > 0) {
                account.DefaultCurrency = new CurrencyDao().BuildCurrency(reader, securityContext);
            }
            account.Name = (string)reader["account_name"];
            account.UpdatedDate = GetDateTime(reader, "account_updatedDate");

            int createdById = reader["account_createdBy"] != DBNull.Value ? (int)reader["account_createdBy"] : 0;
            if (createdById > 0) {
                account.CreatedBy = new UserDao().BuildUser(reader, securityContext, "creator_");
            }
            int updatedById = reader["account_updatedBy"] != DBNull.Value ? (int)reader["account_updatedBy"] : 0;
            if (updatedById > 0) {
                account.UpdatedBy = new UserDao().BuildUser(reader, securityContext, "updater_");
            }
            account.UpdatedDate = GetDateTime(reader, "account_updatedDate");
            int ownerId = reader["account_owner"] != DBNull.Value ? (int)reader["account_owner"] : 0;
            if (ownerId > 0) {
                account.Owner = new UserDao().BuildUser(reader, securityContext, "owner_");
            }

            return account;
        }

        public int Save(ISecurityContext securityContext, IAccount account) {
            if (account.oid == 0) {
                return InsertAccount(account);
            }
            else {
                UpdateAccount(account);
                return account.oid;
            }
        }

        private int InsertAccount(IAccount account) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.account 
                        (createdBy, createdDate, defaultCurrency, name, owner, updatedBy, updatedDate)
                        values
                        (@createdBy, @createdDate, @defaultCurrency, @name, @owner, @updatedBy, @updatedDate);
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("createdBy", account.CreatedBy.oid);
                    cmd.Parameters.AddWithValue("createdDate", account.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", account.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("name", account.Name);
                    cmd.Parameters.AddWithValue("owner", account.Owner.oid);
                    cmd.Parameters.AddWithValue("updatedBy", 0);
                    cmd.Parameters.AddWithValue("updatedDate", DBNull.Value);

                    SqlParameter p = new SqlParameter();
                    p.ParameterName = "@ID";
                    p.SqlDbType = SqlDbType.Int;
                    p.Size = 4;
                    p.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(p);
                    cmd.ExecuteNonQuery();
                    return (int)p.Value;
                }
            }
        }

        private void UpdateAccount(IAccount account) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.account 
                        set createdBy = @createdBy,
                            createdDate = @createdDate,
                            defaultCurrency = @defaultCurrency,
                            name = @name,
                            owner = @owner,
                            updatedBy = @updatedBy,
                            updatedDate = @updatedDate
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("defaultCurrency", account.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("name", account.Name);
                    cmd.Parameters.AddWithValue("owner", account.Owner.oid);
                    AddBaseParameters(cmd, account);
                    cmd.Parameters.AddWithValue("oid", account.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
