namespace API.Test.Jitsi.JWT.Models
{
    public class TokenRequestModel
    {
        public string ApiKey { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string UserAvatar { get; set; }
        public string AppID { get; set; }
        public bool isModerator { get; set; }
    }
}
