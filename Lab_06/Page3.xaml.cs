using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;

namespace Lab_06
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }
        public void Raschet1(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //читаем файл ассортимент и определяем число продуктов
            string FileName = @"D:\Projects\visualstudio_source\Resources\ProdAssort.txt";
            string[] SortArray = new string[10];
            string[] Prod = new string[10];

            int KolProd = 0;
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                    Prod[KolProd++] = sr.ReadLine().Split(',')[0];

            //открываем файл-матрицу сырье-продукт и создаем массив суточный выпуск
            FileName = @"D:\Projects\visualstudio_source\Resources\ProdRecept.txt";
            string[] Syrie = new string[100];
            double[,] Rashod = new double[100, 10];


            int KolSyrie = 0;
            //читаем расход сырья кг на тонну продукции
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Syrie[KolSyrie] = SortArray[0];
                    for (int j = 1; j < KolProd + 1; j++)
                        Rashod[KolSyrie, j] = (SortArray[j] == "" || SortArray[j] == " ") ? Rashod[KolSyrie, j] = 0.0 : Convert.ToDouble(SortArray[j]);
                    KolSyrie++;
                }
            }
            tbKolSyrie.Text = Convert.ToString(KolSyrie);


            //открываем файл с характеристикой сырья и формируем массив
            int[] SyrieSrokHran = new int[100];
            int[] SyrieNagruzka = new int[100];

            FileName = @"D:\Projects\visualstudio_source\Resources\ProdSyrie.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Syrie[i] = SortArray[0];
                    SyrieSrokHran[i] = Convert.ToInt32(SortArray[1]);
                    SyrieNagruzka[i] = Convert.ToInt32(SortArray[2]);
                    i++;
                }
            }

            //открываем файл с режимом работы и формируем массив режима работы
            //открываем файл с оборудованием и формируем массив производительности кг в час
            int[] Proizvod = new int[10];
            double[] KoefIsp = new double[10];
            int[] KolOborud = new int[10];
            string[] Sort = new string[10];
            int[] KolRab = new int[10];
            int[] KolSmena = new int[10];
            double[] TimeSmena = new double[10];

            KolProd = 0;
            FileName = @"D:\Projects\visualstudio_source\Resources\ProdOborudRezim.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');

                    Sort[KolProd] = SortArray[0];
                    Proizvod[KolProd] = Convert.ToInt32(SortArray[2]);
                    KoefIsp[KolProd] = Convert.ToDouble(SortArray[3]);
                    KolOborud[KolProd] = Convert.ToInt32(SortArray[4]);

                    KolRab[KolProd] = Convert.ToInt32(SortArray[5]);
                    KolSmena[KolProd] = Convert.ToInt32(SortArray[6]);
                    TimeSmena[KolProd] = Convert.ToDouble(SortArray[7]);
                    KolProd++;
                }
            }
            tbKolProd.Text = Convert.ToString(KolProd);

            //вычисляем суточный выпуск для каждого сорта кг в смену
            double[] SutVypusk = new double[10];
            for (int j = 0; j < KolProd; j++)
                SutVypusk[j] = Proizvod[j] * KoefIsp[j] * KolOborud[j] * TimeSmena[j] * KolSmena[j];


            // Записать файл суточного выпуска
            using (StreamWriter writer = new StreamWriter(@"D:\Projects\visualstudio_source\Resources\ProdSutVypusk.txt"))
                for (int i = 0; i < KolProd; i++)
                    writer.WriteLine(Sort[i] + "," + SutVypusk[i]);


            //расчет площади склада сырья

            double[] Plo = new double[100];
            for (int i = 0; i < KolSyrie; i++)
            {
                double sum = 0;
                for (int j = 0; j < KolProd; j++)
                    sum += Rashod[i, j] / 1000 * SutVypusk[j] * SyrieSrokHran[j] / SyrieNagruzka[j];

                Plo[i] = Math.Round(sum, 1);
                // срок хрранения и гагрузка на площадь другие
            }

            // Записать файл 
            using (StreamWriter writer = new StreamWriter(@"D:\Projects\visualstudio_source\Resources\ProdPloSyrie.txt"))
            {
                double sum = 0;
                for (int i = 0; i < KolSyrie; i++)
                {
                    sum += Plo[i];
                    writer.WriteLine(Syrie[i] + "," + Plo[i]);
                }
                writer.WriteLine("Итого: " + sum);
            }

            //заполняем таблицу на экране
            List<ProdPlo> prods = new List<ProdPlo>();
            {
                double sum = 0;
                for (int i = 0; i < KolSyrie; i++)
                {
                    prods.Add(new ProdPlo() { Prod = Syrie[i], Plo = Math.Round(Plo[i], 1) });
                    sum += Plo[i];
                }
                prods.Add(new ProdPlo() { Prod = "Итого:", Plo = Math.Round(sum, 1) });
            }
            dgSyrie.ItemsSource = prods;

            MessageBox.Show("Расчет окончен!");
        }
        public void Raschet2(object sender, RoutedEventArgs e)
        {
            //расчет склада готовой продукции
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string FileName;
            string[] SortArray = new string[10];
            string[] Sort = new string[10];

            //читаем файл ассортимент и определяем число продуктов
            FileName = @"D:\Projects\visualstudio_source\Resources\ProdAssort.txt";
            string[] Prod = new string[10];
            int[] SrokHran = new int[10];
            int[] Nagruzka = new int[10];

            int KolProd = 0;

            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Prod[KolProd] = SortArray[0];
                    SrokHran[KolProd] = Convert.ToInt32(SortArray[1]);
                    Nagruzka[KolProd] = Convert.ToInt32(SortArray[2]);
                    KolProd++;
                }

            //открываем файл с оборудованием и формируем массив производительности кг в час
            string[] MarkaOborud = new string[10];
            int[] Proizvod = new int[10];
            double[] KoefIsp = new double[10];
            int[] KolOborud = new int[10];

            FileName = @"D:\Projects\visualstudio_source\Resources\ProdOborudRezim.txt";
            KolProd = 0;
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');

                    Sort[KolProd] = SortArray[0];

                    MarkaOborud[KolProd] = SortArray[1];
                    Proizvod[KolProd] = Convert.ToInt32(SortArray[2]);
                    KoefIsp[KolProd] = Convert.ToDouble(SortArray[3]);
                    KolOborud[KolProd] = Convert.ToInt32(SortArray[4]);
                    KolProd++;
                }


            // Читаем файл суточного выпуска
            double[] SutVypusk = new double[10];

            using (StreamReader sr = new StreamReader(@"D:\Projects\visualstudio_source\Resources\ProdSutVypusk.txt"))
            {
                int j = 0;
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    SutVypusk[j] = Convert.ToDouble(SortArray[1]);
                    j++;
                }
            }

            //расчет площади склада сырья
            double[] Plo = new double[100];

            for (int j = 0; j < KolProd; j++)
                Plo[j] = SutVypusk[j] * SrokHran[j] / Nagruzka[j];

            // Записать файл 
            double sum = 0;
            using (StreamWriter writer = new StreamWriter(@"D:\Projects\visualstudio_source\Resources\ProdPlo.txt"))
            {
                for (int i = 0; i < KolProd; i++)
                {
                    sum += Plo[i];
                    writer.WriteLine(Sort[i] + "," + Math.Round(Plo[i], 1));
                }
                writer.WriteLine("Итого: " + Math.Round(sum));
            }

            //заполняем таблицу на экране
            List<ProdPlo> prods = new List<ProdPlo>();
            for (int i = 0; i < KolProd; i++)
            {
                prods.Add(new ProdPlo() { Prod = Sort[i], Plo = Math.Round(Plo[i], 1) });
                sum += Plo[i];
            }
            prods.Add(new ProdPlo() { Prod = "Итого:", Plo = Math.Round(sum, 1) });

            dgSyrie.ItemsSource = prods;
            MessageBox.Show(" Расчет окончен! ");
        }
        //unverified code. Example was not present.
        public void Raschet3(object sender, RoutedEventArgs e) 
        {
            List<ProdPlo> prods = new List<ProdPlo>();
            prods.Add(new ProdPlo() { Prod = "М1", Plo = 6.6 });
            prods.Add(new ProdPlo() { Prod = "М2", Plo = 8.2 });
            prods.Add(new ProdPlo() { Prod = "М3", Plo = 1.5 });
            prods.Add(new ProdPlo() { Prod = "М4", Plo = 1.5 });
            prods.Add(new ProdPlo() { Prod = "М5", Plo = 1.9 });
            prods.Add(new ProdPlo() { Prod = "М6", Plo = 1.1 });
            prods.Add(new ProdPlo() { Prod = "М7", Plo = 3.8 });
            prods.Add(new ProdPlo() { Prod = "М8", Plo = 1.5 });
            double sum = 0;
            foreach (ProdPlo element in prods) sum += element.Plo;
            prods.Add(new ProdPlo() { Prod = "Итого:", Plo = Math.Round(sum, 1) });
            dgSyrie.ItemsSource = prods;
            MessageBox.Show(" Расчет окончен! ");
        }
    }
}
