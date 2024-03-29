﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;
using System.IO;

namespace Lab_04
{
    /// <summary>
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3() => InitializeComponent(); 
        public void Raschet1(object sender, RoutedEventArgs e)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //читаем файл ассортимент и определяем число продуктов
            string FileName = @".\..\..\..\..\Resources\ProdAssort.txt";
            string[] SortArray = new string[10];
            string[] Prod = new string[10];

            int KolProd = 0;
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                    Prod[KolProd++] = sr.ReadLine().Split(',')[0];

            //открываем файл-матрицу сырье-продукт и создаем массив суточный выпуск
            FileName = @".\..\..\..\..\Resources\ProdRecept.txt";
            string[] Syrie = new string[100];
            double[,] Rashod = new double[100, 10];

            
            int KolSyrie = 0;
            //читаем расход сырья кг на тонну продукции
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Syrie[KolSyrie] = SortArray[0];
                    for (int j = 1; j < KolProd + 1; j++)
                        Rashod[KolSyrie, j] = (SortArray[j] == "" || SortArray[j] == " ") ? Rashod[KolSyrie, j] = 0.0 : Convert.ToDouble(SortArray[j]);
                    KolSyrie++;
                }
            tbKolSyrie.Text = Convert.ToString(KolSyrie);


            //открываем файл с характеристикой сырья и формируем массив
            int[] SyrieSrokHran = new int[100];
            int[] SyrieNagruzka = new int[100];

            FileName = @".\..\..\..\..\Resources\ProdSyrie.txt";
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
            int   [] Proizvod  = new int   [10];
            double[] KoefIsp   = new double[10];
            int   [] KolOborud = new int   [10];
            string[] Sort      = new string[10];
            int   [] KolRab    = new int   [10];
            int   [] KolSmena  = new int   [10];
            double[] TimeSmena = new double[10];

            KolProd = 0;
            FileName = @".\..\..\..\..\Resources\ProdOborudRezim.txt";
            using (StreamReader sr = new StreamReader(FileName))
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
            tbKolProd.Text = Convert.ToString(KolProd);

            //вычисляем суточный выпуск для каждого сорта кг в смену
            double[] SutVypusk = new double[10];
            for (int j = 0; j < KolProd; j++)
                SutVypusk[j] = Proizvod[j] * KoefIsp[j] * KolOborud[j] * TimeSmena[j] * KolSmena[j];


            // Записать файл суточного выпуска
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\..\Resources\ProdSutVypusk.txt"))
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
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\..\Resources\ProdPloSyrie.txt"))
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
        public void Raschet2(object sender, RoutedEventArgs e) => MessageBox.Show("There is no function ready to execute a task.");
        public void Raschet3(object sender, RoutedEventArgs e) => MessageBox.Show("There is no function ready to execute a task.");
    }
}
