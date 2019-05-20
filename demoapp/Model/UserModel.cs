namespace demoapp.Model
{
    public class UserModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public string terms_and_conditions { get; set; }
        public string birthday { get; set; }
    }

    public class loginModel
    {
        public string email { get; set; }
        public string password { get; set; }
        public string channelPartnerId { get; set; }
    }
}
