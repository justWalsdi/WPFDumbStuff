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

        private void Matrix(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ////читаем содержание файла
            string FileName = @"D:\Projects\visualstudio_source\Resources\prod1.txt";
            string[] RecArray;
            string[] Srie = new string[100];
            double[] Rshd = new double[100];
            int KolRec = 0;

            string[] Syrie = new string[100];
            double[,] Rashod = new double[100, 20];

            List<Recept> recept = new List<Recept>();

            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    Srie[i] = RecArray[0];
                    Rshd[i] = Double.Parse(RecArray[1]);
                    Syrie[i] = Srie[i];
                    Rashod[i, 0] = Rshd[i];
                    recept.Add(new Recept() { NameSyrie = Srie[i], Rashod = Rshd[i] });
                    i++;

                    KolRec = i;
                }
            }
            dgTable.ItemsSource = recept;

            MessageBox.Show("Выполнен первый этап");

            int KolRec1 = 0;
            List<Recept> recept1 = new List<Recept>();
            FileName = @"D:\Projects\visualstudio_source\Resources\prod2.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    Srie[i] = RecArray[0];
                    Rshd[i] = Double.Parse(RecArray[1]);
                    recept1.Add(new Recept() { NameSyrie = Srie[i], Rashod = Rshd[i] });
                    i++;

                    KolRec1 = i;
                }
            }

            dgTable.ItemsSource = recept1;
            int KolNew = 0;
            for (int i = 0; i < KolRec1; i++)
            {

                int priznak = 0;
                for (int j = 0; j < KolRec; j++)
                {
                    if (Syrie[j] == Srie[i])
                    {
                        Rashod[j, 1] = Rshd[i];
                        priznak++;
                    }
                }
                if (priznak != 1)  //совпадение необнаружено
                {
                    Syrie[KolRec + KolNew] = Srie[i];
                    Rashod[KolRec + KolNew, 1] = Rshd[i];
                    KolNew++;
                }
            }
            MessageBox.Show("Выполнен второй этап");

            int KolRec2 = 0;
            List<Recept> recept2 = new List<Recept>();
            FileName = @"D:\Projects\visualstudio_source\Resources\prod3.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    Srie[i] = RecArray[0];
                    Rshd[i] = Double.Parse(RecArray[1]);
                    recept2.Add(new Recept() { NameSyrie = Srie[i], Rashod = Rshd[i] });
                    i++;

                    KolRec2 = i;
                }
            }

            dgTable.ItemsSource = recept2;

            for (int i = 0; i < KolRec2; i++)
            {

                int priznak = 0;
                for (int j = 0; j < KolRec + KolNew; j++)
                {
                    if (Syrie[j] == Srie[i])
                    {
                        Rashod[j, 2] = Rshd[i];
                        priznak++;
                    }
                }
                if (priznak != 1)  //совпадение необнаружено
                {
                    Syrie[KolRec + KolNew] = Srie[i];
                    Rashod[KolRec + KolNew, 2] = Rshd[i];
                    KolNew++;
                }
            }
            MessageBox.Show("Выполнен третий этап");

            int KolRec3 = 0;
            List<Recept> recept3 = new List<Recept>();
            FileName = @"D:\Projects\visualstudio_source\Resources\prod4.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    Srie[i] = RecArray[0];
                    Rshd[i] = Double.Parse(RecArray[1]);
                    recept3.Add(new Recept() { NameSyrie = Srie[i], Rashod = Rshd[i] });
                    i++;

                    KolRec3 = i;
                }
            }

            dgTable.ItemsSource = recept3;

            for (int i = 0; i < KolRec3; i++)
            {

                int priznak = 0;
                for (int j = 0; j < KolRec + KolNew; j++)
                {
                    if (Syrie[j] == Srie[i])
                    {
                        Rashod[j, 3] = Rshd[i];
                        priznak++;
                    }
                }
                if (priznak != 1)  //совпадение необнаружено
                {
                    Syrie[KolRec + KolNew] = Srie[i];
                    Rashod[KolRec + KolNew, 3] = Rshd[i];
                    KolNew++;
                }
            }
            MessageBox.Show("Выполнен четвертый этап");

            int KolRec4 = 0;
            List<Recept> recept4 = new List<Recept>();
            FileName = @"D:\Projects\visualstudio_source\Resources\prod5.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    Srie[i] = RecArray[0];
                    Rshd[i] = Double.Parse(RecArray[1]);
                    recept4.Add(new Recept() { NameSyrie = Srie[i], Rashod = Rshd[i] });
                    i++;

                    KolRec4 = i;
                }
            }

            dgTable.ItemsSource = recept4;

            for (int i = 0; i < KolRec4; i++)
            {

                int priznak = 0;
                for (int j = 0; j < KolRec + KolNew; j++)
                {
                    if (Syrie[j] == Srie[i])
                    {
                        Rashod[j, 4] = Rshd[i];
                        priznak++;
                    }
                }
                if (priznak != 1)  //совпадение необнаружено
                {
                    Syrie[KolRec + KolNew] = Srie[i];
                    Rashod[KolRec + KolNew, 4] = Rshd[i];
                    KolNew++;
                }
            }
            MessageBox.Show("Выполнен пятый этап");



            // добавление в файл
            using (StreamWriter writer = new StreamWriter(@"D:\Projects\visualstudio_source\Resources\ProdRecept.txt"))
            {
                for (int i = 0; i < KolRec + KolNew; i++)
                    writer.WriteLine(Syrie[i] + "," + Rashod[i, 0] + "," + Rashod[i, 1] + "," + Rashod[i, 2] + "," + Rashod[i, 3] + "," + Rashod[i, 4]);
            }


            MessageBox.Show("Выполнено");

            //чтение матрицы
            List<Matrix> matr = new List<Matrix>();

            using (StreamReader sr = new StreamReader(@"D:\Projects\visualstudio_source\Resources\ProdRecept.txt"))
            {

                while (!sr.EndOfStream)
                {
                    RecArray = sr.ReadLine().Split(',');
                    matr.Add(new Matrix() { NameSyrie = RecArray[0], Rashod1 = RecArray[1], Rashod2 = RecArray[2], Rashod3 = RecArray[3], Rashod4 = RecArray[4], Rashod5 = RecArray[5] });
                }
                dgTable.ItemsSource = matr;
            }


        }

    }
}
