using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace Lab_8_1_OOP
{
    class Patient
    {
        private string Name;
        private string Surname;
        private int Age;
        private string Address;
        private string PhoneNumber;

        public Patient() { }
        public Patient(string name, string surname, int age, string address, string phonenumber)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Address = address;
            PhoneNumber = phonenumber;
        }

        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }

        public string surname
        {
            get
            {
                return Surname;
            }
            set
            {
                Surname = value;
            }
        }

        public int age
        {
            get
            {
                return Age;
            }
            set
            {
                Age = value;
            }
        }

        public string address
        {
            get
            {
                return Address;
            }
            set
            {
                Address = value;
            }
        }

        public string phonenumber
        {
            get
            {
                return PhoneNumber;
            }
            set
            {
                PhoneNumber = value;
            }
        }

    }



    class Program
    {
        static void WriteDB(List<Patient> patients)
        {
            string textRow;
            StreamWriter file = new StreamWriter("output.txt");
            foreach (Patient rr in patients)
            {
                textRow = rr.name + ";" + rr.surname + ";" + Convert.ToString(rr.age) + ";" +
                    rr.address + ";" + rr.phonenumber;
                file.WriteLine(textRow);
            }
            file.Close();
        }


        static List<Patient> ReadBD()
        {
            string sNameFile, textRow;
            string pName, pSurname, sAge, pAddress, pPhoneNumber;
            int pAge;
            int i, ip;

            List<Patient> patients = new List<Patient>();
            StreamReader file = new StreamReader("output.txt");
            while (file.Peek() >= 0)
            {
                pName = ""; pSurname = ""; sAge = ""; pAddress = ""; pPhoneNumber = "";
                textRow = file.ReadLine();
                i = textRow.IndexOf(';') - 1;
                for (int j = 0; j <= i; j++) pName = pName + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pSurname = pSurname + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) sAge = sAge + textRow[j];
                ip = i + 2;
                i = textRow.IndexOf(';', ip) - 1;
                for (int j = ip; j <= i; j++) pAddress = pAddress + textRow[j];
                ip = i + 2;
                for (int j = ip; j <= textRow.Length - 1; j++) pPhoneNumber = pPhoneNumber + textRow[j];

                pAge = Convert.ToInt32(sAge);
                patients.Add(new Patient(pName, pSurname, pAge, pAddress, pPhoneNumber));
            }
            file.Close();
            return patients;

        }

        static void AddPatient(List<Patient> patients)
        {
            Console.Write(" Iм'я: ");
            string name = Console.ReadLine();
            Console.Write("Прiзвище: ");
            string surname = Console.ReadLine();
            Console.Write("Вiк: ");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Адреса проживання: ");
            string address = Console.ReadLine();
            Console.Write("Номер телефону: ");
            string phonenumber = Console.ReadLine();

            patients.Add(new Patient(name, surname, age, address, phonenumber));
            WriteDB(patients);
        }

        static void EditPatient(List<Patient> patients)
        {
            Console.Write("Введiть прiзвище пацiєнта, iнформацiю про якого бажаєте редагувати: ");
            string surname = Console.ReadLine();

            if (patients.All(b => b.surname != surname))
            {
                Console.WriteLine("Пацiєнта з таким прiзвищем не iснує!");
                return;
            }

            Console.WriteLine("");
            Console.WriteLine("Оберiть параметр, який бажаєте редагувати: ");
            Console.WriteLine("Iм'я - 1");
            Console.WriteLine("Прiзвище - 2");
            Console.WriteLine("Вiк - 3");
            Console.WriteLine("Адреса проживання - 4");
            Console.WriteLine("Номер телефону - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1)
            {
                Console.Write("Нове iм'я: ");
                string newname = Console.ReadLine();
                patients.FindAll(s => s.surname == surname).ForEach(x => x.name = newname);
                WriteDB(patients);
            }
            else if (k == 2)
            {
                Console.Write("Нове прiзвище: ");
                string newsurname = Console.ReadLine();
                patients.FindAll(s => s.surname == surname).ForEach(x => x.surname = newsurname);
                WriteDB(patients);
            }
            else if (k == 3)
            {
                Console.Write("Новий вiк: ");
                int newage = Convert.ToInt32(Console.ReadLine());
                patients.FindAll(s => s.surname == surname).ForEach(x => x.age = newage);
                WriteDB(patients);
            }
            else if (k == 4)
            {
                Console.Write("Нова адреса: ");
                string newaddress = Console.ReadLine();
                patients.FindAll(s => s.surname == surname).ForEach(x => x.address = newaddress);
                WriteDB(patients);
            }
            else if (k == 5)
            {
                Console.Write("Новий номер телефону: ");
                string newphonenumber = Console.ReadLine();
                patients.FindAll(s => s.surname == surname).ForEach(x => x.phonenumber = newphonenumber);
                WriteDB(patients);
            }
            else if (k == 0) return;
        }

        static void RemovePatient(List<Patient> patients)
        {
            Console.Write("Введiть прiзвище пацiєнта, якого бажаєте видалити: ");
            string surname = Console.ReadLine();
            if (patients.All(b => b.surname != surname))
            {
                Console.WriteLine("Пацiєнта з таким прiзвищем не iснує!");
                return;
            }
            var itemToDelete = patients.Where(x => x.surname == surname).Select(x => x).First();
            patients.Remove(itemToDelete);
            WriteDB(patients);
        }

        static void SortPatients(List<Patient> patients)
        {
            Console.WriteLine("");
            Console.WriteLine("Оберiть критерiй сортування: ");
            Console.WriteLine("Iм'я - 1");
            Console.WriteLine("Прiзвище - 2");
            Console.WriteLine("Вiк - 3");
            Console.WriteLine("Адреса проживання - 4");
            Console.WriteLine("Номер телефону - 5");
            Console.WriteLine("Назад - 0");

            Console.Write("Ваш вибiр: ");
            int k = Convert.ToInt32(Console.ReadLine());

            if (k == 1) patients.Sort((a, b) => a.name.CompareTo(b.name));
            else if (k == 2) patients.Sort((a, b) => a.surname.CompareTo(b.surname));
            else if (k == 3) patients.Sort((a, b) => a.age.CompareTo(b.age));
            else if (k == 4) patients.Sort((a, b) => a.address.CompareTo(b.address));
            else if (k == 5) patients.Sort((a, b) => a.phonenumber.CompareTo(b.phonenumber));
            else if (k == 0) return;


            Console.WriteLine("+--------------------+----------------------+--------+---------------------------------------------+--------------------------+");
            Console.WriteLine("|        Iм'я        |       Прiзвище       |  Вiк   |              Адреса проживання              |      Номер телефону      |");
            Console.WriteLine("+--------------------+----------------------+--------+---------------------------------------------+--------------------------+");
            foreach (Patient item in patients)
            {
                Console.WriteLine(String.Format("| {0,-18} | {1,-20} | {2,-6} | {3,-43} | {4,-24} |", item.name, item.surname, item.age, item.address, item.phonenumber));
            }
            Console.WriteLine("+--------------------+----------------------+--------+---------------------------------------------+--------------------------+");
        }

        static void Main(string[] args)
        {
            List<Patient> patients = ReadBD();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Оберiть: ");
                Console.WriteLine("Додати запис - 1");
                Console.WriteLine("Редагувати запис - 2");
                Console.WriteLine("Видалити запис - 3");
                Console.WriteLine("Сортування - 4");
                Console.WriteLine("Вийти - 0");

                Console.Write("Ваш вибiр: ");
                int k = Convert.ToInt32(Console.ReadLine());

                if (k == 1) AddPatient(patients);
                else if (k == 2) EditPatient(patients);
                else if (k == 3) RemovePatient(patients);
                else if (k == 4) SortPatients(patients);
                else if (k == 0) break;
            }
        }
    }
}
