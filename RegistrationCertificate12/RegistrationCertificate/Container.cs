using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RegistrationCertificate
{
    public class Container<Type> where Type : BaseClass, new()
    {
        private List<Type> container;
        private string fileName;

        public Container(string fileName)
        {
            container = new List<Type>();
            this.fileName = $"../../../{fileName}";
            ReadFromFile();
        }

        public void Add(Type item)
        {
            container.Add(item);
            UpdateFile();
        }

        public void Delete(int id)
        {
            try
            {
                var item = container.First(x => x.Id == id);
                container.Remove(item);
            }
            catch
            {
                Console.WriteLine(">>> Помилка! Об'єкт з таким ID не знайдено");
                return;
            }
            Console.WriteLine($"Об'єкт з ID = {id} було видалено");
            UpdateFile();
        }

        public void Sort(string property, string sort_type)
        {
            container = container
                .OrderBy(value => value.GetType().GetProperty(property)
                ?.GetValue(value))
                .ToList();

            if (sort_type == "desc")
                container.Reverse();

            UpdateFile();
        }

        public void Edit(int id)
        {
            while (true)
            {
                Type item = container.First(i => i.Id == id);
                if (item == null)
                {
                    Console.WriteLine("Об'єкт за даним ID не знайдено");
                    return;
                }

                var properties = typeof(Type).GetProperties();
                int index = this.EnterIntForMenuEditAction(properties);
                if (index == -1) return;

                Console.Write($"Введiть {properties[index].Name}: ");
                string newValue = Console.ReadLine();

                try
                {
                    properties[index].SetValue(item, Convert.ChangeType(newValue, properties[index].PropertyType));
                }
                catch (Exception ex)
                {
                    if (ex.InnerException != null)
                        Console.WriteLine($">>> Помилка! {ex.InnerException.Message}");
                    else
                        Console.WriteLine($">>> Помилка! {ex.Message}");
                    continue;
                }

                Console.WriteLine($"\n{properties[index].Name} вiдредаговано");
                UpdateFile();
            }
        }

        public void Search(string value)
        {
            List<Type> objects = new List<Type>();
            var properties = typeof(Type).GetProperties();

            foreach (Type item in container)
                foreach (PropertyInfo property in properties)
                    if (property.GetValue(item).ToString().Contains(value))
                    {
                        objects.Add(item);
                        break;
                    }
            Console.WriteLine($"Успiшно знайдено об'єктiв: {objects.Count}\n");

            foreach(Type item in objects)
            {
                Console.WriteLine(item);
                Console.WriteLine("--------------------");
            }
        }

        private void UpdateFile()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            string jsonItems = JsonConvert.SerializeObject(container, new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            File.WriteAllText(fileName, jsonItems);
        }

        private void ReadFromFile()
        {
            if (!File.Exists(fileName))
                File.Create(fileName).Close();

            string jsonItems = File.ReadAllText(fileName);
            try
            {
                container = JsonConvert.DeserializeObject<List<Type>>(jsonItems);
            }
            catch
            {
                Console.WriteLine("Файл невалiдний!");
            }

            if (container is null)
                container = new List<Type>();
        }

        public override string ToString()
        {
            string items = String.Empty;
            foreach (var item in container)
            {
                items += $"{item} ----------------------------\n";
            }
            return items;
        }

        public void SetFileName(string filename)
        { 
            fileName = filename;
        }

        private int EnterIntForMenuEditAction(PropertyInfo[] properties)
        {
            int index = -1;
            while (index < 0 || index >= properties.Length)
            {
                Console.WriteLine("\nЯке поле ви хочете редагувати?");
                for (int i = 0; i < properties.Length; i++)
                    Console.WriteLine($"  {i}. {properties[i].Name}");
                Console.WriteLine("  <. Повернутись назад");

                string action = Console.ReadLine();
                if (action == "<") return -1;
                try
                {
                    index = Convert.ToInt32(action);
                }
                catch
                {
                    Console.WriteLine(">>> Помилка! Ви повинні ввести натуральне число!!!");
                }
            }
            return index;
        }
    }
}
