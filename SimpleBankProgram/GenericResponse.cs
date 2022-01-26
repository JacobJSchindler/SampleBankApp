using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankProgram
{
    public class GenericResponse
    {
        public bool Completed { get; set; } = false;
        public string Message { get; set; }
    }
}
