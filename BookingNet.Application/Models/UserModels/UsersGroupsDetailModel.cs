namespace BookingNet.Application.Models.UserModels
{
    public class UsersGroupsDetailModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public GroupRoleModel Role { get; set; }
    }
}