namespace BookingNet.Application.Contracts.AuthContracts
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
    }
}