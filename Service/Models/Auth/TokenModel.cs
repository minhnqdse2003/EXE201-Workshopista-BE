using Service.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Auth
{
    public class TokenModel
    {
        public required string Token { get; set; }

        public required string RefreshToken { get; set; }

        public LoginUserResponseModel User { get; set; }
    }

    public class LoginUserResponseModel : UserInformationModel
    {
        public Guid UserId { get; set; }
    }


}
