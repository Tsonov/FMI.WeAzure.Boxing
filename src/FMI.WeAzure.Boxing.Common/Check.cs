using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMI.WeAzure.Boxing.Common
{
    public static class Check
    {
        /// <summary>
        /// Checks the given value for null and throws <see cref="System.ArgumentNullException"/> if it is
        /// </summary>
        /// <typeparam name="T">the type of the value</typeparam>
        /// <param name="value">the value to check</param>
        /// <param name="paramName">name of the parameter in the method signature</param>
        /// <param name="message">the message to use</param>
        public static void ThrowIfNull<T>(T value, string paramName = "", string message = "") where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
    }
}
