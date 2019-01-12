using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.API.Models
{
    public class Token
    {
        public string ActualToken { get; private set; }
        public Guid UserId { get; private set;  }

        public Token()
        {
            // EF
        }

        public Token(Guid userId)
        {
            ActualToken = Guid.NewGuid().ToString();
            UserId = userId;
        }
    }
}
