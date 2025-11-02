namespace ConsoleApp72
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<User> users = new List<User>();
            Console.WriteLine("Welcome to Bank Application!");

            bool running = true;

            while (running)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Create account.");
                Console.WriteLine("2. Login to existing account.");
                Console.WriteLine("3. Exit.");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount(users);
                        break;
                    case "2":
                        Login(users);
                        break;
                    case "3":
                        Console.WriteLine("Goodbye!");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        break;
                }
            }
        }
        static void CreateAccount(List<User> users)
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine();
            Console.Write("Enter login: ");
            string login = Console.ReadLine();

            bool accountExist = false;

            foreach (User u in users)
            {
                if (u.LoginExist(login))
                {
                    accountExist = true;
                    break;
                }
            }

            if (accountExist)
            {
                Console.WriteLine("Account with this login already exists!");
                return;
            }

            Console.WriteLine("Choose account type:");
            Console.WriteLine("1. Business Account.");
            Console.WriteLine("2. Savings Account");
            string accountType = Console.ReadLine();
            BankAccount account;

            if (accountType == "1")
            {
                account = new BusinessAccount();
            }
            else if (accountType == "2")
            {
                account = new SavingsAccount();
            }
            else
            {
                Console.WriteLine("Invalid option.");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm password: ");
            string confirm = Console.ReadLine();

            if (password == confirm)
            {
                Console.WriteLine("Account created!");
                User newUser = new User(name, login, password, account);
                users.Add(newUser);
            }
            else
            {
                Console.WriteLine("Passwords don't match.");
            }
        }

        static void Login(List<User> users)
        {
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            User found = null;

            foreach (User u in users)
            {
                if (u.CheckCredentials(login, password))
                {
                    found = u;
                    break;
                }
            }

            if (found != null)
            {
                Console.WriteLine("Logged in!");
                found.AccountManagement();
            }
            else
            {
                Console.WriteLine("Wrong login or password.");
            }
        }

        class User
        {
            private string Name { get; set; }
            private string Login { get; set; }
            private string Password { get; set; }
            private BankAccount account { get; set; }

            public User(string name, string login, string password, BankAccount acc)
            {
                Name = name;
                Login = login;
                Password = password;
                account = acc;
            }

            public bool CheckCredentials(string login, string password)
            {
                return Login == login && Password == password;
            }

            public bool LoginExist(string login)
            {
                return Login == login;
            }

            public void AccountManagement()
            {
                bool inAccount = true;

                while (inAccount)
                {
                    Console.WriteLine("Bank menu:");
                    Console.WriteLine("1. Show balance.");
                    Console.WriteLine("2. Deposit.");
                    Console.WriteLine("3. Withdraw.");
                    Console.WriteLine("4. Add interest (only for savings account).");
                    Console.WriteLine("5. Logout.");
                    string choice = Console.ReadLine();

                    switch (choice)
                    {
                        case "1":
                            account.ShowBalance();
                            break;
                        case "2":
                            account.Deposit();
                            break;
                        case "3":
                            account.Withdraw();
                            break;
                        case "4":
                            if(account is SavingsAccount savings)
                            {
                                savings.AddInterestRate();
                            }
                            else
                            {
                                Console.WriteLine("This option is available only for Savings Accounts.");
                            }
                            break;
                        case "5":
                            Console.WriteLine("Logged out.");
                            inAccount = false;
                            break;
                        default:
                            Console.WriteLine("Invalid option.");
                            break;
                    }
                }
            }
        }

        class BankAccount
        {
            protected decimal Balance { get; set; }

            public BankAccount()
            {
                Balance = 1000;
            }

            public virtual void ShowBalance()
            {
                Console.WriteLine("Your current balance: " + Balance + " USD");
            }

            public virtual void Deposit()
            {
                Console.Write("Amount you want to deposit: ");
                decimal deposit = Convert.ToDecimal(Console.ReadLine());

                if (deposit <= 0)
                {
                    Console.WriteLine("Invalid amount!");
                }
                else
                {
                    Balance += deposit;
                    Console.WriteLine("Your current balance: " + Balance + " USD");
                }
            }

            public virtual void Withdraw()
            {
                Console.Write("Amount you want to withdraw: ");
                decimal withdraw = Convert.ToDecimal(Console.ReadLine());

                if (withdraw > Balance)
                {
                    Console.WriteLine("You don't have enough balance.");
                }
                else if (withdraw <= 0)
                {
                    Console.WriteLine("Invalid amount!");
                }
                else
                {
                    Balance -= withdraw;
                    Console.WriteLine("Your current balance: " + Balance + " USD");
                }
            }
        }

        class BusinessAccount : BankAccount
        {
            private decimal WithdrawFee = 5;

            public override void Withdraw()
            {
                Console.Write("Amount you want to withdraw: ");
                decimal withdraw = Convert.ToDecimal(Console.ReadLine());

                decimal total = withdraw + WithdrawFee;

                if(total > Balance)
                {
                    Console.WriteLine("You don't have enough balance.");
                }
                else if(total <= 0)
                {
                    Console.WriteLine("Invalid amount!");
                }
                else
                {
                    Balance -= total;
                    Console.WriteLine("Your current balance: " + Balance + " USD.");
                }
            }
        }

        class SavingsAccount : BankAccount
        {
            private decimal InterestRate = 0.03m;

            public void AddInterestRate()
            {
                decimal interest = Balance * InterestRate;
                Balance += interest;
                Console.WriteLine("Interest added: " + interest + " USD. Current balance: " + Balance);
            }
        }
    }
}