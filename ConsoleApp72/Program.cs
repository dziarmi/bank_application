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
                Console.WriteLine("Choose an option!");
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
                        Console.WriteLine("Goodbye :)");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option!");
                        break;
                }
            }

        }

        static void CreateAccount(List<User> users)
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your login: ");
            string login = Console.ReadLine();

            bool loginExists = users.Exists(u => u.HasLogin(login));

            if (loginExists)
            {
                Console.WriteLine("This login already exists! Please choose another one.");
                return;
            }

            Console.Write("Enter password: ");
            string password = Console.ReadLine();
            Console.Write("Confirm password: ");
            string confirm = Console.ReadLine();

            if (confirm == password)
            {
                User newuser = new User(name, login, password);
                users.Add(newuser);
                Console.WriteLine("Account created!");
            }
            else
            {
                Console.WriteLine("Passwords don't match!");
            }
        }

        static void Login(List<User> users)
        {
            Console.Write("Enter login: ");
            string login = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            User foundUser = users.Find(u => u.CheckCredentials(login, password));

            if (foundUser != null)
            {
                Console.WriteLine("Welcome back, " + foundUser.GetName());
                foundUser.ManageBankAccount();
            }
            else
            {
                Console.WriteLine("Wrong login or password!");
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

            public bool HasLogin(string login)
            {
                return Login == login;
            }

            public string GetName()
            {
                return Name;
            }


            public void ManageBankAccount()
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
                            Console.WriteLine("Invalid option!");
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
                Console.WriteLine("Your current balance: " + Balance + " USD.");
            }

            public void Deposit()
            {
                Console.Write("Enter amount to deposit: ");
                decimal deposit = Convert.ToDecimal(Console.ReadLine());

                if (deposit <= 0)
                {
                    Console.WriteLine("Invalid amount.");
                }
                else
                {
                    Balance += deposit;
                    Console.WriteLine("Your current balance: " + Balance + " USD.");
                }
            }

            public void Withdraw()
            {
                Console.Write("Enter amount to withdraw: ");
                decimal withdraw = Convert.ToDecimal(Console.ReadLine());

                if (Balance < withdraw)
                {
                    Console.WriteLine("You dont have enough balance!");
                }
                else if (withdraw <= 0)
                {
                    Console.WriteLine("Invalid amount.");
                }
                else
                {
                    Balance -= withdraw;
                    Console.WriteLine("Your current balance: " + Balance + " USD.");
                }

            }
        }

    }
}
