namespace BackendApi.Contracts
{
    public class GetUserResponse
    {
        public int id_user { get; set; }
        public string login { get; set; } = null!;
        public string password { get; set; } = null!;
        public int role_id { get; set; }
        public string address { get; set; } = null!;
        public bool is_deleted { get; set; }
    }
}
