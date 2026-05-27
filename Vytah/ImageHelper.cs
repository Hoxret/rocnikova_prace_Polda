using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace Vytah
{
    public static class ImageHelper
    {
        private static readonly string BasePath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Backgrounds");

        public static BitmapImage Load(string fileName)
        {
            string fullPath = Path.Combine(BasePath, fileName);
            if (!File.Exists(fullPath)) return null;

            var img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri(fullPath, UriKind.Absolute);
            img.CacheOption = BitmapCacheOption.OnLoad;
            img.EndInit();
            img.Freeze();
            return img;
        }
    }
}