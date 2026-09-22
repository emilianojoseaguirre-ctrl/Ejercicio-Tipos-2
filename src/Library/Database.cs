//------------------------------------------------------------------------------
// <copyright file="CarsDatabase.cs" company="Universidad Católica del Uruguay">
//     Copyright (c) Programación II. Derechos reservados.
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Collections.ObjectModel;

namespace Ucu.Poo.Repositories
{
    public class Database<T> where T : IHasValue
    {

        private List<T> items = new List<T>();

        public void Add(T newItem)
        {
            if (newItem != null)
            {
                this.items.Add(newItem);
            }
        }

        public void Remove(T item)
        {
            this.items.Remove(item);
        }

        public T Find(string field, string value)
        {
            foreach (T item in this.items)
            {
                if (item.HasValue(field, value))
                {
                    return item;
                }
            }

            return default(T);
        }
        
        public string ConvertToJson()
        {
            return JsonSerializer.Serialize(this.items);
        }

        public void LoadFromJson(string content)
        {
            List<T> items = JsonSerializer.Deserialize<List<T>>(content);
            if (items != null)
            {
                 this.items = items;
            }
             else
            {
                 this.items = new List<T>();
            }
        }
         
        public void SaveToFile(string filePath)
        {
            string content = this.ConvertToJson();
            File.WriteAllText(filePath, content);
        }

        public bool LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                this.LoadFromJson(content);
                return true;
            }

            return false;
        }
    
        public ReadOnlyCollection<T> Items
        {
            get
            {
                return this.items.AsReadOnly();
            }
        }


    }
}
