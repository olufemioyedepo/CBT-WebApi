using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_WebApi.Helper
{
    public class Util
    {
        public static string LogError(Exception ex)
        {
            string exceptionMessage = String.Format("Mesaage:\n {0}\n\n Inner Exception:\n {1}\n\n Stacktrace:\n {2}\n " +
                "_______________________________________________________________________________________________________________________________________", ex.Message, ex.InnerException == null ? "N/A" : ex.InnerException.Message, ex.StackTrace);
            return exceptionMessage;
        }
    }
}
