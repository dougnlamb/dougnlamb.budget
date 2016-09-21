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
    public class BudgetDao : BaseDao, IBudgetDao {

        public IObservableList<IBudget> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IBudget Retrieve(ISecurityContext securityContext, int oid) {
            IBudget budget = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        createdBy, 
                                        createdDate, 
                                        defaultCurrency, 
                                        name, 
                                        owner, 
                                        isclosed,
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.budget where oid = @oid
                                        order by name";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (budget == null) {
                                budget = BuildBudget(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return budget;
        }

        public IObservableList<IBudget> Retrieve(ISecurityContext securityContext, IUser user) {
            IObservableList<IBudget> budgets = new ObservableList<IBudget>();

            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        createdBy, 
                                        createdDate, 
                                        defaultCurrency, 
                                        name, 
                                        owner, 
                                        isclosed,
                                        updatedBy, 
                                        updatedDate  
                                    from budget.dbo.budget where owner = @owner";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("owner", user.oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            budgets.Add(BuildBudget(reader, securityContext));
                        }
                    }
                }

            }
            return budgets;
        }

        private IBudget BuildBudget(SqlDataReader reader, ISecurityContext securityContext) {
            Budget budget = new Budget(securityContext);
            budget.oid = (int)reader["oid"];
            budget.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            budget.CreatedDate = GetDateTime(reader, "createdDate");
            int currencyId = (int)reader["defaultCurrency"];
            if (currencyId > 0) {
                budget.DefaultCurrency = new Currency(securityContext, currencyId);
            }
            budget.Name = (string)reader["name"];
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                budget.UpdatedBy = new User(securityContext, updatedById);
            }
            budget.UpdatedDate = GetDateTime(reader, "updatedDate");
            int ownerId = reader["owner"] != DBNull.Value ? (int)reader["owner"] : 0;
            if (ownerId > 0) {
                budget.Owner = new User(securityContext, ownerId);
            }
            budget.IsClosed = (bool)reader["isclosed"];

            return budget;
        }

        public int Save(ISecurityContext securityContext, IBudget budget) {
            if (budget.oid == 0) {
                return InsertBudget(budget);
            }
            else {
                UpdateBudget(budget);
                return budget.oid;
            }
        }

        private int InsertBudget(IBudget budget) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.budget 
                        (createdBy, createdDate, defaultCurrency, name, owner, isclosed, updatedBy, updatedDate)
                        values
                        (@createdBy, @createdDate, @defaultCurrency, @name, @owner, @isclosed, @updatedBy, @updatedDate);
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("createdBy", budget.CreatedBy.oid);
                    AddDateParameter(cmd, "createdDate", budget.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", budget.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("name", budget.Name);
                    cmd.Parameters.AddWithValue("owner", budget.Owner.oid);
                    cmd.Parameters.AddWithValue("isclosed", budget.IsClosed);
                    cmd.Parameters.AddWithValue("updatedBy", 0);
                    AddDateParameter(cmd, "updatedDate", budget.CreatedDate);

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

        private void UpdateBudget(IBudget budget) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.budget 
                        set createdBy = @createdBy,
                            createdDate = @createdDate,
                            defaultCurrency = @defaultCurrency,
                            name = @name,
                            owner = @owner,
                            isclosed = @isclosed,
                            updatedBy = @updatedBy,
                            updatedDate = @updatedDate
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("createdBy", budget?.CreatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "createdDate", budget.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", budget.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("name", budget.Name);
                    cmd.Parameters.AddWithValue("owner", budget.Owner.oid);
                    cmd.Parameters.AddWithValue("isclosed", budget.IsClosed);
                    cmd.Parameters.AddWithValue("updatedBy", budget?.UpdatedBy?.oid ?? 0);
                    AddDateParameter(cmd, "updatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("oid", budget.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
