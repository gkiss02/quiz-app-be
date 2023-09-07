namespace DotNet.Models {
    public class UserWithScore {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int Score { get; set; }

        public UserWithScore() { 
            if (UserName == null) UserName = "";
        }
    }
}