using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lb1SPZ
{
    //структура депозитов содержит название банка, сумму и годовой %
    struct dep
    {
        public string name;
        public double money;
        public double percent;
    }
    class Bank_Account
    {
        private string FIO;
        private string account;
        private List<dep> deposite = new List<dep>(); //вместо массива депозитов
        private double balance;
        //инициализирующий конструктор
        public Bank_Account(string FIO, string account, double balance)
        {
            //проверка на корректность задания баланса
            if (balance < 0)
            {
                Console.WriteLine("Ваш баланс отрицательный! ПОВЕРНIТЬ ГРОШI БАНКУ!!!");
                Console.Read();
                Environment.Exit(0);
            }
            else
            {
                this.FIO = FIO;
                this.account = account;
                this.balance = balance;
            }
        }
        //переопределение метода ToString() -> возвращает нужную инфу о пользователе
        public override string ToString()
        {
            return "\nЗдравствуйте, " + FIO + ", ваш номер счета #" + account + ", ваш текущий баланс равен " + balance + "у.е.\n";
        }
        //метод добавления нового депозита в структуру
        public void add_deposite(double money, double percent, string name)
        {
            dep deposite = new dep();
            deposite.money = money;
            deposite.percent = percent;
            deposite.name = name;
            this.deposite.Add(deposite);
        }
        //начисление процентов на депозит за год
        public void percent_by_year()
        {
            for (int i = 0; i < deposite.Count; i++)
            {
                Console.WriteLine("Из депозита в " + deposite[i].money + " y.e. с процентной ставкой " + deposite[i].percent + "% за год вы получите " + (deposite[i].money * deposite[i].percent / 100 + deposite[i].money) + " y.e. Банк - " + deposite[i].name + "\n");
            }
        }
        //метод пополнения баланса
        public void add_money(double money)
        {
            this.balance += money;
            Console.WriteLine("~~~Вы положили " + money + "у.е. на баланс~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
        }
        //метод снятия денег с баланса
        public void sub_money(double money)
        {
            if (this.balance >= money)
            {
                this.balance -= money;
                Console.WriteLine("~~~Вы сняли " + money + "у.е. с баланса~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
            }
            else
            {
                Console.WriteLine("~~~Попытка снять " + money + "у.е. с баланса~~~\nУ вас недостаточно средств на балансе!\n");
            }
        }
        //удаление депозита т.е. перевод баблишка на баланс
        public void del_deposite(string name)
        {
            dep l = new dep();
            for (int i = 0; i < deposite.Count(); i++)
            {
                if (deposite[i].name == name)
                {
                    l = deposite[i];
                }
            }
            Console.WriteLine("~~~Вы сняли деньги с депозита банка " + l.name + "~~~\n\nТеперь ваш баланс равен " + (balance + l.money) + "у.е.\n");
            deposite.Remove(l);
            this.balance += l.money;
        }
        //подсчет всех денег на депозитах
        public void all_money()
        {
            double sum = 0;
            for (int i = 0; i < deposite.Count(); i++)
            {
                sum += deposite[i].money;
            }
            Console.WriteLine("Общее количество средств на депозитах равно " + sum + "у.е.\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("\t~~~Пожалуйста, для ввода ФИО используйте только буквы русской раскладки!~~~\n\t    ~~~Для ввода номера счета используйте только 8 цифр без пробелов!~~~\n\n");
            Console.Write("Введите ваше ФИО: ");
            string FIO = Console.ReadLine();
            Console.Write("Введите ваш номер счёта: ");
            string account = Console.ReadLine();
            if (!Regex.IsMatch(FIO, @"^[а-яА-Я ]+$") || !Regex.IsMatch(account, @"^[0-9]+$") || account.Length != 8)
            {
                Console.WriteLine("Вы ввели некорректные данные!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Bank_Account acc = new Bank_Account(FIO, account, 23456.55);
            Console.WriteLine(acc.ToString());
            acc.add_deposite(5434, 9, "Privat");
            acc.add_deposite(12145, 7, "PUMB");
            acc.add_deposite(2377, 11, "Oshad");
            acc.percent_by_year();
            acc.all_money();
            acc.add_money(2150);
            acc.sub_money(6300);
            acc.sub_money(50000);
            acc.del_deposite("Privat");
            acc.all_money();
            acc.del_deposite("Oshad");
            acc.all_money();
            Console.Read();
        }
    }
}
