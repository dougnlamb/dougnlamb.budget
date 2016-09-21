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
    public class BudgetItemDao : BaseDao, IBudgetItemDao {

        public IBudgetItem Retrieve(ISecurityContext securityContext, int oid) {
            IBudgetItem budgetItem = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        name, 
                                        budget,
                                        defaultAccount,
                                        budgetAmount,
                                        currency,
                                        balance,
                                        notes,
                                        reminderDate,
                                        dueDate,
                                        isClosed,
                                        closedBy,
                                        closedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.budgetitem where oid = @oid";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (budgetItem == null) {
                                budgetItem = BuildBudgetItem(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return budgetItem;
        }

        public IObservableList<IBudgetItem> Retrieve(ISecurityContext securityContext, IBudget budget) {
            IObservableList<IBudgetItem> budgetItems = new ObservableList<IBudgetItem>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        name, 
                                        budget,
                                        defaultAccount,
                                        budgetAmount,
                                        currency,
                                        balance,
                                        notes,
                                        reminderDate,
                                        dueDate,
                                        isClosed,
                                        closedBy,
                                        closedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.budgetitem where budget = @budget";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("budget", budget.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            budgetItems.Add(BuildBudgetItem(reader, securityContext));
                        }
                    }
                }

            }
            return budgetItems;
        }

        public IObservableList<IBudgetItem> RetrieveOpen(ISecurityContext securityContext, IBudget budget) {
            IObservableList<IBudgetItem> budgetItems = new ObservableList<IBudgetItem>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        name, 
                                        budget,
                                        defaultAccount,
                                        budgetAmount,
                                        currency,
                                        balance,
                                        notes,
                                        reminderDate,
                                        dueDate,
                                        isClosed,
                                        closedBy,
                                        closedDate,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.budgetitem where budget = @budget and (isclosed = 0 or isclosed is null)";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("budget", budget.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            budgetItems.Add(BuildBudgetItem(reader, securityContext));
                        }
                    }
                }

            }
            return budgetItems;
        }

        private IBudgetItem BuildBudgetItem(SqlDataReader reader, ISecurityContext securityContext) {
            BudgetItem budgetItem = new BudgetItem(securityContext);
            budgetItem.oid = (int)reader["oid"];
            budgetItem.BudgetAmount = BuildMoney(reader, "budgetAmount", "currency");
            budgetItem.Balance = BuildMoney(reader, "balance", "currency");
            budgetItem.Budget = new Budget(securityContext, (int)reader["budget"]);
            budgetItem.ClosedBy = new User(securityContext, (int)reader["closedBy"]);
            budgetItem.ClosedDate = GetDateTime(reader, "closedDate");
            budgetItem.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            budgetItem.CreatedDate = GetDateTime(reader, "createdDate");
            int defaultAccountId = (int)reader["defaultAccount"];
            if (defaultAccountId > 0) {
                budgetItem.DefaultAccount = new Account(securityContext, defaultAccountId);
            }
            budgetItem.DueDate = GetDateTime(reader, "dueDate");
            budgetItem.IsClosed = (bool)reader["isClosed"];
            budgetItem.Name = (string)reader["name"];
            budgetItem.Notes = (string)reader["notes"];
            budgetItem.ReminderDate = GetDateTime(reader, "reminderDate");
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                budgetItem.UpdatedBy = new User(securityContext, updatedById);
            }
            budgetItem.UpdatedDate = GetDateTime(reader, "updatedDate");

            return budgetItem;
        }

        public int Save(ISecurityContext securityContext, IBudgetItem budgetItem) {
            if (budgetItem.oid == 0) {
                return InsertBudgetItem(budgetItem);
            }
            else {
                UpdateBudgetItem(budgetItem);
                return budgetItem.oid;
            }
        }

        private int InsertBudgetItem(IBudgetItem budgetItem) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.budgetitem 
                        (name, budget, defaultAccount, budgetAmount, currency, balance,  notes, reminderDate, dueDate, isClosed, closedBy, closedDate, createdBy, createdDate, updatedBy, updatedDate)
                        values
                        (@name, @budget, @defaultAccount, @budgetAmount, @currency, @balance, @notes, @reminderDate, @dueDate, @isClosed, @closedBy, @closedDate, @createdBy, @createdDate, @updatedBy, @updatedDate)
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("name", budgetItem.Name);
                    cmd.Parameters.AddWithValue("budget", budgetItem.Budget.oid);
                    cmd.Parameters.AddWithValue("defaultAccount", budgetItem.DefaultAccount?.oid ?? 0);
                    cmd.Parameters.AddWithValue("budgetAmount", budgetItem.BudgetAmount?.Value ?? 0);
                    cmd.Parameters.AddWithValue("currency", budgetItem.BudgetAmount?.Currency?.oid ?? 0);
                    cmd.Parameters.AddWithValue("balance", budgetItem.Balance?.Value ?? 0);
                    cmd.Parameters.AddWithValue("notes", budgetItem.Notes);
                    AddDateParameter(cmd, "reminderDate", budgetItem.ReminderDate);
                    AddDateParameter(cmd, "dueDate", budgetItem.DueDate);
                    cmd.Parameters.AddWithValue("isclosed", budgetItem.IsClosed);
                    cmd.Parameters.AddWithValue("closedBy", budgetItem.ClosedBy?.oid ?? 0);
                    AddDateParameter(cmd, "closedDate", budgetItem.ClosedDate);
                    cmd.Parameters.AddWithValue("createdBy", budgetItem.CreatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "createdDate", budgetItem.CreatedDate);
                    cmd.Parameters.AddWithValue("updatedBy", budgetItem.UpdatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "updatedDate", budgetItem.UpdatedDate);

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

        private void UpdateBudgetItem(IBudgetItem budgetItem) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.budgetitem 
                        set name = @name, 
                            budget = @budget,
                            defaultAccount = @defaultAccount,
                            budgetAmount = @budgetAmount,
                            currency = @currency,
                            balance = @balance,
                            notes = @notes,
                            reminderDate = @reminderDate,
                            dueDate = @dueDate,
                            isClosed = @isClosed,
                            closedBy = @closedBy,
                            closedDate = @closedDate,
                            createdBy = @createdBy, 
                            createdDate = @createdDate, 
                            updatedBy = @updatedBy, 
                            updatedDate = @updatedDate  
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("name", budgetItem.Name);
                    cmd.Parameters.AddWithValue("budget", budgetItem.Budget.oid);
                    cmd.Parameters.AddWithValue("defaultAccount", budgetItem.DefaultAccount?.oid ?? 0);
                    cmd.Parameters.AddWithValue("budgetAmount", budgetItem.BudgetAmount?.Value ?? 0);
                    cmd.Parameters.AddWithValue("currency", budgetItem.BudgetAmount?.Currency?.oid ?? 0);
                    cmd.Parameters.AddWithValue("balance", budgetItem.Balance?.Value ?? 0);
                    cmd.Parameters.AddWithValue("notes", budgetItem.Notes);
                    AddDateParameter(cmd, "reminderDate", budgetItem.ReminderDate);
                    AddDateParameter(cmd, "dueDate", budgetItem.DueDate);
                    cmd.Parameters.AddWithValue("isclosed", budgetItem.IsClosed);
                    cmd.Parameters.AddWithValue("closedBy", budgetItem.ClosedBy?.oid ?? 0);
                    AddDateParameter(cmd, "closedDate", budgetItem.ClosedDate);
                    cmd.Parameters.AddWithValue("createdBy", budgetItem.CreatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "createdDate", budgetItem.CreatedDate);
                    cmd.Parameters.AddWithValue("updatedBy", budgetItem.UpdatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "updatedDate", budgetItem.UpdatedDate);

                    cmd.Parameters.AddWithValue("oid", budgetItem.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IObservableList<IBudgetItem> RetrieveOpen(ISecurityContext mSecurityContext, IUser user) {
            throw new NotImplementedException();
        }
    }
}
