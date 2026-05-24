using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Vytah.Models
{
    public class InventoryItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BitmapImage IconPath { get; set; }

        public InventoryItem(string id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;

            string path = Path.Combine(
                System.AppDomain.CurrentDomain.BaseDirectory,
                "Assets", $"{id}.png");

            if (File.Exists(path))
            {
                var img = new BitmapImage();
                img.BeginInit();
                img.UriSource = new Uri(path, UriKind.Absolute);
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.EndInit();
                img.Freeze();
                IconPath = img;
            }
            else
            {
                IconPath = null;
            }
        }
    }
}