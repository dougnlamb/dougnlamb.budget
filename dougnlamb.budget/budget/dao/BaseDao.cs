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
using dougnlamb.core;

namespace dougnlamb.budget.dao {
    public abstract class BaseDao {

        protected DateTime GetDateTime(SqlDataReader reader, string column) {
            return reader[column] != DBNull.Value ? (DateTime)reader[column] : new DateTime();
        }

        protected IMoney GetMoney(SqlDataReader reader, string valueColumn, string currencyColumn) {
            decimal value = 0;
            if(reader[valueColumn] != DBNull.Value) {
                value = (decimal)reader[valueColumn];
            }
            Money money = new Money() {
                Value = value,
                Currency = new Currency(null, (int)reader[currencyColumn])
            };
            return money;
        }

        protected void AddDateParameter(SqlCommand cmd, string parameter, DateTime value) {
            if (value == DateTime.MinValue) {
                cmd.Parameters.AddWithValue(parameter, DBNull.Value);
            }
            else {
                cmd.Parameters.AddWithValue(parameter, value);
            }
        }

        protected void AddMoneyParameter(SqlCommand cmd, IMoney money, string valueColumn, string currencyColumn) {
            cmd.Parameters.AddWithValue(valueColumn, money?.Value ?? 0);
            cmd.Parameters.AddWithValue(currencyColumn, money?.Currency?.oid ?? 0);
        }

        protected void AddBaseParameters(SqlCommand cmd, IBaseObject baseObject) {
            cmd.Parameters.AddWithValue("createdBy", baseObject.CreatedBy?.oid ?? 0);
            AddDateParameter(cmd, "createdDate", baseObject.CreatedDate);
            cmd.Parameters.AddWithValue("updatedBy", baseObject.UpdatedBy?.oid ?? 0);
            AddDateParameter(cmd, "updatedDate", baseObject.UpdatedDate);
        }

        protected string GetConnectionString() {
            return ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

    }
}
