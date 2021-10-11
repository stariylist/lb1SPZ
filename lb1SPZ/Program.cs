using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace lb1SPZ
{
    //структура депозитов содержит название банка, сумму депозита и годовой %
    struct dep
    {
        public string name;
        public double money;
        public double percent;
    }
    class Bank_Account
    {
        private string FIO; //имя пользователя
        private string account; // номер счета
        private List<dep> deposite = new List<dep>(); //вместо массива депозитов
        private double balance; //сумма на балансе
        //инициализирующий конструктор
        public Bank_Account(string FIO, string account, double balance)
        {
            //проверка на корректность задания баланса
            if (balance < 0)//если баланс меньше 0
            {
                Console.WriteLine("Ваш баланс отрицательный! ПОВЕРНIТЬ ГРОШI БАНКУ!!!");
                Console.Read();
                Environment.Exit(0);
            }
            else // если баланс положительный, то присваеваем переменным значения
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
            dep deposite = new dep();//создаем структуру
            deposite.money = money;
            deposite.percent = percent;
            deposite.name = name;
            this.deposite.Add(deposite);//заносим в структуру все переменные
        }
        //начисление процентов на депозит за год
        public void percent_by_year()
        {
            for (int i = 0; i < deposite.Count; i++)//просто цикл для структуры с выводом информации
            {
                Console.WriteLine("Из депозита в " + deposite[i].money + " y.e. с процентной ставкой " + deposite[i].percent + "% за год вы получите " + (deposite[i].money * deposite[i].percent / 100 + deposite[i].money) + " y.e. Банк - " + deposite[i].name + "\n");
            }
        }
        //метод пополнения баланса
        public void add_money(double money)
        {
            this.balance += money;//прибавляем деньги именно к балансу
            Console.WriteLine("~~~Вы положили " + money + "у.е. на баланс~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
        }
        //метод снятия денег с баланса
        public void sub_money(double money)
        {
            if (this.balance >= money)//если денег на балансе больше, чем хотим снять, то все ок
            {
                this.balance -= money;
                Console.WriteLine("~~~Вы сняли " + money + "у.е. с баланса~~~\nТеперь ваш баланс равен " + balance + "у.е.\n");
            }
            else//иначе выводим инфу о том, что денег недостаточно
            {
                Console.WriteLine("~~~Попытка снять " + money + "у.е. с баланса~~~\nУ вас недостаточно средств на балансе!\n");
            }
        }
        //удаление депозита т.е. перевод денег на баланс
        public void del_deposite(string name)
        {
            dep l = new dep();//создаем новую временную структуру
            for (int i = 0; i < deposite.Count(); i++)
            {
                if (deposite[i].name == name)//если в структуре депозит мы находим тот банк, что нужно
                {
                    l = deposite[i];//то заносим структуру во временную
                }
            }
            Console.WriteLine("~~~Вы сняли деньги с депозита банка " + l.name + "~~~\n\nТеперь ваш баланс равен " + (balance + l.money) + "у.е.\n");
            deposite.Remove(l);//удаляем
            this.balance += l.money;//прибавляем деньги с депозита на баланс
        }
        //подсчет всех денег на депозитах
        public void all_money()
        {
            double sum = 0;//переменная суммы
            for (int i = 0; i < deposite.Count(); i++)
            {
                sum += deposite[i].money;//просто в цикле считаем сумму всех депозитов структуры
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
            string FIO = Console.ReadLine();//ввод ФИО
            Console.Write("Введите ваш номер счёта: ");
            string account = Console.ReadLine();//ввод банковского счета
            //далее проверка ФИО на символы русского алфавита, номера счета на только цифры и длинну не более 8 символов
            //через валидатор Regex
            if (!Regex.IsMatch(FIO, @"^[а-яА-Я ]+$") || !Regex.IsMatch(account, @"^[0-9]+$") || account.Length != 8)
            {
                Console.WriteLine("Вы ввели некорректные данные!");
                Console.ReadLine();
                Environment.Exit(0);
            }
            Bank_Account acc = new Bank_Account(FIO, account, 23456.55);//создаем экземпляр класса с помощью инициализируещего конструктора
            Console.WriteLine(acc.ToString());//выводим инфу через переопределенный ToString()
            acc.add_deposite(5434, 9, "Privat");//создаем депозит
            acc.add_deposite(12145, 7, "PUMB");//создаем депозит
            acc.add_deposite(2377, 11, "Oshad");//создаем депозит
            acc.percent_by_year();//вывод инфу по процентам каждого банка
            acc.all_money();//общая сумма на депозитах
            acc.add_money(2150);//добавляем деньги на баланс
            acc.sub_money(6300);//снимаем деньги с баланса
            acc.sub_money(50000);//ПОПЫТКА снять деньги с баланса
            acc.del_deposite("Privat");//удаление депозита, деньги должны перейти на баланс
            acc.all_money();//проверка работы удаления депозита
            acc.del_deposite("Oshad");//то же, что и с приватом
            acc.all_money();//снова проверка
            Console.Read();
        }
    }
}
