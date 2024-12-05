namespace School.Data.Helper;
public class JwtAuthResponse
{
    public string AccessToken { get; set; }
    public RefreshTokenResponse RefreshToken { get; set; }
}

public class RefreshTokenResponse
{
    public string UserName { get; set; }
    public string RefreshToken { get; set; }
    public DateTime ExpiredAt { get; set; }
}
