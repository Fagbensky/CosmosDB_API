using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmoDB.core.Models.viewModels
{
    public class ReturnVM <T>
    {
        public string ResponseCode { get; set; } = "";
        public string ResponseDescription { get; set; } = "";
        public T? ResponseData { get; set; }
    }
}
