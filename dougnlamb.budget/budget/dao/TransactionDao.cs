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
    public class TransactionDao : BaseDao, ITransactionDao {

        public ITransaction Retrieve(ISecurityContext securityContext, int oid) {
            ITransaction transaction = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        note, 
                                        account,
                                        transactionAmount,
                                        currency,
                                        allocatedAmount,
                                        allocatedCurrency,
                                        transactionDate,
                                        reportedBy,
                                        reportedDate,
                                        isAllocated,
                                        isCleared,
                                        clearedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.[transaction] where oid = @oid";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (transaction == null) {
                                transaction = BuildTransaction(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return transaction;
        }

        public IObservableList<ITransaction> Retrieve(ISecurityContext securityContext, IBudget budget) {
            IObservableList<ITransaction> transactions = new ObservableList<ITransaction>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        note, 
                                        account,
                                        transactionAmount,
                                        currency,
                                        allocatedAmount,
                                        allocatedCurrency,
                                        transactionDate,
                                        reportedBy,
                                        reportedDate,
                                        isAllocated,
                                        isCleared,
                                        clearedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.[transaction] where budget = @budget";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("budget", budget.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            transactions.Add(BuildTransaction(reader, securityContext));
                        }
                    }
                }

            }
            return transactions;
        }

        public IObservableList<ITransaction> RetrieveOpen(ISecurityContext securityContext, IBudget budget) {
            IObservableList<ITransaction> transactions = new ObservableList<ITransaction>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        note, 
                                        account,
                                        transactionAmount,
                                        currency,
                                        allocatedAmount,
                                        allocatedCurrency,
                                        transactionDate,
                                        reportedBy,
                                        reportedDate,
                                        isAllocated,
                                        isCleared,
                                        clearedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.[transaction] where budget = @budget and (isclosed = 0 or isclosed is null)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("budget", budget.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            transactions.Add(BuildTransaction(reader, securityContext));
                        }
                    }
                }

            }
            return transactions;
        }

        private ITransaction BuildTransaction(SqlDataReader reader, ISecurityContext securityContext) {
            Transaction transaction = new Transaction(securityContext);
            transaction.oid = (int)reader["oid"];
            transaction.Note = (string)reader["note"];
            int accountId = (int)reader["account"];
            if (accountId > 0) {
                transaction.Account = new Account(securityContext, accountId);
            }
            transaction.TransactionAmount = BuildMoney(reader, "transactionAmount", "currency");
            transaction.AllocatedAmount = BuildMoney(reader, "allocatedAmount", "allocatedCurrency");
            transaction.TransactionDate = GetDateTime(reader, "transactionDate");
            transaction.ReportedBy = new User(securityContext, (int)reader["reportedBy"]);
            transaction.ReportedDate = GetDateTime(reader, "reportedDate");
            transaction.IsAllocated = (bool)reader["isAllocated"];
            transaction.IsCleared = (bool)reader["isCleared"];
            transaction.ClearedDate = GetDateTime(reader, "clearedDate");
            transaction.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            transaction.CreatedDate = GetDateTime(reader, "createdDate");
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                transaction.UpdatedBy = new User(securityContext, updatedById);
            }
            transaction.UpdatedDate = GetDateTime(reader, "updatedDate");

            return transaction;
        }

        public int Save(ISecurityContext securityContext, ITransaction transaction) {
            if (transaction.oid == 0) {
                return InsertTransaction(transaction);
            }
            else {
                UpdateTransaction(transaction);
                return transaction.oid;
            }
        }

        private int InsertTransaction(ITransaction transaction) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.[transaction] 
                        (note, account, transactionAmount, currency, allocatedAmount, allocatedCurrency, isAllocated, transactionDate, reportedBy, reportedDate, isCleared, clearedDate, createdBy, createdDate, updatedBy, updatedDate)
                        values
                        (@note, @account, @transactionAmount, @currency, @allocatedAmount, @allocatedCurrency, @isAllocated, @transactionDate, @reportedBy, @reportedDate, @isCleared, @clearedDate, @createdBy, @createdDate, @updatedBy, @updatedDate)
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("note", transaction?.Note ?? "");
                    cmd.Parameters.AddWithValue("account", transaction.Account.oid);
                    AddMoneyParameter(cmd, transaction.TransactionAmount, "transactionAmount", "currency");
                    AddMoneyParameter(cmd, transaction.AllocatedAmount, "allocatedAmount", "allocatedCurrency");
                    AddDateParameter(cmd, "transactionDate", transaction.TransactionDate);
                    cmd.Parameters.AddWithValue("reportedBy", transaction.ReportedBy?.oid ?? 0);
                    AddDateParameter(cmd, "reportedDate", transaction.ReportedDate);
                    cmd.Parameters.AddWithValue("isallocated", transaction.IsAllocated);
                    cmd.Parameters.AddWithValue("iscleared", transaction.IsCleared);
                    AddDateParameter(cmd, "clearedDate", transaction.ClearedDate);

                    AddBaseParameters(cmd, transaction);

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

        private void UpdateTransaction(ITransaction transaction) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.transaction 
                        set note = @note, 
                            account = @account,
                            transactionAmount = @transactionAmount
                            currency = @currency
                            allocatedAmount = @allocatedAmount,
                            allocatedCurrency = @allocatedCurrency,
                            isAllocated = @isAllocated,
                            transactionDate = @transactionDate,
                            reportedBy = @reportedBy,
                            reportedDate = @reportedDate,
                            isCleared = @isClosed,
                            clearedDate = @clearedDate,
                            createdBy = @createdBy, 
                            createdDate = @createdDate, 
                            updatedBy = @updatedBy, 
                            updatedDate = @updatedDate 
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("note", transaction.Note);
                    cmd.Parameters.AddWithValue("account", transaction.Account.oid);
                    AddMoneyParameter(cmd, transaction.TransactionAmount, "transactionAmount", "currency");
                    AddMoneyParameter(cmd, transaction.AllocatedAmount, "allocatedAmount", "allocatedCurrency");
                    AddDateParameter(cmd, "transactionDate", transaction.TransactionDate);
                    cmd.Parameters.AddWithValue("reportedBy", transaction.ReportedBy?.oid ?? 0);
                    AddDateParameter(cmd, "reportedDate", transaction.ReportedDate);
                    cmd.Parameters.AddWithValue("isallocated", transaction.IsAllocated);
                    cmd.Parameters.AddWithValue("iscleared", transaction.IsCleared);
                    AddDateParameter(cmd, "clearedDate", transaction.ClearedDate);

                    AddBaseParameters(cmd, transaction);

                    cmd.Parameters.AddWithValue("oid", transaction.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IObservableList<ITransaction> RetrieveOpen(ISecurityContext mSecurityContext, IUser user) {
            throw new NotImplementedException();
        }
    }
}
