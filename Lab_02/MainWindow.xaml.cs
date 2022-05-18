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

        private void matrix(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            ////читаем содержание файла
            string FileName = @".\..\..\..\Resources\prod1.txt";
            string[] Srie = new string[100];
            double[] Rshd = new double[100];
            int KolRec = 0;

            string[] Syrie = new string[100];
            double[,] Rashod = new double[100, 20];

            List<Recept> recept = new List<Recept>();

            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    Srie[KolRec] = RecArray[0];
                    Rshd[KolRec] = Double.Parse(RecArray[1]);
                    Syrie[KolRec] = Srie[KolRec];
                    Rashod[KolRec, 0] = Rshd[KolRec];
                    recept.Add(new Recept() { NameSyrie = Srie[KolRec], Rashod = Rshd[KolRec] });
                    KolRec++;
                }
            dgTable.ItemsSource = recept;
            MessageBox.Show("Выполнен первый этап");

            int KolRec1 = 0;
            List<Recept> recept1 = new List<Recept>();
            FileName = @".\..\..\..\Resources\prod2.txt";
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    Srie[KolRec1] = RecArray[0];
                    Rshd[KolRec1] = Double.Parse(RecArray[1]);
                    recept1.Add(new Recept() { NameSyrie = Srie[KolRec1], Rashod = Rshd[KolRec1] });
                    KolRec1++;
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
            FileName = @".\..\..\..\Resources\prod3.txt";
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    Srie[KolRec2] = RecArray[0];
                    Rshd[KolRec2] = Double.Parse(RecArray[1]);
                    recept2.Add(new Recept() { NameSyrie = Srie[KolRec2], Rashod = Rshd[KolRec2] });
                    KolRec2++;
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
            FileName = @".\..\..\..\Resources\prod4.txt";
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    Srie[KolRec3] = RecArray[0];
                    Rshd[KolRec3] = Double.Parse(RecArray[1]);
                    recept3.Add(new Recept() { NameSyrie = Srie[KolRec3], Rashod = Rshd[KolRec3] });
                    KolRec3++;
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
            FileName = @".\..\..\..\Resources\prod5.txt";
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    Srie[KolRec4] = RecArray[0];
                    Rshd[KolRec4] = Double.Parse(RecArray[1]);
                    recept4.Add(new Recept() { NameSyrie = Srie[KolRec4], Rashod = Rshd[KolRec4] });
                    KolRec4++;
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
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\Resources\ProdRecept.txt"))
            {
                for (int i = 0; i < KolRec + KolNew; i++)
                    writer.WriteLine(Syrie[i] + "," + Rashod[i, 0] + "," + Rashod[i, 1] + "," + Rashod[i, 2] + "," + Rashod[i, 3] + "," + Rashod[i, 4]);
            }

            MessageBox.Show("Выполнено");

            //чтение матрицы
            List<Matrix> matr = new List<Matrix>();

            using (StreamReader sr = new StreamReader(@".\..\..\..\Resources\ProdRecept.txt"))
                while (!sr.EndOfStream)
                {
                    string[] RecArray = sr.ReadLine().Split(',');
                    matr.Add(new Matrix() { NameSyrie = RecArray[0], Rashod1 = RecArray[1], Rashod2 = RecArray[2], Rashod3 = RecArray[3], Rashod4 = RecArray[4], Rashod5 = RecArray[5] });
                }
            dgTable.ItemsSource = matr;
        }
    }
}
