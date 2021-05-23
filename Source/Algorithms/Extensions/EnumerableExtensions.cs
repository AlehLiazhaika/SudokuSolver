using System.Collections.Generic;
using Algorithms.Utils;

namespace Algorithms.Extensions
{
    internal static class EnumerableExtensions
    {
        internal static void Hide(this IEnumerable<Visible> source)
        {
            foreach (var item in source)
            {
                item.Hide();
            }
        }

        internal static void Show(this IEnumerable<Visible> source)
        {
            foreach (var item in source)
            {
                item.Show();
            }
        }
    }
}