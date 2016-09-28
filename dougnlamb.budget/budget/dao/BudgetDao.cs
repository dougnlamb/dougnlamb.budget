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
                String query = @"select budget.oid budget_oid, 
                                        budget.createdBy budget_createdBy,
                                        budget.createdDate budget_createdDate,
                                        budget.defaultCurrency budget_defaultCurrency, 
                                        budget.name budget_name, 
                                        budget.owner budget_owner, 
                                        budget.isclosed budget_isclosed,
                                        budget.updatedBy budget_updatedBy, 
                                        budget.updatedDate budget_updatedDate,
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
                                    from budget.dbo.budget budget 
                                    left join budget.dbo.currency currency on budget.defaultCurrency = currency.oid
                                    left join budget.dbo.[user] owner on budget.owner = owner.oid
                                    left join budget.dbo.[user] creator on budget.createdBy = creator.oid
                                    left join budget.dbo.[user] updater on budget.updatedBy = updater.oid
                                        where oid = @oid";
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
                String query = @"select budget.oid budget_oid, 
                                        budget.createdBy budget_createdBy,
                                        budget.createdDate budget_createdDate,
                                        budget.defaultCurrency budget_defaultCurrency, 
                                        budget.name budget_name, 
                                        budget.owner budget_owner, 
                                        budget.isclosed budget_isclosed,
                                        budget.updatedBy budget_updatedBy, 
                                        budget.updatedDate budget_updatedDate,
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
                                    from budget.dbo.budget budget 
                                    left join budget.dbo.currency currency on budget.defaultCurrency = currency.oid
                                    left join budget.dbo.[user] owner on budget.owner = owner.oid
                                    left join budget.dbo.[user] creator on budget.createdBy = creator.oid
                                    left join budget.dbo.[user] updater on budget.updatedBy = updater.oid
                                        where owner = @owner
                                        order by budget_name";
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
            budget.oid = (int)reader["budget_oid"];
            budget.CreatedDate = GetDateTime(reader, "budget_createdDate");
            int currencyId = (int)reader["budget_defaultCurrency"];
            if (currencyId > 0) {
                budget.DefaultCurrency = new CurrencyDao().BuildCurrency(reader, securityContext);
            }
            budget.Name = (string)reader["budget_name"];

            int createdById = reader["budget_createdBy"] != DBNull.Value ? (int)reader["budget_createdBy"] : 0;
            if (createdById > 0) {
                budget.CreatedBy = new UserDao().BuildUser(reader, securityContext, "creator_");
            }
            int updatedById = reader["budget_updatedBy"] != DBNull.Value ? (int)reader["budget_updatedBy"] : 0;
            if (updatedById > 0) {
                budget.UpdatedBy = new UserDao().BuildUser(reader, securityContext, "updater_");
            }
            budget.UpdatedDate = GetDateTime(reader, "budget_updatedDate");
            int ownerId = reader["budget_owner"] != DBNull.Value ? (int)reader["budget_owner"] : 0;
            if (ownerId > 0) {
                budget.Owner = new UserDao().BuildUser(reader, securityContext, "owner_");
            }

            budget.IsClosed = (bool)reader["budget_isclosed"];

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
