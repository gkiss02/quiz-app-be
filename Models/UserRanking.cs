namespace DotNet.Models {
    public class UserRanking {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public int TotalScore { get; set; }
        public int UserRank { get; set; }

        public UserRanking() { 
            if (UserName == null) UserName = "";
        }
    }
}