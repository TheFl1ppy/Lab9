namespace BackendApi.Contracts
{
    public class CreateUserRequest
    {
        public string login { get; set; } = null!;
        public string password { get; set; } = null!;
        public int role_id { get; set; }
        public string address { get; set; } = null!;
    }
}
