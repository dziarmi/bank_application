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

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm password: ");
            string confirm = Console.ReadLine();

            if (password == confirm)
            {
                Console.WriteLine("Account created!");
                User newUser = new User(name, login, password);
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

            public User(string name, string login, string password)
            {
                Name = name;
                Login = login;
                Password = password;

                account = new BankAccount();
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
                    Console.WriteLine("4. Logout.");
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
            private decimal Balance { get; set; }

            public BankAccount()
            {
                Balance = 1000;
            }

            public void ShowBalance()
            {
                Console.WriteLine("Your current balance: " + Balance + " USD");
            }

            public void Deposit()
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

            public void Withdraw()
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
    }
}