using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace eCar.Applicaton.Infrastructure
{
    public class JavaScriptException : Exception
    {
        public JavaScriptException(string message)
            : base(message)
        {
        }
    }
}