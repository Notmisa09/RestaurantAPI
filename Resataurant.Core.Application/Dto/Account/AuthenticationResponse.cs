using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Restaurant.Core.Application.Dto.Account
{
    public class AuthenticationResponse
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool IsVerified { get; set; }
        public List<string> Reles {  get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
        public string JWTtoken { get; set; }
        
        [JsonIgnore]
        public string RefreshToken { get; set; }
    }
}
