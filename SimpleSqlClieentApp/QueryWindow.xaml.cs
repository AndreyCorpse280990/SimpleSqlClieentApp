using SimpleSqlClieentApp.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleSqlClieentApp
{
    /// <summary>
    /// Interaction logic for QueryWindow.xaml
    /// </summary>
    public partial class QueryWindow : Window
    {
        private readonly QueryExecutor _executor;
        public QueryWindow(QueryExecutor executor)
        {
            InitializeComponent();
            //
            _executor = executor;
        }

        private void executeButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = queryTextBox.Text;
                DataTable queryResult = _executor.ExecuteQuery(query);
                resultDataGrid.ItemsSource = queryResult.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
