namespace BookingNet.Application.Contracts.UserContracts
{
    public class UserUpdateRequest
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string PassWord { get; set; }
        public byte[] RowVersion { get; set; }
    }
}