using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Configuration;

namespace dougnlamb.budget.dao {
    public class CurrencyDao : ICurrencyDao {
        public ICurrency Retrieve(ISecurityContext securityContext, int oid) {
            ICurrency currency = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid, 
                                        code, 
                                        description 
                                    from budget.dbo.currency where oid = @oid";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    cmd.Parameters.AddWithValue("oid", oid);
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            if (currency == null) {
                                currency = BuildCurrency(reader, securityContext);
                            }
                            else {
                                throw new Exception("Too many records found.");
                            }
                        }
                    }
                }

            }
            return currency;
        }

        private ICurrency BuildCurrency(SqlDataReader reader, ISecurityContext securityContext) {
            Currency usr = new Currency(securityContext);
            usr.oid = (int)reader["oid"];
            usr.Code = (string)reader["code"];
            usr.Description = (string)reader["description"];

            return usr;
        }

        private string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }
    }
}
