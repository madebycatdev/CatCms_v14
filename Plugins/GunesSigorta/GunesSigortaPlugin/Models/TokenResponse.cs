namespace EuroCMS.Plugin.GunesSigorta
{
    public class TokenResponse
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public int expires_in { get; set; }
        public long consented_on { get; set; }
        public string Scope { get; set; }
    }
}