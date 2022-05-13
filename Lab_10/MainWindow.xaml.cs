using System;
using System.Windows;
using System.Diagnostics;
using System.Data.SqlClient;

namespace Lab_10
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Open(object sender, RoutedEventArgs e)
        {
            var datasource = @"HOME-PC\SQLEXPRESS"; //Your Server name
            var database = "Lab_10";                //Your DB
            string ConnectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT NameSyrie, SV, SrokHran FROM Chocolad;", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                    while (reader.Read())
                        Trace.WriteLine($"{reader.GetString(0)}\t{reader.GetDouble(1)}\t{reader.GetInt32(2)}");

                else
                    Trace.WriteLine("No rows found.");
                reader.Close();
            }
            MessageBox.Show("Завершено. Результат см. в консоли.");
        }
    }
}
