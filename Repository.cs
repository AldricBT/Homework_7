using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Homework_7
{
    class Repository
    {
        private string[] titles;
        private Worker[] workers;
        private string path;
        private int index;

        /// <summary>
        /// Чтение из файла и возврат массива экземпляров
        /// </summary>
        /// <returns></returns>
        public Worker[] GetAllWorkers()
        {
            Load();

        }

        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string[] args = sr.ReadLine().Split(',');

                    Add(new Worker(Convert.ToInt32(args[0]), Convert.ToDateTime(args[1]), args[2],
                        Convert.ToInt32(args[3]), Convert.ToInt32(args[4]), Convert.ToDateTime(args[5]), args[6]));
                }
            }

        }
        public void Add(Worker ConcreteWorker)
        {
            if (index >= this.workers.Length)
            {
                Resize();
            }
            this.workers[index] = ConcreteWorker;
            this.index++;
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
        /// Увеличение массива работников в два раза. Вызывается в случае выхода из массива
        /// </summary>
        private void Resize()
        {
            Array.Resize(ref this.workers, this.workers.Length * 2);            
        }
        /// <summary>
        /// Конструктор экземпляра класса. Вызывается при создании экземпляра (команда new)
        /// </summary>
        /// <param name="Path">Путь к файлу данных</param>
        public Repository(string Path)
        {
            this.titles = new string[]
        {   "ID",
            "Дата добавления записи",
            "ФИО",
            "Возраст",
            "Рост",
            "Дата рождения",
            "Место рождения"};
            this.workers = new Worker[2];
            this.path = Path;
            this.index = 0;
            if (File.Exists(Path))
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
