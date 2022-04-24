using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;
using System.Data.SqlClient;



namespace Lab_07
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
        public void CSVtoSQL(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            string FileName = @"D:\Projects\visualstudio_source\Resources\Lab_07_csv.txt";

            string[] FileData = new string[30];

            List<CsvData> Data = new List<CsvData>();   
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    FileData = sr.ReadLine().Split(',');
                    CsvData temp = new CsvData() { NameData = FileData[0], Data1Value = Convert.ToDouble(FileData[1]), Data2Value = Convert.ToInt32(FileData[2]), Data3Value = Convert.ToInt32(FileData[3]) };
                    Data.Add(temp);
                }
            }


            CsvDataInput.ItemsSource = Data;

            if (MessageBox.Show("Отправить информацию на сервер?",
                    "Отправка данных на сервер",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (CsvData data in Data) pushToSQL(data);
                MessageBox.Show("Данные отправлены!");
            }

            //MessageBox.Show("Data send to Database!");
        }
        public void pushToSQL(CsvData sendToSql)
        {

            var datasource = @"HOME-PC\SQLEXPRESS"; //Your Server name
            var database = "Lab_07";                //Your DB
            string ConnectionString = @"Data Source=" + datasource + ";Initial Catalog=" + database + ";Integrated Security=True";

            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                string query = "INSERT INTO Table_01 (Name, Data1, Data2, Data3) VALUES (@Name, @Data1, @Data2, @Data3)";

                using (SqlCommand quarySendString = new SqlCommand(query))
                {
                    quarySendString.Connection = conn;
                    conn.Open();

                    quarySendString.Parameters.Add("@Name", System.Data.SqlDbType.NVarChar, 300).Value = sendToSql.NameData;
                    quarySendString.Parameters.Add("@Data1", System.Data.SqlDbType.Float).Value = sendToSql.Data1Value;
                    quarySendString.Parameters.Add("@Data2", System.Data.SqlDbType.Int).Value = sendToSql.Data2Value;
                    quarySendString.Parameters.Add("@Data3", System.Data.SqlDbType.Int).Value = sendToSql.Data3Value;

                    quarySendString.ExecuteNonQuery();
                }
            }
        }
    }
}
