
using System;
using System.Collections.Generic;

namespace TrainingManagementSystem.Utilities
{
    public class GenericHelper
    {
        public static void DisplayItems<T>(IList<T> items)
        {
            if (items == null)
            {
                Console.WriteLine("Collection is null.");
                return;
            }

            if (items.Count == 0)
            {
                Console.WriteLine("No items available.");
                return;
            }

            for (int i = 0; i < items.Count; i++)
            {
                Console.WriteLine(items[i]);
            }
        }

        public static int SearchItem<T>(
            IList<T> items,
            T target)
        {
            if (items == null)
            {
                return -1;
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(items[i], target))
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
