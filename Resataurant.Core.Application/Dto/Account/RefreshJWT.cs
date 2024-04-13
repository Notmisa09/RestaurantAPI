namespace Restaurant.Core.Application.Dto.Account
{
    public class RefreshJWT
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires {  get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public DateTime? Created { get; set; }
        public DateTime? Revoked { get; set; }
        public string? ReplaceByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
