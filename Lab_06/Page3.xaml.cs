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
        public void Raschet2(object sender, RoutedEventArgs e)
        {
            //расчет склада готовой продукции
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            string FileName;
            string[] SortArray = new string[10];
            string[] Sort = new string[10];

            //читаем файл ассортимент и определяем число продуктов
            FileName = @".\..\..\..\..\Resources\ProdAssort.txt";
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

            FileName = @".\..\..\..\..\Resources\ProdOborudRezim.txt";
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

            using (StreamReader sr = new StreamReader(@".\..\..\..\..\Resources\ProdSutVypusk.txt"))
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
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\..\Resources\ProdPlo.txt"))
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
        private void Raschet3(object sender, RoutedEventArgs e)
        {
            //Расчет склада вспомогательных материалов и тары
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");

            //читаем файл ассортимент и определяем число продуктов
            string FileName = @".\..\..\..\..\Resources\ProdAssort.txt";
            string[] SortArray = new string[10];
            string[] Prod = new string[10];
            string[] SortName = new string[10];

            int KolProd = 0;

            //читаем расход материалов кг на тонну продукции
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Prod[KolProd] = SortArray[0];
                    SortName[KolProd] = SortArray[1];
                    KolProd++;
                }
            //открываем файл-матрицу материал-продукт и создаем массив
            string[] Mater = new string[10];
            FileName = @".\..\..\..\..\Resources\ProdMatSort.txt";
            double[,] Rashod = new double[100, 10];

            int KolMat = 0;

            //читаем расход материалов кг на тонну продукции
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');

                    Mater[KolMat] = SortArray[0];
                    for (int j = 0; j < KolProd; j++)
                    {
                        tbKolSyrie.Text = SortArray[j + 1];
                        if (SortArray[j + 1] == "")
                            Rashod[KolMat, j] = 0;
                        else
                            Rashod[KolMat, j] = Double.Parse(SortArray[j + 1]);

                    }
                    KolMat++;
                }
            tbKolSyrie.Text = Convert.ToString(KolMat);

            //открываем файл с характеристикой вспомогательных материалов и формируем массив
            int[] MatSrokHran = new int[100];
            int[] MatNagruzka = new int[100];

            FileName = @".\..\..\..\..\Resources\ProdMat.txt";
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    MatSrokHran[i] = Convert.ToInt32(SortArray[1]);
                    MatNagruzka[i] = Convert.ToInt32(SortArray[2]);
                    i++;
                }
            }

            //открываем файл с режимом работы и формируем массив режима работы
            string[] Sort = new string[10];
            int[] KolRab = new int[10];
            int[] KolSmena = new int[10];
            double[] TimeSmena = new double[10];

            KolProd = 0;
            FileName = @".\..\..\..\..\Resources\ProdOborudRezim.txt";
            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Sort[KolProd] = SortArray[0];
                    KolRab[KolProd] = Convert.ToInt32(SortArray[5]);
                    KolSmena[KolProd] = Convert.ToInt32(SortArray[6]);
                    TimeSmena[KolProd] = Convert.ToDouble(SortArray[7]);

                    KolProd++;
                }

            tbKolProd.Text = Convert.ToString(KolProd);

            // Читаем файл суточного выпуска
            double[] SutVypusk = new double[10];
            using (StreamReader sr = new StreamReader(@".\..\..\..\..\Resources\ProdSutVypusk.txt"))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    SortArray = sr.ReadLine().Split(',');
                    Sort[i] = SortArray[0];
                    SutVypusk[i] = Double.Parse(SortArray[1]);

                    i++;
                }
            }


            //расчет площади склада сырья
            double[] Plo = new double[100];
            for (int i = 0; i < KolMat; i++)  //по материалам
            {
                double sum1 = 0;
                for (int j = 0; j < KolProd; j++) //по продуктам
                {
                    if (Mater[i] == "Гофрокартон")
                        sum1 += SutVypusk[j] * 1.0 / Rashod[i, j] * MatSrokHran[i] / MatNagruzka[i];
                    else if (Mater[i] == "Футляры" || Mater[i] == "Картон коробочный")
                    {
                        if (Rashod[i, j] != 0)
                            sum1 += SutVypusk[j] * 0.03 / Rashod[i, j] * MatSrokHran[i] / MatNagruzka[i]; 
                    }
                    else
                    {
                        sum1 += Rashod[i, j] / 1000 * SutVypusk[j] * MatSrokHran[i] / MatNagruzka[i];
                        if (i == 0 & j == 6)
                        {
                            // ?
                        }
                    }
                }
                Plo[i] = Math.Round(sum1, 1);
                // срок хранения и нагрузка на площадь другие
            }

            // Записать файл 
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\..\Resources\ProdPloMat.txt"))
            {
                double sum2 = 0;
                for (int i = 0; i < KolMat; i++)
                {
                    sum2 += Plo[i];
                    writer.WriteLine(Mater[i] + "," + Convert.ToString(Plo[i]));
                }
                writer.WriteLine("Итого: " + sum2);
            }

            // Записать файл 
            using (StreamWriter writer = new StreamWriter(@".\..\..\..\..\Resources\ProdPloMatNew.txt"))
            {
                double sum2 = 0;
                for (int i = 0; i < KolMat; i++)
                {
                    double PodlezitHran = SutVypusk[i] * MatSrokHran[i];
                    sum2 += Plo[i];
                    writer.WriteLine(Mater[i] + "," + SutVypusk[i] + "," + PodlezitHran + "," + MatSrokHran[i] + "," + MatNagruzka[i] + "," + Convert.ToString(Plo[i]));
                }
                writer.WriteLine("Итого: " + sum2);
            }

            //заполняем таблицу на экране
            List<ProdPlo> prods = new List<ProdPlo>();

            double sum = 0;
            for (int i = 0; i < KolMat; i++)
            {
                prods.Add(new ProdPlo() { Prod = Mater[i], Plo = Math.Round(Plo[i], 1) });
                sum += Plo[i];
            }
            prods.Add(new ProdPlo() { Prod = "Итого:", Plo = Math.Round(sum, 1) });
            dgSyrie.ItemsSource = prods;
            MessageBox.Show(" Расчет окончен! ");
        }
    }
}
