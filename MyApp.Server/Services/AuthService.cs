using Microsoft.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


public class AuthService
{
    private readonly string _secretKey = "GodisGoodallthetime_appdev_net_8128!"; 

    private readonly IHttpContextAccessor _context;
    private readonly IJSRuntime _js;

    public AuthService(IHttpContextAccessor context, IJSRuntime js)
    {
        _context = context;
        _js = js;
    }


    public async Task LoginAsync(string jwtToken)
    {
        await _js.InvokeVoidAsync("cookieHelper.setCookie", "jwtToken", jwtToken, 12);
    }


    public string? GetToken()
    {
        var httpContext = _context.HttpContext;
        if (httpContext != null)
        {
            httpContext.Request.Cookies.TryGetValue("jwtToken", out var token);
            return token;
        }
        return null;
    }


    public async Task CheckAsync()
    {
        await _js.InvokeVoidAsync("cookieHelper.getCookie", "jwtToken");
    }



    public async Task LogoutAsync()
    {
        await _js.InvokeVoidAsync("cookieHelper.deleteCookie", "jwtToken");
    }


    public _Info GetAccountInfo()
    {
        var info = new _Info();
        var context = _context.HttpContext;
        var cookieName = "jwtToken"; 
        var cookie = context?.Request.Cookies[cookieName];

        if (cookie == null)
        {
            info.isAuthenticated = false;
            return info;
        }

        var token = new JwtSecurityToken(jwtEncodedString: cookie);

        info.isAuthenticated = true;
        info.cookie = cookie;
        info.cookie_name = cookieName;
        info.Account_ID = int.Parse(token.Claims.FirstOrDefault(c => c.Type == "AccountId")?.Value ?? "0");
        info.NickName = token.Claims.FirstOrDefault(c => c.Type == "NickName")?.Value ?? "";
        info.AccountName = token.Claims.FirstOrDefault(c => c.Type == "AccountName")?.Value ?? "";
        info.Region = token.Claims.FirstOrDefault(c => c.Type == "Region")?.Value ?? "";
        info.Language = token.Claims.FirstOrDefault(c => c.Type == "Language")?.Value ?? "";
        info.Channel = token.Claims.FirstOrDefault(c => c.Type == "Channel")?.Value ?? "";
        info.CountryCode = token.Claims.FirstOrDefault(c => c.Type == "CountryCode")?.Value ?? "";
        info.exp = token.Claims.FirstOrDefault(c => c.Type == "exp")?.Value ?? "";
        info.iss = token.Claims.FirstOrDefault(c => c.Type == "iss")?.Value ?? "";
        info.aud = token.Claims.FirstOrDefault(c => c.Type == "aud")?.Value ?? "";

        return info;
    }


    public string GenerateJwt(string email, string accountId, string nickname)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", email),
            new Claim("AccountId", accountId),
            new Claim("NickName", nickname),
            new Claim("logintype", "Eval"),
            new Claim("Region", "NA"),
            new Claim("Language", "EN,PH"),
            new Claim("Channel", "N"),
            new Claim("CountryCode", "PH")
        };

        var token = new JwtSecurityToken(
            issuer: "http://appsdev.net",
            audience: "http://appsdev.net",
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

