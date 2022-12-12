using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ATM_Assignment
{
    public class Bank
    {
        private int bankCapacity;
        private BankAccount[] all_acount;
        public int NumberOfCustomers = 0;
        public Bank(int bankCapacity)
        {
            if (bankCapacity <= 0)
            {
                bankCapacity = 10;
            }
            all_acount = new BankAccount[bankCapacity];
            this.bankCapacity = bankCapacity;
        }

        public Bank()
        {
            bankCapacity = 10;
            all_acount = new BankAccount[bankCapacity];
        }

       

        public void AddNewAccount(BankAccount a)
        {
            all_acount[NumberOfCustomers] = a;
            NumberOfCustomers++;
           
        }

        public bool IsBankUser(string cardNumber, string pinCode)
        {

            for (int i = 0; i < NumberOfCustomers; i++)
            {
                if (all_acount[i].is_bank_user(cardNumber, pinCode))
                {
                    return true;
                }
            }
            return false;
        }

        public int CheckBalance(string card, string pin)
        {
            bool are_in_bank = false;
            int b = 0;
            int x = 0;
            while (x < NumberOfCustomers)
            {
                if (all_acount[x].is_bank_user(card, pin))
                {
                    are_in_bank = true;
                   b=all_acount[x].AccountBalance;

                }
                x++;
            }
            if (!are_in_bank)
            {
                Console.WriteLine("The acount you ask for not in the bank");
            }
            return b;
        }

        public void Withdraw(BankAccount account, int amount)
        {
            bool are_in_bank = false;
            int x = 0;
            while (x < NumberOfCustomers)
            {
                if (all_acount[x].is_bank_user(account.CardNumber, account.PinCode))
                {
                    if (amount <= all_acount[x].AccountBalance)
                    {
                        all_acount[x].AccountBalance = all_acount[x].AccountBalance- amount;
                         are_in_bank = true;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("The amount you ask for more then AccountBalance");
                        break;
                    }
                }
                x++;
            }
            if (!are_in_bank)
            {
                Console.WriteLine("The acount you ask for not in the bank");
            }
          
        }

        public void Deposit(BankAccount account, int amount)
        {
            bool are_in_bank = false;
            int x = 0;
            while (x < NumberOfCustomers)
            {
                if (all_acount[x].is_bank_user(account.CardNumber, account.PinCode))
                {
                   all_acount[x].AccountBalance = all_acount[x].AccountBalance + amount;
                    are_in_bank = true;
                    break;
                   
                }
                x++;
            }
            if (!are_in_bank)
            {
                Console.WriteLine("The acount you ask for not in the bank");
            }
        }

        public void Save()
        {
            // public BankAccount(Person obj, string m, string card_Number, string pin_Code, int account_Balance)
            string current_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all.txt");
            string []Lines = new string[NumberOfCustomers];
            for (int i = 0; i < NumberOfCustomers; i++)
            {
                BankAccount a = all_acount[i];
                Lines[i]= a.Person_user_bank.First_name + "\t" + a.Person_user_bank.Last_name + "\t" + a.Email + "\t"   + a.CardNumber + "\t" + a.PinCode + "\t" + a.AccountBalance;
            }
            File.WriteAllLines(current_path, Lines);
        }

        public void Load()
        {
            string current_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "all.txt");
            string[] Lines = File.ReadAllLines(current_path);
           
            bankCapacity = Lines.Length;
            all_acount = new BankAccount[bankCapacity];
            NumberOfCustomers = 0;
            foreach (string L in Lines)
            {
                string []obj=L.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                Console.WriteLine(L);
                Person person = new Person();
                if (obj[0] != null)
                {
                    person.First_name = obj[0];

                }

                if (obj[1] != null)
                {
                    person.Last_name = obj[1];
                }
               
                BankAccount ac = new BankAccount(person, obj[2], obj[3], obj[4], int.Parse(obj[5]));
                all_acount[NumberOfCustomers] = ac;
                NumberOfCustomers++;

            }
        }
    }
}