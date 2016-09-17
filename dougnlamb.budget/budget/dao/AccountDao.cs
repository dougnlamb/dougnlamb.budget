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
    public class AccountDao : IAccountDao {

        public IObservableList<IAccount> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IAccount Retrieve(ISecurityContext securityContext, int oid) {
            IAccount account = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        createdBy, 
                                        createdDate, 
                                        defaultCurrency, 
                                        name, 
                                        owner, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.account where oid = @oid";
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
                String query = @"select oid, 
                                        createdBy, 
                                        createdDate, 
                                        defaultCurrency, 
                                        name, 
                                        owner, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.account where owner = @owner";
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
            account.oid = (int)reader["oid"];
            account.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            account.CreatedDate = (DateTime)reader["createdDate"];
            int currencyId = (int)reader["defaultCurrency"];
            if (currencyId > 0) {
                account.DefaultCurrency = new Currency(securityContext, currencyId);
            }
            account.Name = (string)reader["name"];
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                account.UpdatedBy = new User(securityContext, updatedById);
            }
            account.UpdatedDate = reader["updatedDate"] != DBNull.Value ? (DateTime)reader["updatedDate"] : new DateTime();
            int ownerId = reader["owner"] != DBNull.Value ? (int)reader["owner"] : 0;
            if (ownerId > 0) {
                account.Owner = new User(securityContext, ownerId);
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
                    cmd.Parameters.AddWithValue("createdBy", account?.CreatedBy?.oid ?? 0);
                    cmd.Parameters.AddWithValue("createdDate", account.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", account.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("name", account.Name);
                    cmd.Parameters.AddWithValue("owner", account.Owner.oid);
                    cmd.Parameters.AddWithValue("updatedBy", account?.UpdatedBy?.oid ?? 0);
                    cmd.Parameters.AddWithValue("updatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("oid", account.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
