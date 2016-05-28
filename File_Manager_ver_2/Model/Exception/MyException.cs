using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Manager_ver_2
{
  
    public class MyException : Exception
    {
        public string message;
       
        public Exception innerException;
        

        public MyException(string Message, Exception innerException)
        {
            this.innerException = innerException;
            this.message = Message;
        }

        public override string ToString()
        {
            return message + "\n" + innerException.ToString();
        }
    }
}
