# Лабораторная работа #7

## Требования

- `.NET Framework 4.8`

- `Visual Studio 2022 Community Edition`

- `MS SQL Express 2019`

- `SQL Server Managment Studio`

- Файл [Lab_07_csv.txt](./../Resources/Lab_07_csv.txt)

## Копирование

1. Заменить `D:\Projects\visualstudio_source\Resources\` внутри файла [MainWindow.xaml.cs](./MainWindow.xaml.cs)

2. Выполнить всё в соответствии с 4 пунктом **Создания нового проекта**

## Создание нового проекта

1. Создать проект `Name` 

2. Скопировать код из [`MainWindow.xaml`](./MainWindow.xaml), заменив `Lab_07` на `Name`

3. Скопировать код из [`MainWindow.xaml.cs`](./MainWindow.xaml.cs), заменив `Lab_07` на `Name`

4. Создать класс [`CsvData.cs`](./CsvData.cs), скопировать код и заменить `Lab_07` на `Name`

5. Здесь понадобится получить строки:
   
   1. `datasoruce` (YOUR-PC-NAME\SQLEXPRESS),
   
   2. `database` (Самостоятельно создать базу данных в  SQL Express)
   
   И создать внутри базы данных таблицу `TableName`, заменив внутри файла [`MainWindow.xaml.cs`](./MainWindow.xaml.cs) `Table_01` на 64 строке.
