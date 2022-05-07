using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace Lab_12
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


            var Name = new List<string>();
            var SV = new List<double>();
            var SrokHran = new List<int>();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand(
                  "SELECT NameSyrie, SV, SrokHran FROM Sysie;",
                  connection);
                connection.Open();
                List<Class1> spisok = new List<Class1>();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    int i = 0;
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetString(0),
                        reader.GetDouble(1),
                        reader.GetInt32(2));
                        Name.Add(reader.GetString(0));
                        SV.Add(reader.GetDouble(1));
                        SrokHran.Add(reader.GetInt32(2));

                        spisok.Add(new Class1() { Nam = Name[i], Suh = SV[i], Srok = SrokHran[i] });
                        i++;
                    }
                }
                else 
                    Console.WriteLine("No rows found.");
                reader.Close();

                dgGrid.ItemsSource = spisok;
            }
            MessageBox.Show($"Завершено. Результат см. в консоли. Сухие вещества:{SV[0]}" + SrokHran[4]);
        }
    }
}
