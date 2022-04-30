using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    [Serializable]
    public class Comparer:IComparer<string>
    {
       
        public int Compare(string a, string b)
        {
            if (a.Length > b.Length)
            {
                return 1;
            }
            else if (a.Length < b.Length)
            {
                return -1;
            }
            else
            {
                return a.CompareTo(b);
            }
        }

    }
}
