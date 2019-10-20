using System;
using System.Collections.Generic;

namespace Capgemini.GreatOutdoors.Entities
{
    public interface IUser
    {
        string Email { get; set; }
        string Password { get; set; }
    }
}

