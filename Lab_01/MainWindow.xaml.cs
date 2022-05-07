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
        String FileName;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void OpenRecept(object sender, RoutedEventArgs e)
        {
            switch (cmbSort.Text)
            {
                case "Фирменный":
                    FileName = @"D:\Projects\visualstudio_source\Resources\prod1.txt";
                    break;
                case "Бисквитно-фруктовый":
                    FileName = @"D:\Projects\visualstudio_source\Resources\prod2.txt";
                    break;
                case "Киевский":
                    FileName = @"D:\Projects\visualstudio_source\Resources\prod3.txt";
                    break;
                case "Бисквитный":
                    FileName = @"D:\Projects\visualstudio_source\Resources\prod4.txt";
                    break;
                case "С творожным кремом":
                    FileName = @"D:\Projects\visualstudio_source\Resources\prod5.txt";
                    break;
                default:
                    MessageBox.Show("Такого сорта нет");
                    break;
            }
            string[] RecArray = new string[20];
            List<Recept> sostav = new List<Recept>();

            using (StreamReader sr = new StreamReader(FileName))
            {

                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
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
}
