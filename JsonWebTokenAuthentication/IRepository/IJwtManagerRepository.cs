using JsonWebTokenAuthentication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JsonWebTokenAuthentication.IRepository
{
    public interface IJwtManagerRepository
    {
        Tokens Authenticate(Users users);
    }
}
