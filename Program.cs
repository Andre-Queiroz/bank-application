using System;
using System.Collections.Generic;
using System.Linq;

namespace console_bank_application
{
    class Program
    {
        static List<Account> AccountList = new List<Account>();
        static void Main(string[] args)
        {
            string option;
            do
            {
                option = Menu();
                switch (option)
                {
                    case "1":
                        ListAccounts();
                        break;
                    case "2":
                        CreateAccount();
                        break;
                    case "3":
                        TransferFromAccount();
                        break;
                    case "4":
                        WithdrawFromAccount();
                        break;
                    case "5":
                        DepositInAccount();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    case "X":
                        Console.WriteLine("Thank you for using our services!\n");
                        break;
                    default:
                        Console.WriteLine("Invalid Option\n");
                        break;
                }

            } while (option != "X");
        }

        private static string Menu()
        {
            Console.WriteLine("\n[1] List Accounts");
            Console.WriteLine("[2] Create Account");
            Console.WriteLine("[3] Transfer");
            Console.WriteLine("[4] Withdraw");
            Console.WriteLine("[5] Deposit");
            Console.WriteLine("[c] Clear Screen");
            Console.WriteLine("[x] Exit Application\n");
            Console.WriteLine("Please, insert the option:");

            return Console.ReadLine().Trim().ToUpper();
        }

        private static void ListAccounts()
        {
            if (AccountList.Count == 0)
            {
                Console.WriteLine("There are no registered accounts");
            }
            else
            {
                for (int i = 0; i < AccountList.Count; i++)
                {
                    Console.WriteLine(AccountList[i]);
                }

            }
        }

        private static void CreateAccount()
        {
            Console.WriteLine("Creating Account:");
            Console.WriteLine("[1] Individual Person (Default)");
            Console.WriteLine("[2] Legal Entity");
            string type = "";
            string name = "";

            try
            {
                type = Console.ReadLine().Trim();
                if (type != "1" && type != "2")
                {
                    type = "1";
                }
            }
            catch (Exception)
            {
                throw new Exception();
            }

            try
            {
                Console.WriteLine("Name:");
                name = Console.ReadLine().Trim();
            }
            catch (Exception)
            {
                throw new Exception();
            }

            Random AccountNumber = new Random();
            int number = AccountNumber.Next(1000, 9999);

            Console.WriteLine($"Your account number is {number}");
            Console.WriteLine("Your current balance is BRL 0.00 and the default " +
                              "overdraft amount is BRL 100.00");
            Console.WriteLine("Thank you for becoming our customer!");
            Account NewAccount = new Account((AccountType)int.Parse(type), 0.00, -100.00, name, number);
            AccountList.Add(NewAccount);
        }
        private static void TransferFromAccount()
        {
            int originAccount = -1;
            int destinationAccount = -1;
            try
            {
                Console.WriteLine("Your Account Number:");
                originAccount = int.Parse(Console.ReadLine().Trim().Substring(0, 4));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid account number");
            }

            try
            {
                Console.WriteLine("Destination account Number:");
                destinationAccount = int.Parse(Console.ReadLine().Trim().Substring(0, 4));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid account number");
            }

            int originIndex = FindAccountByNumber(originAccount); // -1 if did not find the account
            int destinationIndex = FindAccountByNumber(destinationAccount); // -1 if did not find the account
            if (originAccount != -1 && destinationAccount != -1)
            {
                Console.WriteLine("Enter the amount to be transferred: (just numbers)");
                try
                {
                    double transferValue = double.Parse(Console.ReadLine());
                    AccountList[originIndex].Transfer(transferValue, AccountList[destinationIndex]);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Something went wrong, we apologize");
                }
            }
        }
        private static void WithdrawFromAccount()
        {
            int accountNumber = -1;
            try
            {
                Console.WriteLine("Your Account Number:");
                accountNumber = int.Parse(Console.ReadLine().Trim().Substring(0, 4));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid account number");
            }

            int index = FindAccountByNumber(accountNumber); // -1 if did not find the account
            if (index != -1)
            {
                Console.WriteLine("Enter the amount to be withdrawn: (just numbers)");
                try
                {
                    double withdrawValue = double.Parse(Console.ReadLine());
                    AccountList[index].Withdraw(withdrawValue);
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong, we apologize");
                }
            }
        }

        private static void DepositInAccount()
        {
            int accountNumber = -1;
            try
            {
                Console.WriteLine("Your Account Number:");
                accountNumber = int.Parse(Console.ReadLine().Trim().Substring(0, 4));
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid account number");
            }

            int index = FindAccountByNumber(accountNumber); // -1 if did not find the account
            if (index != -1)
            {
                Console.WriteLine("Enter the amount to be deposited: (just numbers)");
                try
                {
                    double depositValue = double.Parse(Console.ReadLine());
                    AccountList[index].Deposit(depositValue);
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong, we apologize");
                }
            }
        }

        /**
         * Returns the account index.
         * If the function did not find the account, it will return -1
         */
        private static int FindAccountByNumber(int accountNumber)
        {
            try
            {
                int index = AccountList.FindIndex(item => item.GetNumber() == accountNumber);
                return index;
            }
            catch (Exception)
            {
                Console.WriteLine("Account not found");
            }
            return -1;
        }
    }
}
