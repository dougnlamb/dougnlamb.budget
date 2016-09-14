using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.collections;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Configuration;

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
                using (SqlCommand cmd = new SqlCommand($"select * from budget.dbo.[user] where oid = {oid}", sqlConn)) {
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
                return MockDatabase.InsertUser(user);
            }
            else {
                MockDatabase.UpdateUser(user);
                return user.oid;
            }
        }

        private string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
