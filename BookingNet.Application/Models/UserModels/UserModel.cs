namespace BookingNet.Application.Models.UserModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int NotificationWayId { get; set; }
        public ICollection<UsersGroupsModel> Groups { get; set; }
    }
}