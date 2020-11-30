using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dto
{
    public class BetAttemptResult
    {
        //this could be an enum
        public string Message { get; set; }
        public bool Approved { get; set; }
    }
}
