namespace BookingNet.Application.Contracts.UserContracts
{
    public class UserCreateRequest
    {
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
    }
}