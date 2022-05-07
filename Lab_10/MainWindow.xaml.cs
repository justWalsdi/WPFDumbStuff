using System;
using System.Windows;
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
            var database = "Lab_07";                //Your DB
            string ConnectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=True";


            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand("SELECT NameSyrie, SV, SrokHran FROM Sysie;", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                if (reader.HasRows)
                    while (reader.Read())
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetString(0), reader.GetDouble(1), reader.GetInt32(2));

                else
                    Console.WriteLine("No rows found.");
                reader.Close();
            }
            MessageBox.Show("Завершено. Результат см. в консоли.");
        }
    }
}
