using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Vvod
    {
        public static int ConvertInt(string input)
        {
            if (input.Split(' ').Count() == 1)
            {
                int value = 0;
                Int32.TryParse(input, out value);
                return value;
            }
            switch (input.Split(' ').First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    break;
                case "load":
                case "Load":
                case "LOAD":
                    break;
                default:
                    break;
            }
            return 0;
        }

        public static int[] ConvertArrInt(string input)
        {
            if (input.Split(' ').Count() != 2) 
                return null;
            int[] value = new int[2];
            string[] buf = input.Split(' ');
            if (Int32.TryParse(buf.First(), out value[0]))
            {
                Int32.TryParse(buf.Last(), out value[1]);
                return value;
            }
            switch (buf.First())
            {
                case "save":
                case "Save":
                case "SAVE":
                    break;
                case "load":
                case "Load":
                case "LOAD":
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
