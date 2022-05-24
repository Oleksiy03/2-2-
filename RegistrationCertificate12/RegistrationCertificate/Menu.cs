using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RegistrationCertificate
{
    class Menu<Type> where Type : BaseClass, new()
    {
        private static void Add(Container<Type> container)
        {
            string errors = string.Empty;
            var data = EnterData(properties);
            Type item = new Type();

            foreach (PropertyInfo property in properties)
            {
                try
                {
                    property.SetValue(item, Convert.ChangeType(data[property.Name], property.PropertyType));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        errors += $">>> Помилка! {ex.InnerException.Message}\n";
                    else
                        errors += $">>> Помилка! {ex.Message}\n";
                }
            }
            if (errors != string.Empty) throw new BadModelException(errors);
            container.Add((Type)item);
        }

        private static void Edit(Container<Type> container)
        {
            Console.Write("Введiть id для редагування: ");
            int editId = Int32.Parse(Console.ReadLine());
            container.Edit(editId);
        }

        private static void Delete(Container<Type> container)
        {
            Console.Write("Введiть id для видалення: ");
            int deleteId = Int32.Parse(Console.ReadLine());
            container.Delete(deleteId);
        }

        private static void Search(Container<Type> container)
        {
            Console.Write("Введiть значення для пошуку: ");
            string searchValue = Console.ReadLine();
            container.Search(searchValue);
        }

        private static void Sort(Container<Type> container)
        {
            int index = -1;
            while (index < 0 || index >= properties.Length)
            {
                Console.WriteLine("Виберiть поле для сортування: ");
                for (int i = 0; i < properties.Length; i++)
                    Console.WriteLine($"  {i}. {properties[i].Name}");
                Console.WriteLine("  <. Повернутись назад");

                string action = Console.ReadLine();
                if (action == "<") return;
                try
                {
                    index = Convert.ToInt32(action);
                }
                catch
                {
                    Console.WriteLine(">>> Помилка! Ви повиннi ввести натуральне число!!!");
                }
            }

            Console.Write("Сортувати за зростанням чи за спаданням? (asc/desc): ");
            string sortType = string.Empty;
            while (sortType != "asc" && sortType != "desc")
                sortType = Console.ReadLine();

            if (sortType == "asc") container.Sort(properties[index].Name, "asc");
            else if (sortType == "desc") container.Sort(properties[index].Name, "desc");
        }

        private static Dictionary<string, string> EnterData(PropertyInfo[] properties)
        {
            Dictionary<string, string> data = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                Console.Write($"Введiть {property.Name}: ");
                data.Add(property.Name, Console.ReadLine());
            }

            return data;
        }

        public readonly Dictionary<int, Action> dictionaryActions = new Dictionary<int, Action>()
            {
                { 1, new Action(Print) }, { 2, new Action(Add) }, { 3, new Action(Edit) },
                { 4, new Action(Delete) },{ 5, new Action(Search) }, { 6, new Action(Sort) },
            };

        private static PropertyInfo[] properties;
        public delegate void Action(Container<Type> container);

        public Menu()
        {
            properties = typeof(Type).GetProperties();
        }

        private static void Print(Container<Type> container)
        {
            Console.WriteLine(container.ToString());
        }

    }
}
