using System;
using System.Windows;
using System.Collections.Generic;
using System.IO;

namespace Lab_01
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow() => InitializeComponent();
        private void OpenRecept(object sender, RoutedEventArgs e)
        {
            string FileName = "";
            switch (cmbSort.Text)
            {
                case "П1":
                    FileName = @".\..\..\..\Resources\prod1.txt";
                    break;
                case "П2":
                    FileName = @".\..\..\..\Resources\prod2.txt";
                    break;
                case "П3":
                    FileName = @".\..\..\..\Resources\prod3.txt";
                    break;
                case "П4":
                    FileName = @".\..\..\..\Resources\prod4.txt";
                    break;
                case "П5":
                    FileName = @".\..\..\..\Resources\prod5.txt";
                    break;
                default:
                    MessageBox.Show("Такого сорта нет");
                    break;
            }
            List<Recept> sostav = new List<Recept>();

            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    try
                    {
                        sostav.Add(new Recept() { NameSyrie = RecArray[0], Rashod = Convert.ToDouble(RecArray[1]) });
                    }
                    catch(Exception exception)
                    {
                        MessageBox.Show("Ошибка чтения файла. Ошибка: " + exception);
                    }
                }
            dgTable.ItemsSource = sostav;
        }
    }
}
