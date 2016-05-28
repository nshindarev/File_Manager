using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_Manager_ver_2
{
    class EntriesComparer<T> : IEqualityComparer<T> where T : IEntry
    {
        public bool Equals(T x, T y)
        {
            return x.FullName.Equals(y.FullName);
        }

        public int GetHashCode(T obj)
        {
            return obj.FullName.GetHashCode();
        }
    }
}
