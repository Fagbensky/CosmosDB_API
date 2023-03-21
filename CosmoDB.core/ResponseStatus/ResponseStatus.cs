using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.ResponseStatus
{
    public static class ResponseStatus
    {
        public static readonly (string code, string message) success = ("00", "Operation Successfull");
        public static readonly (string code, string message) badRequest = ("01", "Invalid request");
        public static readonly (string code, string message) exception = ("02", "An Error Ocurred");
    }
}
