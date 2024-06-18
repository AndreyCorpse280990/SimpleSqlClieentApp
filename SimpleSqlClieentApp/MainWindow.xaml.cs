using SimpleSqlClieentApp.SQL;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleSqlClieentApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //
            sqlServerTextBox.Text = @"HOME-PC\SQLEXPRESS";
            databaseTextBox.Text = "Academy_db";
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void connectionButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // создать и открыть соединение к БД, передав для работы в окно запросов
                string sqlServer = sqlServerTextBox.Text;
                string database = databaseTextBox.Text;
                using (QueryExecutor executor = new QueryExecutor(sqlServer, database))
                {
                    // скрыть текущее окно
                    Hide();
                    // создать и открыть в модальном режиме окно для работы с БД, передав соединение
                    QueryWindow queryWindow = new QueryWindow(executor);
                    queryWindow.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } finally
            {
                // открыть текущее
                Show();
            }
        }
    }
}
