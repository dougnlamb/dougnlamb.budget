using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace dougnlamb.budget.dao {
    public class UserDao : IUserDao {
        public IPagedList<IUser> Find(IUser user, string name) {
            throw new NotImplementedException();
        }

        public IPagedList<IUser> Find(ISecurityContext securityContext, string name) {
            throw new NotImplementedException();
        }

        public IUser Retrieve(ISecurityContext securityContext, int oid) {
            IUser usr = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand("select * from budget.dbo.[user] where oid = @oid", sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (usr == null) {
                                usr = BuildUser(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return usr;
        }

        private IUser BuildUser(SqlDataReader reader, ISecurityContext securityContext) {
            User usr = new User(securityContext);
            usr.oid = (int)reader["oid"];
            usr.CreatedBy = new User(securityContext, (int)reader["createdBy"]);
            usr.CreatedDate = (DateTime)reader["createdDate"];
            int currencyId = (int)reader["defaultCurrency"];
            if (currencyId > 0) {
                usr.DefaultCurrency = new Currency(securityContext, currencyId);
            }
            usr.DisplayName = (string)reader["displayName"];
            usr.Email = (string)reader["email"];
            int updatedById = reader["updatedBy"] != DBNull.Value ? (int)reader["updatedBy"] : 0;
            if (updatedById > 0) {
                usr.UpdatedBy = new User(securityContext, updatedById);
            }
            usr.UpdatedDate = reader["updatedDate"] != DBNull.Value ? (DateTime)reader["updatedDate"] : new DateTime();
            usr.UserId = (string)reader["userId"];

            return usr;
        }

        public int Save(ISecurityContext securityContext, IUser user) {
            if (user.oid == 0) {
                return InsertUser(user);
            }
            else {
                MockDatabase.UpdateUser(user);
                return user.oid;
            }
        }

        private int InsertUser(IUser user) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"insert into budget.dbo.[user] 
                        (createdBy, createdDate, defaultCurrency, displayName, email, updatedBy, updatedDate, userId)
                        values
                        (@createdBy, @createdDate, @defaultCurrency, @displayName, @email, @updatedBy, @updatedDate, @userId);
                        SET @ID=SCOPE_IDENTITY()",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("createdBy", user?.CreatedBy?.oid ?? 0);
                    cmd.Parameters.AddWithValue("createdDate", user.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", user.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("displayName", user.DisplayName);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("updatedBy", 0);
                    cmd.Parameters.AddWithValue("updatedDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("userId", user.UserId);

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

        private void UpdateUser(IUser user) {
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                using (SqlCommand cmd = new SqlCommand(@"update budget.dbo.[user] 
                        set createdBy = @createdBy,
                            createdDate = @createdDate,
                            defaultCurrency = @defaultCurrency,
                            displayName = @displayName,
                            email = @email,
                            updatedBy = @updatedBy,
                            updatedDate = @updatedDate,
                            userId = @userId
                        where
                            oid = @oid",
                    sqlConn)) {
                    cmd.Parameters.AddWithValue("createdBy", user?.CreatedBy?.oid ?? 0);
                    cmd.Parameters.AddWithValue("createdDate", user.CreatedDate);
                    cmd.Parameters.AddWithValue("defaultCurrency", user.DefaultCurrency.oid);
                    cmd.Parameters.AddWithValue("displayName", user.DisplayName);
                    cmd.Parameters.AddWithValue("email", user.Email);
                    cmd.Parameters.AddWithValue("updatedBy", user?.UpdatedBy?.oid ?? 0);
                    cmd.Parameters.AddWithValue("updatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("userId", user.UserId);
                    cmd.Parameters.AddWithValue("oid", user.oid);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
