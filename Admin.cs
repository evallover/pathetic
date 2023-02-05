
namespace ReStore
{
    internal class Admin : ICrud
    {
        static private List<User> users = Class_save.Deserialize<List<User>>("users.json");
        static private List<string> users_str = new();
        static public User new_u;
        static private int index;

        static private int id;
        static private string login_new = "";
        static private string password_new = "";
        static private string role_new = "";

        static public int id_ed;
        static public string login_ed = "";
        static public string password_ed = "";
        static public string role_ed = "";

        static public void Start(int y)
        {
            List<User> users = Class_save.Deserialize<List<User>>("users.json");
            Admin adm = new();
            Console.Clear();
            adm.Read(y);
            Draw();
        }
        static public void Draw()
        {
            string user_name = $"Добро пожаловать в ReStore, {Class_Log_in.login}!, \tРоль: {Class_Log_in.role}";
            Console.SetCursorPosition(Console.BufferWidth / 2 - user_name.Length / 2, 0);
            Console.WriteLine(user_name);
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 2; i < 10; i++)
            {
                Console.SetCursorPosition(90, i);
                Console.WriteLine("|");
            }
        }

        public void Create(int y)
        {
            User user_info = new();
            Draw();

            Draw_roles();

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("  ID:");
            Console.WriteLine("  Login:");
            Console.WriteLine("  Password:");
            Console.WriteLine("  Role:");
            Console.WriteLine("  Сохранить");

            switch (y)
            {
                case 2:
                    Console.SetCursorPosition(5, y);
                    try
                    {
                        id = Convert.ToInt32(Console.ReadLine());
                        foreach (string elem in users_str)
                        {
                            if (id.ToString() == elem)
                            {
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("ID занят");
                                Thread.Sleep(1000);
                                Console.Clear();
                                Create(y);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine(ex.Message + "Походу не цифра :(");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Create(y);
                    }
                    break;
                case 3:
                    Console.SetCursorPosition(8, y);
                    try
                    {
                        login_new = Console.ReadLine();
                        foreach (string elem in users_str)
                        {
                            if (login_new == elem)
                            {
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("Логин занят");
                                Thread.Sleep(1000);
                                Console.Clear();
                                Create(y);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(1000);
                        Console.Clear();
                        Create(y);
                    }
                    break;
                case 4:
                    Console.SetCursorPosition(11, y);
                    try
                    {
                        password_new = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(1000);
                        Console.Clear();
                        Create(y);
                    }
                    break;
                case 5:
                    Console.SetCursorPosition(7, y);
                    try
                    {
                        int role = Convert.ToInt32(Console.ReadLine());
                        switch (role)
                        {
                            case 0:
                                role_new = Roles.Admin.ToString();
                                break;
                            case 1:
                                role_new = Roles.Kassir.ToString();
                                break;
                            case 2:
                                role_new = Roles.HR.ToString();
                                break;
                            case 3:
                                role_new = Roles.Sklad.ToString();
                                break;
                            case 4:
                                role_new = Roles.Buhgalter.ToString();
                                break;
                            default:
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("Неверное число!");
                                Create(y);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Thread.Sleep(1000);
                        Console.Clear();
                        Create(y);
                    }
                    break;
                case 6:
                    user_info.id_table = id;
                    user_info.login_table = login_new;
                    user_info.password_table = password_new;
                    user_info.role_table = role_new;
                    users.Add(user_info);
                    Class_save.Serialize(users, "users.json");
                    Class_arrow.page = 0;
                    Class_arrow.deep = 1;
                    Start(y);
                    break;
            }
        }

        public void Delete()
        {
            users.RemoveAt(index);
            Class_save.Serialize(users, "users.json");
            Console.Clear();
            Class_arrow.page = 0;
            Start(3);
        }

        public void Read(int y)
        {
            users_str.Clear();
            users.Clear();
            users = Class_save.Deserialize<List<User>>("users.json");

            int next = 0;
            int count = 0;
            foreach (User user in users)
            {
                users_str.Add(user.id_table.ToString());
                users_str.Add(user.login_table);
                users_str.Add(user.password_table);
                users_str.Add(user.role_table);
            }

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\t\tID\t\tЛогин\t\tПароль\t\tРоль");
            foreach (string elem in users_str)
            {
                Console.Write("\t\t" + elem);
                next++;
                if (next % 4 == 0)
                {
                    Console.WriteLine(" ");
                    count++;
                }
            }
            Class_arrow.arrov_max = Class_arrow.min + count - 1;

            Console.SetCursorPosition(92, 3);
            Console.WriteLine("F1 - добавить");
            Console.SetCursorPosition(92, 4);
            Console.WriteLine("F2 - поиск");
            Console.SetCursorPosition(92, 6);
            Console.WriteLine("F3 - авторизация");
        }

        public void Update(int y)
        {
            index = y - Class_arrow.min - 1;
            if (y == 2)
            {
                index++;
            }
            new_u = users.ElementAt(index);
            Draw();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine($"  ID:{new_u.id_table}");
            Console.WriteLine($"  Login:{new_u.login_table}");
            Console.WriteLine($"  Password:{new_u.password_table}");
            Console.WriteLine($"  Role:{new_u.role_table}");

            Draw_roles();

            Console.SetCursorPosition(92, 10);
            Console.WriteLine("F10 - изменить");
            Console.SetCursorPosition(92, 9);
            Console.WriteLine("Del - удалить");
            Console.SetCursorPosition(92, 8);
            Console.WriteLine("s - сохранить");
        }
        public void Edit(int y)
        {
            switch (y)
            {
                case 2:
                    Console.SetCursorPosition(5, y);
                    try
                    {
                        id_ed = Convert.ToInt32(Console.ReadLine());
                        foreach (string elem in users_str)
                        {
                            if (id_ed.ToString() == elem)
                            {
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("ID занят");
                                Thread.Sleep(1000);
                                Console.Clear();
                                Update(y);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine(ex.Message + "Походу не цифра :(");
                        Thread.Sleep(1000);
                        Console.Clear();
                        Update(y);
                    }
                    break;
                case 3:
                    Console.SetCursorPosition(8, y);
                    try
                    {
                        login_ed = Console.ReadLine();
                        foreach (string elem in users_str)
                        {
                            if (login_ed == elem)
                            {
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("Логин занят");
                                Thread.Sleep(1000);
                                Console.Clear();
                                Update(y);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 4:
                    Console.SetCursorPosition(11, y);
                    try
                    {
                        password_ed = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
                case 5:
                    Console.SetCursorPosition(7, y);
                    try
                    {
                        int role = Convert.ToInt32(Console.ReadLine());
                        switch (role)
                        {
                            case 0:
                                role_ed = Roles.Admin.ToString();
                                break;
                            case 1:
                                role_ed = Roles.Kassir.ToString();
                                break;
                            case 2:
                                role_ed = Roles.HR.ToString();
                                break;
                            case 3:
                                role_ed = Roles.Sklad.ToString();
                                break;
                            case 4:
                                role_ed = Roles.Buhgalter.ToString();
                                break;
                            default:
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine("Неверное число");
                                Update(y);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    break;
            }
        }

        static public void Update_save()
        {
            if (role_ed.Length <= 0)
            {
                role_ed = new_u.role_table;
            }

            for (int i = 0; i < 3; i++)
            {
                if (login_ed.Length <= 0)
                {
                    login_ed = new_u.login_table;
                }
                else if (password_ed.Length <= 0)
                {
                    password_ed = new_u.password_table;
                }
                else if (id_ed == 0)
                {
                    id_ed = new_u.id_table;
                }
            }

            new_u.id_table = id_ed;
            new_u.login_table = login_ed;
            new_u.password_table = password_ed;
            new_u.role_table = role_ed;
            users.Add(new_u);
            users.RemoveAt(index);
            Class_save.Serialize(users, "users.json");
        }

        static public void Search_Draw()
        {
            Console.Clear();
            Draw();
            Console.SetCursorPosition(0, 2);
            Console.WriteLine("Выберите, по какому пункту делать поиск: ");
            Console.SetCursorPosition(0, 3);
            Console.WriteLine("  ID:");
            Console.WriteLine("  Login:");
            Console.WriteLine("  Password:");
            Console.WriteLine("  Role:");
        }
        static public void Search(int y)
        {
            Search_Draw();
            switch (y)
            {
                case 3:
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите id:");
                    int search_id = Convert.ToInt32(Console.ReadLine());
                    Search_elem("id_table", search_id);
                    break;
                case 4:
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите login:");
                    string search = Console.ReadLine();
                    Search_elem("login_table", search);
                    break;
                case 5:
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите password:");
                    search = Console.ReadLine();
                    Search_elem("password_table", search);
                    break;
                case 6:
                    Console.SetCursorPosition(0, 7);
                    Console.WriteLine("Введите role:");
                    search = Console.ReadLine();
                    Search_elem("role_table", search);
                    break;
                default:
                    Console.WriteLine("Неверное число");
                    break;
            }
        }

        static private void Search_elem<T>(string search_item, T search_el)
        {
            Console.Clear();
            Draw();
            users_str.Clear();
            users.Clear();
            users = Class_save.Deserialize<List<User>>("users.json");
            int next = 0;
            int count = 0;
            foreach (User user in users)
            {
                var a = user.GetType().GetFields().FirstOrDefault(x => x.Name == search_item);
                if (String.Equals(a?.GetValue(user), search_el.ToString()))
                {
                    users_str.Add(user.id_table.ToString());
                    users_str.Add(user.login_table);
                    users_str.Add(user.password_table);
                    users_str.Add(user.role_table);
                }
            }

            Console.SetCursorPosition(0, 2);
            Console.WriteLine("\t\tID\t\tЛогин\t\tПароль\t\tРоль");
            foreach (string elem in users_str)
            {
                Console.Write("\t\t" + elem);
                next++;
                if (next % 4 == 0)
                {
                    Console.WriteLine(" ");
                    count++;
                }
            }
            Class_arrow.arrov_max = Class_arrow.min + count - 1;

            Console.SetCursorPosition(92, 3);
            Console.WriteLine("F1 - добавить");
            Console.SetCursorPosition(92, 4);
            Console.WriteLine("F2 - поиск");
        }

        static private void Draw_roles()
        {
            Console.SetCursorPosition(92, 2);
            Console.WriteLine("0. Администратор");
            Console.SetCursorPosition(92, 3);
            Console.WriteLine("1. Кассир");
            Console.SetCursorPosition(92, 4);
            Console.WriteLine("2. Кадровик");
            Console.SetCursorPosition(92, 5);
            Console.WriteLine("3. Склад-менеджер");
            Console.SetCursorPosition(92, 6);
            Console.WriteLine("4. Бухгалтер");
            Console.SetCursorPosition(92, 8);
            Console.WriteLine("Esc - выход в меню");
        }
    }
}
