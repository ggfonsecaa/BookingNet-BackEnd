using BookingNet.Application.Models.GroupModels;
using BookingNet.Application.Models.UserModels;

namespace BookingNet.Application.Models.AuthModels
{
    public class LoginModel
    {
        public UserModel User { get; set; }
        public string Token { get; set; }
    }
}