using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;
using System.Data.SqlClient;

namespace Lab_09
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Содержит инфу из файла при старте программы.
        public List<CsvDataClass> csvInfo = new List<CsvDataClass>();
        public MainWindow()
        {
            InitializeComponent();
            startingMethod();
        }
        public void startingMethod()
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string FileName = @".\..\..\..\Resources\Pran3.txt";
            
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] FileData = sr.ReadLine().Split(',');
                    csvInfo.Add(new CsvDataClass() { NameData = FileData[0], RashodValue = Convert.ToDouble(FileData[1]) });
                }
            CsvDataInput.ItemsSource = csvInfo;
        }
        public void ZagruzkaCSV(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Отправить информацию на сервер?",
                   "Отправка данных на сервер",
                   MessageBoxButton.YesNo,
                   MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (CsvDataClass data in csvInfo) pushToSQL(data);
                MessageBox.Show("Данные отправлены!");
            }
        }
        public void pushToSQL(CsvDataClass sendToSql)
        {

            var datasource = @"HOME-PC\SQLEXPRESS"; //Your Server name
            var database = "Lab_09";                //Your DB
            string ConnectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Recipe (Name, Rashod) VALUES (@name, @rashod)";

                using (SqlCommand quarySendString = new SqlCommand(query))
                {
                    quarySendString.Connection = conn;
                    conn.Open();

                    quarySendString.Parameters.Add("@name", System.Data.SqlDbType.NVarChar, 300).Value = sendToSql.NameData;
                    quarySendString.Parameters.Add("@rashod", System.Data.SqlDbType.Float).Value = sendToSql.RashodValue;

                    quarySendString.ExecuteNonQuery();
                }
            }
        }
    }
}
