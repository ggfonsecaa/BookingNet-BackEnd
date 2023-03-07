namespace BookingNet.Application.Models.GroupModels
{
    public class GroupModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int RoleId { get; set; }
        public RoleGroupModel Role { get; set; }
    }
}