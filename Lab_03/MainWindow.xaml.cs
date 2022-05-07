using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Data;

namespace Lab_03
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow() => InitializeComponent();
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //чтение матрицы
            List<Matrix> matr = new List<Matrix>();
            using (StreamReader sr = new StreamReader(@"D:\Projects\visualstudio_source\Resources\ProdRecept.txt"))
            {
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    matr.Add(new Matrix() { NameSyrie = RecArray[0], Rashod1 = RecArray[1], Rashod2 = RecArray[2], Rashod3 = RecArray[3], Rashod4 = RecArray[4], Rashod5 = RecArray[5] });
                }

                lvUsers.ItemsSource = matr;

                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(lvUsers.ItemsSource);
                view.SortDescriptions.Add(new SortDescription("NameSyrie", ListSortDirection.Ascending));
            }

            using (StreamWriter writer = new StreamWriter(@"D:\Projects\visualstudio_source\Resources\ProdReceptNew.txt"))
                for (int i = 0; i < matr.Count; i++)
                    writer.WriteLine(matr[i].NameSyrie + "," + matr[i].Rashod1 + "," + matr[i].Rashod2 + "," + matr[i].Rashod3 + "," + matr[i].Rashod4 + "," + matr[i].Rashod5);
            MessageBox.Show("Матрица выведена в файл");
        }

        public class Matrix
        {
            public string NameSyrie { get; set; }
            public string Rashod1 { get; set; }
            public string Rashod2 { get; set; }
            public string Rashod3 { get; set; }
            public string Rashod4 { get; set; }
            public string Rashod5 { get; set; }
        }
    }
}