using System;
namespace DZ_3
{
    [Serializable]
    public class Employee
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Salary { get; set; }       
        public Employee()
        { }
        //public Employee(string name, string lastname)
        //{
        //    Firstname = name;
        //    Lastname = lastname;
        //}
        public string ToString( int number)
        {
           return number + ". " + Firstname +" "+ Lastname;
        }
        public override string ToString()
        {
            return " Имя: " + Firstname + "\n Фамилия: " + Lastname + "\n Должность: " + Position + "\n Оклад: " + Salary +
                    "\n Номер телефона: " + PhoneNumber + "\n E-mail: " + Email;
                    
        }
    }
}
