namespace ELISExtension.Interface.Models
{
    public class User : Base
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string roleName { get; set; }
    }
}
