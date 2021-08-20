using System;

namespace console_bank_application.Classes
{
    public class Account
    {
        private AccountType AccountType { get; set; }
        private double Balance { get; set; }
        private double Overdraft { get; set; }
        private string Name { get; set; }
        private int Number { get; set; }

        public Account(AccountType AccountType, double Balance, double Overdraft, string Name, int Number)
        {
            this.AccountType = AccountType;
            this.Balance = Balance;
            this.Overdraft = Overdraft;
            this.Name = Name;
            this.Number = Number;
        }

        public bool Withdraw(double value)
        {
            if (this.Balance - value < this.Overdraft)
            {
                Console.WriteLine("Insufficient balance to withdraw.");
                return false;
            }
            this.Balance -= value;
            Console.WriteLine($"{this.Name}'s current account balance is BRL {this.Balance:N2}");

            return true;
        }

        public void Deposit(double value)
        {
            this.Balance += value;
            Console.WriteLine($"{this.Name}'s current account balance is BRL {this.Balance:N2}");
        }

        public void Transfer(double value, Account destination)
        {
            if (Withdraw(value))
            {
                destination.Deposit(value);
            }
        }

        public int GetNumber()
        {
            return this.Number;
        }

        public override string ToString()
        {
            string info = $"\tNumber:\t\t{Number}\n";
            info += $"\tHolder:\t\t{this.Name}\n";
            info += $"\tType:\t\t{this.AccountType}\n";
            info += $"\tBalance:\tBRL {this.Balance}\n";
            info += $"\tOverdraft:\tBRL {this.Overdraft}\n";

            return info;
        }

    }
}
