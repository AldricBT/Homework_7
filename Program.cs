using System;
using System.Threading;

namespace Homework_7
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Repository rep1 = new Repository("DB.txt");            
             

            Console.WriteLine("Приложения для работы с базой данных работников.");
            Console.WriteLine();
            Console.WriteLine("Вам доступны следующие команды:");

            Console.WriteLine("createrandom -- создает случайную базу данных и перезаписывает файл;");
            Console.WriteLine("printall -- вывод всей базы данных в консоль;");
            Console.WriteLine("print -- вывод информации о работнике по id;");
            Console.WriteLine("add -- добавление работника в конец базы;");
            Console.WriteLine("delete -- удаление работника по id;");
            Console.WriteLine("betweendate -- вывод работников в выбранном диапазоне дат их добавления в базу (дата в формате dd.MM.yyyy);");
            Console.WriteLine("sort -- сортировка работников по выбранным полям и вывод их на экран;");
            Console.WriteLine("save -- сохранение изменений в файл");
            Console.WriteLine("exit -- выход из программы");
            Console.WriteLine();

            bool exitflag = true;
            while (exitflag)
            {
                Console.Write("Введите команду: ");
                string command = Console.ReadLine();
                switch (command)
                {
                    case "createrandom":
                        {
                            Console.Write("Введите число работников (не больше 100): ");
                            try
                            {
                                int num = int.Parse(Console.ReadLine());
                                if (num > 100) num = 100;
                                rep1.CreateRandomDB(num);     //создание базы данных со случайными работниками,
                                Console.WriteLine("База успешно создана и сохранена в файл!");
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); }                                                     
                            break;
                        }                        
                    case "printall":
                        {
                            rep1.PrintAll();
                            break;
                        }
                    case "print":
                        {
                            Console.WriteLine("Введите id");
                            try
                            {
                                int id = int.Parse(Console.ReadLine());
                                rep1.PrintWorkerByID(id);
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); }                            
                            break;
                        }
                    case "add":
                        {
                            Console.Write("Введите ФИО: ");
                            string fio = Console.ReadLine();

                            Console.Write("Введите рост: ");
                            int height = 0;
                            try
                            {
                                height = int.Parse(Console.ReadLine());
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); break; }
                            
                            Console.Write("Введите дату рождения в формате dd.MM.yyyy: ");
                            DateTime dateofbirth = DateTime.Now;
                            try
                            {
                                dateofbirth = DateTime.Parse(Console.ReadLine());
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); break; }

                            int age = DateTime.Now.Year - dateofbirth.Year;
                            if (age < 18) { Console.WriteLine("Детям нельзя работать!"); break; }
                            if (age > 100) { Console.WriteLine("Таким старым нельзя работать!"); break; }
                            

                            Console.Write("Введите место рождения: ");
                            string placeofbirth = Console.ReadLine();
                                                        
                            rep1.Add(new Worker(rep1.GetNewId(), DateTime.Now, fio, age, height, dateofbirth, placeofbirth));
                            Console.WriteLine("Работник успешно добавлен! Не забудьте сохранить данные!");
                            break;
                        }
                    case "delete":
                        {
                            Console.Write("Введите id: ");
                            try
                            {
                                int id = int.Parse(Console.ReadLine());                                
                                if (rep1.Delete(id))
                                {
                                    Console.WriteLine("Работник успешно удален!");
                                }
                                else
                                {
                                    Console.WriteLine("Работника с данным id нет в базе!");
                                }
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); }                            
                            break;
                        }
                    case "betweendate":
                        {                            
                            try
                            {
                                Console.Write("Введите дату начала поиска в формате dd.MM.yyyy: ");
                                DateTime datestart = DateTime.Parse(Console.ReadLine());
                                Console.Write("Введите дату конца поиска в формате dd.MM.yyyy: ");
                                DateTime dateend = DateTime.Parse(Console.ReadLine());
                                rep1.GetWorkersBetweenTwoDates(datestart, dateend.AddDays(1).AddSeconds(-1));
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); }
                            break;
                        }
                    case "sort":
                        {
                            Console.WriteLine("1 - ID;");
                            Console.WriteLine("2 - Дата добавления сотрудника в базу;");
                            Console.WriteLine("3 - ФИО;");
                            Console.WriteLine("4 - Возраст;");
                            Console.WriteLine("5 - Рост;");
                            Console.WriteLine("6 - Дата рождения;");
                            Console.WriteLine("7 - Место рождения;");
                            Console.WriteLine();
                            Console.Write("Сортировать по: ");
                            try
                            {
                                int key = int.Parse(Console.ReadLine());
                                if ((key >= 1) && (key <= 7))
                                {
                                    rep1.SortWorkers(key);
                                    rep1.PrintAll();
                                }
                                else
                                {
                                    Console.WriteLine("Неверный ввод!");
                                    break;
                                }
                            }
                            catch (Exception ex) { Console.WriteLine("Неверный ввод!"); }
                            
                            break;
                        }
                    case "save":
                        {
                            rep1.RefreshFile();
                            Console.WriteLine("База успешно сохранена в файл!");
                            break;
                        }
                    case "exit":
                        {
                            exitflag = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Неверно введена команда!");
                            break;
                        }
                }
                Console.WriteLine();
            }

        }
    }
}
