
namespace ReStore
{
    internal class Class_Log_in
    {
        static public string login = "";
        static public string role = "";
        static private string _password_check = "";
        static public void Log_in(int y)
        {
            List<string> logs = new List<string>();
            List<string> pass = new List<string>();

            bool Correct_login = false;
            bool Correct_password = false;

            if (y == 2)
            {
                Console.SetCursorPosition(8, y);
                try
                {
                    login = Console.ReadLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            else if (y == 3)
            {
                HideCharacter(y);
            }
            else
            {
                List<User> users = Class_save.Deserialize<List<User>>("users.json");
                foreach (User user in users)
                {
                    logs.Add(user.login_table);
                    pass.Add(user.password_table);
                }
                foreach (string log in logs)
                {
                    if (login.Equals(log))
                    {
                        Correct_login = true;
                    }
                }
                foreach (string pas in pass)
                {
                    if (_password_check.Equals(pas))
                    {
                        Correct_password = true;
                    }
                }

                if (Correct_login && Correct_password)
                {
                    foreach (User user in users)
                    {
                        if (user.login_table.Equals(login) & user.password_table.Equals(_password_check))
                        {
                            role = user.role_table;
                        }
                    }
                    switch (role)
                    {
                        case "Admin":
                            Class_arrow.min = 3;
                            Admin.Start(y);
                            Class_arrow.deep = 1;
                            break;
                        case "Sklad":
                            Class_arrow.page = 2;
                            Class_arrow.min = 3;
                            Class_arrow.deep = 1;
                            Sklader.Start(y);
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Неверный логин или пароль");
                    Thread.Sleep(1500);
                    Console.Clear();
                    Program.Start();
                }
            }
        }

        public static void HideCharacter(int y)
        {
            int x = 11;
            string password = "";
            ConsoleKeyInfo keyInfo;

            while (true)
            {
                Console.SetCursorPosition(x, y);
                keyInfo = Console.ReadKey(true);
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (password.Length != 0)
                    {
                        password = password.Remove(password.Length - 1);
                        Console.Write("\b \b");
                        x--;
                    }
                }
                else
                {
                    password += keyInfo.KeyChar;
                    Console.Write("*");
                    x++;
                }
            }
            _password_check = password;
        }
    }
}
