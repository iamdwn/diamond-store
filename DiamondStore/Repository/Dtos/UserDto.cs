namespace Service.Dtos
{
    public class UserDto
    {
        public bool isAuthenticated { get; set; }
        public Guid userId { get; set; }
        public string username { get; set; }
    }
}
