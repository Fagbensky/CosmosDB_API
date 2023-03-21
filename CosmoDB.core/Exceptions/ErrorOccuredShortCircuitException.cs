using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Exceptions
{
    public class ErrorOccuredShortCircuitException : Exception
    {
        public ErrorOccuredShortCircuitException() : base()
        {
        }

        public ErrorOccuredShortCircuitException(string message) : base(message)
        {
        }

        public override string? StackTrace
        {
            get { return null; }
        }
    }
}
