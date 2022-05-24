using System;

namespace RegistrationCertificate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Menu();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                    Console.WriteLine($">>> Помилка! {ex.InnerException.Message}\n");
                else
                    Console.WriteLine($">>> Помилка! {ex.Message}\n");
            }
        }

        static private int Print()
        {
            while (true)
            {
                string strMenu = "\n ========== Виберiть операцiю ==========\n"
                               + "   1. Вивести контейнер\n"
                               + "   2. Додати елемент до контейнеру\n"
                               + "   3. Редагувати елемент у контейнерi\n"
                               + "   4. Видалити елемент з контейнеру за ID\n"
                               + "   5. Пошук у контейнерi\n"
                               + "   6. Сортування контейнеру\n"
                               + "   0. Вихiд\n"
                               + " ==========================================\n";
                Console.Write(strMenu);
                int action;
                try
                {
                    action = int.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine(">>> Помилка! Ви повиннi ввести натуральне число");
                    continue;
                }
                if (action >= 0 || action <= 6) return action;
            }
        }

        static private void Menu()
        {
            while (true)
            {
                Console.WriteLine("Введiть назву файлу: ");
                string choice = Console.ReadLine();

                if (choice == "certificates.json")
                {
                    Menu<Certificate> menu = new Menu<Certificate>();
                    Container<Certificate> container = new Container<Certificate>("certificates.json");
                    while (true)
                    {
                        int action = Print();
                        if (action == 0) break; 
                        try
                        {
                            menu.dictionaryActions[action](container);
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException != null)
                                Console.WriteLine($">>> Помилка! {ex.InnerException.Message}");
                            else
                                Console.WriteLine($">>> Помилка! {ex.Message}");
                        }
                    }
                }
                else if (choice == "jewelry.json")
                {
                    Menu<Jewelry> menu = new Menu<Jewelry>();
                    Container<Jewelry> container = new Container<Jewelry>("jewelry.json");
                    while (true)
                    {
                        int action = Print();
                        if (action == 0) break;
                        try
                        {
                            menu.dictionaryActions[action](container);
                        }
                        catch (Exception ex)
                        {
                            if (ex.InnerException != null)
                                Console.WriteLine($">>> Помилка! {ex.InnerException.Message}");
                            else
                                Console.WriteLine($">>> Помилка! {ex.Message}");
                        }
                    }
                }
                else
                    continue;
            }
        }
    }
}
