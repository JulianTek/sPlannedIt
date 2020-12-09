using System;
using System.Collections.Generic;
using System.Text;

namespace sPlannedIt.Entities.DTOs
{
    public class UserDTO
    {
        public string UserId { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
    }
}
