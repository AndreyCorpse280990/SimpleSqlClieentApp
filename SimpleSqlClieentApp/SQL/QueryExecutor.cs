using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSqlClieentApp.SQL
{
    public class QueryExecutor : IDisposable
    {
        // объект подключения, через который будут выполняться запросы
        private readonly SqlConnection _connection;

        // конструктор
        public QueryExecutor(string sqlServer, string database, int timeoutSec=5)
        {
            string connectionString = $"Data Source={sqlServer}; Initial Catalog={database}; Integrated Security=SSPI; Timeout={timeoutSec};";
            _connection = new SqlConnection(connectionString);
            _connection.Open();
        }

        // метод выполнения запроса
        // вход: строка с текстом запроса
        // выход: результат запроса в табличном виде
        public DataTable ExecuteQuery(string query)
        {
            if (IsSelectQuery(query))
            {
                return ExecuteSelect(query);
            }
            return ExecuteNonSelect(query); 
        }

        // вспомогательные методы для выполнения SELECT запроса и INSERT/UPDATE/DELETE запросов
        private DataTable ExecuteSelect(string query)
        {
            DataTable resultTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(resultTable);
                }
            }
            return resultTable;
        }


        private DataTable ExecuteNonSelect(string query)
        {
            DataTable resultTable = new DataTable();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                int rowsAffected = command.ExecuteNonQuery();
                resultTable.Columns.Add("RowsAffected");
                resultTable.Rows.Add(rowsAffected);
            }
            return resultTable;
        }

        private bool IsSelectQuery(string query) {
            // TODO: реализовать метод
            return query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase);
        }

        // финализатор
        public void Dispose()
        {
            _connection.Close();
        }
    }
}
