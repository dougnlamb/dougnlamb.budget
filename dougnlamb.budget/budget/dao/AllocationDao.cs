using System;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Data;

namespace dougnlamb.budget.dao {
    public class AllocationDao : BaseDao, IAllocationDao {

        public IAllocation Retrieve(ISecurityContext securityContext, int oid) {
            IAllocation allocation = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        notes, 
                                        [transaction],
                                        budgetItem,
                                        amount,
                                        currency,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.allocation where oid = @oid";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (allocation == null) {
                                allocation = BuildAllocation(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return allocation;
        }

        public IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, IBudgetItem budgetItem) {
            IObservableList<IAllocation> allocations = new ObservableList<IAllocation>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        notes, 
                                        [transaction],
                                        budgetItem,
                                        amount,
                                        currency,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.allocation where budgetitem = @budgetitem";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("budgetitem", budgetItem.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            allocations.Add(BuildAllocation(reader, securityContext));
                        }
                    }
                }

            }
            return allocations;
        }

        public IObservableList<IAllocation> Retrieve(ISecurityContext securityContext, ITransaction transaction) {
            IObservableList<IAllocation> allocations = new ObservableList<IAllocation>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        notes, 
                                        [transaction],
                                        budgetitem,
                                        amount,
                                        currency,
                                        createdBy, 
                                        createdDate, 
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.allocation where [transaction] = @transaction";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("transaction", transaction.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            allocations.Add(BuildAllocation(reader, securityContext));
                        }
                    }
                }

            }
            return allocations;
        }

        private IAllocation BuildAllocation(SqlDataReader reader, ISecurityContext securityContext) {
            Allocation allocation = new Allocation(securityContext);
            allocation.oid = (int)reader["oid"];
            allocation.Notes = (string)reader["notes"];
            allocation.Amount = BuildMoney(reader, "amount", "currency");
            allocation.BudgetItem = new BudgetItem(securityContext, (int)reader["budgetitem"]);
            allocation.Transaction = new Transaction(securityContext, (int)reader["transaction"]);
            allocation.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            allocation.CreatedDate = GetDateTime(reader, "createdDate");
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                allocation.UpdatedBy = new User(securityContext, updatedById);
            }
            allocation.UpdatedDate = GetDateTime(reader, "updatedDate");

            return allocation;
        }

        public int Save(ISecurityContext securityContext, IAllocation allocation) {
            if (allocation.oid == 0) {
                return InsertAllocation(allocation);
            }
            else {
                UpdateAllocation(allocation);
                return allocation.oid;
            }
        }

        private int InsertAllocation(IAllocation allocation) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.allocation 
                        (notes, [transaction], budgetitem, amount, currency, createdBy, createdDate, updatedBy, updatedDate)
                        values
                        (@notes, @transaction, @budgetitem, @amount, @currency, @createdBy, @createdDate, @updatedBy, @updatedDate)
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("notes", allocation.Notes);
                    cmd.Parameters.AddWithValue("transaction", allocation.Transaction.oid);
                    cmd.Parameters.AddWithValue("budgetItem", allocation.BudgetItem?.oid ?? 0);
                    cmd.Parameters.AddWithValue("amount", allocation.Amount?.Value ?? 0);
                    cmd.Parameters.AddWithValue("currency", allocation.Amount?.Currency?.oid ?? 0);
                    cmd.Parameters.AddWithValue("createdBy", allocation.CreatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "createdDate", allocation.CreatedDate);
                    cmd.Parameters.AddWithValue("updatedBy", allocation.UpdatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "updatedDate", allocation.UpdatedDate);

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

        private void UpdateAllocation(IAllocation allocation) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.allocation 
                        set notes = @notes,
                            [transaction] = @transaction,
                            budgetitem = @budgetitem,
                            amount = @amount,
                            currency = @currency,
                            createdBy = @createdBy, 
                            createdDate = @createdDate, 
                            updatedBy = @updatedBy, 
                            updatedDate = @updatedDate  
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("notes", allocation.Notes);
                    cmd.Parameters.AddWithValue("transaction", allocation.Transaction.oid);
                    cmd.Parameters.AddWithValue("budgetItem", allocation.BudgetItem?.oid ?? 0);
                    cmd.Parameters.AddWithValue("amount", allocation.Amount?.Value ?? 0);
                    cmd.Parameters.AddWithValue("currency", allocation.Amount?.Currency?.oid ?? 0);
                    cmd.Parameters.AddWithValue("createdBy", allocation.CreatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "createdDate", allocation.CreatedDate);
                    cmd.Parameters.AddWithValue("updatedBy", allocation.UpdatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "updatedDate", allocation.UpdatedDate);

                    cmd.Parameters.AddWithValue("oid", allocation.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
