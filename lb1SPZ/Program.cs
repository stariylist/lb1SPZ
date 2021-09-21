using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace lb1SPZ
{
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
        private List<dep> deposite = new List<dep>();
        private double balance;
        public Bank_Account(string FIO, string account, double balance)
        {
            if (!Regex.IsMatch(FIO, @"^[а-яА-Я ]+$")|| !Regex.IsMatch(account, @"^[0-9]+$") || account.Length!=8||balance<0)
            {
                Console.WriteLine("Вы ввели некорректные данные!");
                Environment.Exit(0);
            }
            else
            {
                this.FIO = FIO;
                this.account = account;
                this.balance = balance;
                Console.WriteLine("Здравствуйте, " + FIO + ", ваш номер счета #" + account + ", ваш текущий баланс равен " + balance + "у.е.\n");
            }
        }
        public void add_deposite(double money, double percent, string name)
        {
            dep deposite = new dep();
            deposite.money = money;
            deposite.percent = percent;
            deposite.name = name;
            this.deposite.Add(deposite);
        }

        public void percent_by_year()
        {
            for(int i = 0; i < deposite.Count;i++)
            {
                Console.WriteLine("Из депозита в " + deposite[i].money + " y.e. с процентной ставкой " + deposite[i].percent + "% за год вы получите " + (deposite[i].money * deposite[i].percent / 100 + deposite[i].money) + " y.e. Банк - "+deposite[i].name+"\n");
            }
        }
        public void add_money(double money)
        {
            this.balance += money;
            Console.WriteLine("~~~Вы положили " + money + "у.е. на баланс~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
        }
        public void sub_money(double money)
        {
            if (this.balance >= money)
            {
                this.balance -= money;
                Console.WriteLine("~~~Вы сняли "+money+ "у.е. с баланса~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
            }
            else
            {
                Console.WriteLine("У вас недостаточно средств на балансе!\n");
            }
        }

        public void del_deposite(string name)
        {
            dep l = new dep();
            for(int i = 0; i < deposite.Count(); i++)
            {
                if(deposite[i].name == name)
                {
                    l = deposite[i];
                }
            }
            Console.WriteLine("~~~Вы сняли деньги с депозита банка "+l.name+"~~~\n\nТеперь ваш баланс равен " + (balance + l.money) + "у.е.\n") ;
            deposite.Remove(l);
            this.balance += l.money;
        }
        public void all_money()
        {
            double sum = 0;
            for(int i = 0; i < deposite.Count(); i++)
            {
                sum += deposite[i].money;
            }
            Console.WriteLine("Общее количество средств на депозитах равно "+ sum+ "у.е.\n");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bank_Account acc = new Bank_Account("Осадчий Александр Александро5вич", "12347448", 23456);
            acc.add_deposite(5000, 9,"Privat");
            acc.add_deposite(10000, 7, "PUMB");
            acc.add_deposite(2377, 11, "Oshad");
            acc.percent_by_year();
            acc.all_money();
            acc.add_money(2000);
            acc.sub_money(5000);
            acc.del_deposite("Privat");
            acc.all_money();
            acc.del_deposite("Oshad");
            acc.all_money();
            Console.ReadLine();
        }
    }
}
