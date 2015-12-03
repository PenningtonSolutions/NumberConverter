using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter
{
    public class IntegerConverter
    {
        public static IEnumerable<string> convert(int lowerBound, int upperBound, Dictionary<int, string> conversions)
        {
            if (lowerBound > upperBound)
            {
                throw new ArgumentException("lowerBound cannot be higher than upperBound");
            }

            StringBuilder result = new StringBuilder();

            for (int i = lowerBound; i <= upperBound; i++)
            {
                result.Clear();

                if (conversions != null)
                {
                    foreach (int key in conversions.Keys)
                    {
                        bool divisible = i % key == 0;

                        if (divisible)
                        {
                            result.Append(conversions[key]);
                        }
                    }
                }

                if (result.Length == 0)
                {
                    result.Append(i.ToString());
                }

                yield return result.ToString();
            }
        }

    }
}
