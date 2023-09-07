namespace DotNet.Models {
    public class User {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string EmailAddress { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public bool isActive { get; set; }

        public User() { 
            if (UserName == null) UserName = "";
            if (EmailAddress == null) EmailAddress = "";
            if (PasswordHash == null) PasswordHash = "";
            if (PasswordSalt == null) PasswordSalt = "";
        }
    }
}