using System;
using System.Reflection;
using System.Windows.Media;

namespace FlatMVVM_WPFDemo.Utils
{
    public static class Utils
    {

        private static readonly Random Rnd = new Random();

        public static Brush GetRandomBrush()
        {
            Type brushesType = typeof(Brushes);

            PropertyInfo[] properties = brushesType.GetProperties();

            int random = Rnd.Next(properties.Length);
            var result = (Brush)properties[random].GetValue(null, null);

            return result;
        }

    }
}
