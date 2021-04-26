using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSalesWebMvc.Models.Enums
{
    public enum SaleStatus : int //tipo enumerado
    {
        Pending = 0,
        Billed = 1,
        Canceled = 2,
    }
}
