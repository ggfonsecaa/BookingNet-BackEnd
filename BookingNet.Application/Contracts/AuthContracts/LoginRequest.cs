namespace BookingNet.Application.Contracts.AuthContracts
{
    public class LoginRequest
    {
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
    }
}