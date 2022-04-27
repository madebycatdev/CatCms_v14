namespace EuroCMS.Plugin.Antur
{
    public class LoginResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public LoginResponse() {
            IsSuccess = false;
        }
    }
}