using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KrestNol
{
    public class Vvod
    {
        public int ConvertInt(string input)
        {
            if (input.Split(' ').Count() != 1) 
                return 0;
            int value = 0;
            Int32.TryParse(input, out value);
            return value;
        }

        public int[] ConvertArrInt(string input)
        {
            if (input.Split(' ').Count() != 2) 
                return null;
            int[] value = new int[2];
            string[] buf = input.Split(' ');
            Int32.TryParse(buf.First(), out value[0]);
            Int32.TryParse(buf.Last(), out value[1]);
            return value;
        }
    }
}
