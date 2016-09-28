using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dougnlamb.core.security;
using System.Data.SqlClient;
using System.Configuration;

namespace dougnlamb.budget.dao {
    public class CurrencyDao : BaseDao, ICurrencyDao {
        public ICurrency Retrieve(ISecurityContext securityContext, int oid) {
            ICurrency currency = null;
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid currency_oid,
                                        code currency_code, 
                                        description currency_description 
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

        public IList<ICurrency> RetrieveAll(ISecurityContext securityContext) {
            IList<ICurrency> currencies = new List<ICurrency>();
            using (SqlConnection sqlConn = new SqlConnection(GetConnectionString())) {
                sqlConn.Open();
                String query = @"select oid currency_oid,
                                        code currency_code, 
                                        description currency_description 
                                    from budget.dbo.currency";
                using (SqlCommand cmd = new SqlCommand(query, sqlConn)) {
                    using (SqlDataReader reader = cmd.ExecuteReader()) {
                        while (reader.Read()) {
                            currencies.Add(BuildCurrency(reader, securityContext));
                        }
                    }
                }

            }
            return currencies;
        }

        public ICurrency BuildCurrency(SqlDataReader reader, ISecurityContext securityContext) {
            Currency usr = new Currency(securityContext);
            usr.oid = (int)reader["currency_oid"];
            usr.Code = (string)reader["currency_code"];
            usr.Description = (string)reader["currency_description"];

            return usr;
        }
    }
}
