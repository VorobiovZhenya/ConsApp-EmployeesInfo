using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DZ_3
{
    class Menu
    {
        public List<Employee> employees;
        public void OpenBd(string option)
        {
            employees = new List<Employee>();
            //employees.Add(new Employee("Petya", "Petrov"));
            //employees.Add(new Employee("Ivanov", "Ivan"));
            //employees.Add(new Employee("Sidor", "Sidorov"));
            try
            {
                if (option == "xml")
                {
                    using (FileStream fs = new FileStream("xmlemployees.dat", FileMode.Open))
                    {
                        XmlSerializer formatter = new XmlSerializer(typeof(List<Employee>));
                        employees = (List<Employee>)formatter.Deserialize(fs);
                    }
                }
                else
                {
                    using (FileStream fs = new FileStream("binemployees.dat", FileMode.Open))
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        employees = (List<Employee>)formatter.Deserialize(fs);
                    }
                }
                Console.WriteLine("База данных десериализована.");
                Console.WriteLine();
            }

            catch (Exception e)
            {
                if (e is NullReferenceException)
                {
                    Console.WriteLine("Файл базы данны не коректен или поврежден!!!");
                    Console.WriteLine("Для продолжения работы с новой базой данных нажмите Enter");
                    //Console.WriteLine(e.Message);
                    Console.ReadLine();
                    //employees.Clear();
                }
                if (e is FileNotFoundException)
                {
                    Console.WriteLine("База данных не найдена!!!");
                    Console.WriteLine("Для продолжения работы с новой базой данных нажмите Enter");
                    //Console.WriteLine(e.Message);
                    Console.ReadLine();
                    //employees.Clear();
                }
                else
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            MainMenu(option);
        }        
        public void MainMenu(string option)
        {             
            while (true)
            {
                //Console.Clear();                
                int counter = 1;
                foreach (var emp in employees)
                {
                    Console.WriteLine(emp.ToString(counter++));
                }                    
                Console.WriteLine();
                Console.WriteLine("add - создать запись сотрудника;");
                Console.WriteLine("del - удалить запись сотрудника;");                
                Console.WriteLine("open - просмотреть запись конкретного сотрудника.");
                Console.WriteLine("openAll - просмотреть все записи сотрудников;");
                Console.WriteLine("exit - выход");
                Console.Write("Введите команду: ");

                string command = Console.ReadLine();
                int recordNumber;
                switch (command.ToLower())
                {
                    case "exit" :
                        Exit(option);
                        return;                        
                    case "add" :
                        Adding();
                        Console.Clear();
                        break;
                    case "del":
                        Console.Write("Введите порядковый номер записи для удаления: ");
                        try
                        {
                            recordNumber = Int32.Parse(Console.ReadLine());
                            employees.RemoveAt(recordNumber - 1);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }
                        Console.Clear();
                        break;
                    case "open":
                        Console.Write("Введите порядковый номер записи для просмотра: ");
                        try
                        {
                            recordNumber = Int32.Parse(Console.ReadLine());                            
                            Console.WriteLine(employees[recordNumber - 1].ToString());
                            Console.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            Console.ReadLine();
                        }
                        Console.Clear();
                        break;
                    case "openall":
                        foreach (var emp in employees)
                        {
                            Console.WriteLine(emp.ToString());
                            Console.WriteLine();
                        }                        
                        Console.ReadLine();                                          
                        Console.Clear();
                        break;                        
                    default: 
                        Console.WriteLine("Неверная команда!!!");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }                
            }
        }
        public void Adding()
        {
            employees.Add(new Employee());
            string regMail = @"\b[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}\b";
            string regName = @"[a-z]";
            string regNumber = @"\d";
            string regPhone = @"\d{3}-\d{3}-\d{4}";
            Console.WriteLine();
            while (true)
            {
                Console.Write("Введите имя сотрудника:");
                string fname = Console.ReadLine();

                if (Regex.IsMatch(fname, regName, RegexOptions.IgnoreCase))
                {
                    employees.Last().Firstname = fname;  
                    break;
                }
                else
                {
                    Console.WriteLine("Имя должно содержать только латинские буквы!");
                }
            }
            while (true)
            {
                Console.Write("Введите фамилию сотрудника:");
                string lname = Console.ReadLine();

                if (Regex.IsMatch(lname, regName, RegexOptions.IgnoreCase))
                {
                    employees.Last().Lastname = lname;
                    break;
                }
                else
                {
                    Console.Write("Фамилия должна содержать только латинские буквы!");
                }
            }
            while (true)
            {
                Console.Write("Введите должность сотрудника:");
                string pos = Console.ReadLine();

                if (Regex.IsMatch(pos, regName, RegexOptions.IgnoreCase))
                {
                    employees.Last().Position = pos;
                    break;
                }
                else
                {
                    Console.WriteLine("Должность должна содержать только латинские буквы!");
                }
            }
            while (true)
            {
                Console.Write("Введите оклад сотрудника: ");
                string salary = Console.ReadLine();

                if (Regex.IsMatch(salary, regNumber, RegexOptions.IgnoreCase))
                {
                    employees.Last().Salary = salary;
                    break;
                }
                else
                {
                    Console.WriteLine("Оклад может содержать только цифры!");
                }
            }
            while (true)
            {
                Console.Write("Введите телефон сотрудника (XXX-XXX-XXXX):");
                string phone = Console.ReadLine();

                if (Regex.IsMatch(phone, regPhone, RegexOptions.IgnoreCase))
                {
                    employees.Last().PhoneNumber = phone;
                    break;
                }
                else
                {
                    Console.WriteLine("Введите телефон в формате XXX-XXX-XXXX");
                }
            }
            while (true)
            {
                Console.Write("Введите адрес электронной почты:");
                string email = Console.ReadLine();

            if (Regex.IsMatch(email, regMail, RegexOptions.IgnoreCase))
               {
                   employees.Last().Email = email;
                    break;
               }
               else
               {
                  Console.WriteLine("Некорректный email!");
               } 
            }             
        }        
        public void Exit(string opt)
        {
            if (opt == "xml")
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<Employee>));
                using (FileStream fs = new FileStream("xmlemployees.dat", FileMode.Create))
                {
                    formatter.Serialize(fs, employees);
                    Console.WriteLine("База данных сохранена!");
                    Console.ReadLine();
                }
            }
            else
            {
                BinaryFormatter formatter = new BinaryFormatter();
                using (FileStream fs = new FileStream("binemployees.dat", FileMode.Create))
                {
                    formatter.Serialize(fs, employees);
                    Console.WriteLine("База данных сохранена!");
                    Console.ReadLine();
                }
            }            
        }
    }
}
