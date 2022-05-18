using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using Microsoft.Office.Interop.Word;


namespace Lab_11
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
        private void OpenTable(object sender, RoutedEventArgs e)
        {
            string FileName = @".\..\..\..\Resources\ProdTable2.txt";
            string[] TabArray;

            List<Class1> sostav = new List<Class1>();

            using (StreamReader sr = new StreamReader(FileName))
                while (!sr.EndOfStream)
                {
                    TabArray = sr.ReadLine().Split(',');
                    sostav.Add(new Class1()
                    {
                        Sort = TabArray[0],
                        Procent = TabArray[1],
                        KgSmena = TabArray[2],
                        KgSutki = TabArray[3],
                        VypuskTonnGod = TabArray[4],
                        VidZavert = TabArray[5]
                    });
                }
            dgTable.ItemsSource = sostav;
        }
        private void Export(object sender, RoutedEventArgs e)
        {
            string FileName = @"d:\ProdTable2.txt";
            string[] TabArray;
            string[] Sort = new string[10];
            string[] Procent = new string[10];
            string[] VidUpak = new string[10];
            int KolZap = 0;
            using (StreamReader sr = new StreamReader(FileName))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    TabArray = sr.ReadLine().Split(',');
                    Sort[i] = TabArray[0];
                    Procent[i] = TabArray[1];
                    VidUpak[i] = TabArray[5];
                    i++;
                    KolZap = i;

                }

            }
            var wordApp = new Word.Application();
            //wordApp.Visible = false;
            wordApp.Visible = true;
            //var wordDoucument = wordApp.Documents.Open(TemplateFileName);
            Word.Document document = wordApp.Documents.Add();
            Word.Paragraph userParagraf = document.Paragraphs.Add();
            Word.Range userRange = userParagraf.Range;
            userRange.Text = "Таблица 2";
            userParagraf.set_Style("Заголовок 1");
            userRange.InsertParagraphAfter();

            Word.Paragraph tableParagraf = document.Paragraphs.Add();
            Word.Range tableRange = tableParagraf.Range;
            Word.Table myTable = document.Tables.Add(tableRange, KolZap + 1, 3);
            myTable.Borders.InsideLineStyle = myTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
            myTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

            Word.Range cellRange;

            cellRange = myTable.Cell(1, 1).Range;
            cellRange.Text = "Сырье";
            cellRange = myTable.Cell(1, 2).Range;
            cellRange.Text = "Процент";
            cellRange = myTable.Cell(1, 3).Range;
            cellRange.Text = "Вид упаковки";

            myTable.Rows[1].Range.Bold = 1;
            myTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

            for (int i = 0; i < KolZap; i++)
            {
                cellRange = myTable.Cell(i + 2, 1).Range;
                cellRange.Text = Sort[i];
                cellRange = myTable.Cell(i + 2, 2).Range;
                cellRange.Text = Procent[i];
                cellRange = myTable.Cell(i + 2, 3).Range;
                cellRange.Text = VidUpak[i];
            }
        }
    }
}
