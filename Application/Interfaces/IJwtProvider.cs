﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IJwtProvider
    {
        string GenerateToken(string username, string role);
        string ExtractTokenFromHeader(string authHeader);
        string RenewToken(string token);
    }
}
