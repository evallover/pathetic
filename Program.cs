namespace ReStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Start();
        }
        static public void Start()
        {
            Check_DB();
            Check_Goods_DB();
            string shop_name = "Добро пожаловать в ReStore!";
            Console.SetCursorPosition(Console.BufferWidth / 2 - shop_name.Length / 2, 0);
            Console.WriteLine(shop_name);
            for (int i = 0; i < Console.BufferWidth; i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }

            Console.WriteLine("  Login: ");
            Console.WriteLine("  Password: ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("  Log in");
            Console.ForegroundColor = ConsoleColor.White;

            Console.SetCursorPosition(0, Class_arrow.y);
            Console.WriteLine(Class_arrow.arrow);
            Class_arrow.arrov_max = 4;

            Class_arrow.Draw_Arrow();
        }
        static private void Check_DB()
        {
            if (!File.Exists(Class_save.path + "\\users.json"))
            {
                File.AppendAllText(Class_save.path + "\\users.json", "");

                List<User> users = new();
                User user_info = new();
                user_info.id_table = 0;
                user_info.login_table = "Admin";
                user_info.password_table = "Admin";
                user_info.role_table = "Admin";
                users.Add(user_info);
                Class_save.Serialize(users, "users.json");
            }
        }
        static private void Check_Goods_DB()
        {
            if (!File.Exists(Class_save.path + "\\goods.json"))
            {
                File.AppendAllText(Class_save.path + "\\goods.json", "");

                Staff staff_info = new Staff();
                List<Staff> goods = new();
                staff_info.id_table = 0;
                staff_info.name_table = "chocolate";
                staff_info.price_table = 99.99;
                staff_info.amount_table = 100;
                goods.Add(staff_info);
                Class_save.Serialize(goods, "goods.json");
            }
        }
    }
}