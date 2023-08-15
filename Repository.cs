using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Homework_7
{
    class Repository
    {
        private string[] titles;
        private Worker[] workers;
        private string path;
        private int index;

        /// <summary>
        /// Увеличение массива работников в два раза. Вызывается в случае выхода из массива
        /// </summary>
        private void Resize()
        {
            Array.Resize(ref this.workers, this.workers.Length + 1);
        }

        /// <summary>
        /// Создание файла репозитория
        /// </summary>
        private void CreateFile()
        {
            using (StreamWriter sw = new StreamWriter(this.path, false))
            {
                sw.WriteLine($"{titles[0],5} {titles[1],30} {titles[2],30} {titles[3],10} {titles[4],10} {titles[5],15} {titles[6],20}");
            }
        }

        /// <summary>
        /// Считывает данные с файла в память
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split('#');

                    Add(new Worker(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2], Convert.ToInt32(args[3]),
                      Convert.ToInt32(args[4]), DateTime.ParseExact(args[5], "dd.MM.yyyy", CultureInfo.InvariantCulture), args[6]));
                }
            }
        }

        /// <summary>
        /// Вывод данных одного работника на экран
        /// </summary>
        private void Print(Worker printworker)
        {
            Console.WriteLine($"{printworker.ID,5} {printworker.DateTimeAddNote,30} {printworker.FIO,30} {printworker.Age,10} {printworker.Height,10} {printworker.DateOfBirth.ToString("dd/MM/yyyy"),15} {printworker.PlaceOfBirth,20}");
        }

        /// <summary>
        /// Выводит данные из памяти на экран
        /// </summary>
        public void PrintAll()
        {
            Console.WriteLine($"{titles[0],5} {titles[1],30} {titles[2],30} {titles[3],10} {titles[4],10} {titles[5],15} {titles[6],20}");
            for (int i = 0; i < index; i++)
            {
                Print(workers[i]);
            }
        }

        /// <summary>
        /// Выводит данные одного работика, найденного по ID, на экран
        /// </summary>
        /// <param name="ID"></param>
        public void PrintWorkerByID(int id)
        {            
            Print(Array.Find(workers, w => w.ID == id));
        }        

        /// <summary>
        /// Чтение из файла и возврат массива экземпляров
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            Load();
            return workers;
        }                

        /// <summary>
        /// Добавление работника
        /// </summary>
        /// <param name="ConcreteWorker">Данные конкретного работника</param>
        public void Add(Worker worker)
        {
            if (index >= this.workers.Length)
            {
                Resize();
            }
            this.workers[index] = worker;
            this.index++;
        }
                
        /// <summary>
        /// Удаление работинка
        /// </summary>
        /// <param name="id">ID работника, подлежащего удалению</param>
        public void Delete(int id)
        {      
            workers = workers.Where(w => w.ID != id).ToArray();
            index--;
        }

        /// <summary>
        /// Поиск работников в диапазоне дат их добавления в базу данных
        /// </summary>
        /// <param name="dateFrom">Дата начала поиска</param>
        /// <param name="dateTo">Дата конца поиска</param>
        /// <returns></returns>
        public Worker[] GetWorkersBetweenTwoDates(DateTime dateFrom, DateTime dateTo)
        {
            if (dateFrom <= dateTo)
                return Array.FindAll(workers, w => (w.DateTimeAddNote >= dateFrom) && (w.DateTimeAddNote <= dateTo));
            else
            {
                Console.WriteLine("Даты введены неверно!");
                return null;
            }   
        }

        /// <summary>
        /// Сортировка списка работников в порядке возрастания
        /// </summary>
        /// <param name="key">Номер столбца, по которому производится сортировка</param>
        /// <returns></returns>
        public void SortWorkers(int key)
        {
            switch (key)
            {
                case 1:
                    workers = workers.OrderBy(w => w.ID).ToArray();
                    break;
                case 2:
                    workers = workers.OrderBy(w => w.DateTimeAddNote).ToArray();
                    break;
                case 3:
                    workers = workers.OrderBy(w => w.FIO).ToArray();
                    break;
                case 4:
                    workers = workers.OrderBy(w => w.Age).ToArray();
                    break;
                case 5:
                    workers = workers.OrderBy(w => w.Height).ToArray();
                    break;
                case 6:
                    workers = workers.OrderBy(w => w.DateOfBirth).ToArray();
                    break;
                case 7:
                    workers = workers.OrderBy(w => w.PlaceOfBirth).ToArray();
                    break;
                default:
                    Console.WriteLine("Введен некорректный номер столбца для сортировки");
                    break;
            }
        }

        /// <summary>
        /// Обновляет файл данными из памяти
        /// </summary>
        public void RefreshFile()
        {
            using (StreamWriter sw = new StreamWriter(this.path, false))
            {
                sw.WriteLine($"{titles[0],5} {titles[1],30} {titles[2],30} {titles[3],10} {titles[4],10} {titles[5],15} {titles[6],20}");
                for (int i = 0; i < index; i++)
                {
                    sw.WriteLine($"{workers[i].ID}#{workers[i].DateTimeAddNote}#{workers[i].FIO}#{workers[i].Age}#{workers[i].Height}#{workers[i].DateOfBirth.ToString("dd/MM/yyyy")}#{workers[i].PlaceOfBirth}");
                }
            }
        }

        /// <summary>
        /// Создает базу данных случайных работников
        /// </summary>
        /// <param name="Num">Кол-во элементов</param>
        public void CreateRandomDB(int Num)
        {
            Random rnd = new Random();
            for (int i = 0; i < Num; i++)
            {
                int age = rnd.Next(18, 66);
                DateTime dateofbirth = DateTime.Now.AddYears(-age);
                Add(new Worker(i + 1, DateTime.Now.AddDays(-rnd.Next(1,1000)), $"Ф_{i} И_{i} О_{i}", age,
                    rnd.Next(150, 201), dateofbirth.Date, $"Place_{i}"));
                Thread.Sleep(1);
            }
            RefreshFile();
        }

        /// <summary>
        /// Конструктор экземпляра класса. Вызывается при создании экземпляра (команда new)
        /// </summary>
        /// <param name="Path">Путь к файлу данных</param>
        public Repository()
        {
            this.titles = new string[]
        {   "ID",
            "Время добавления записи",
            "ФИО",
            "Возраст",
            "Рост",
            "Дата рождения",
            "Место рождения"};
            this.workers = new Worker[2];
            this.path = @"D:\Study\Skillbox_C\Урок 7 Структуры и конструкторы\WorkerRepo\DB.txt";
            this.index = 0;
            if (File.Exists(this.path))
            {
                GetAllWorkers();
            }
            else
            {
                CreateFile();
            }
        }

    }
}
