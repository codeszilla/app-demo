using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Components;

public class LogoutService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly NavigationManager _nav;

    public LogoutService(IHttpContextAccessor httpContextAccessor, NavigationManager nav)
    {
        _httpContextAccessor = httpContextAccessor;
        _nav = nav;
    }

    public void Logout()
    {
        var context = _httpContextAccessor.HttpContext;

        if (context != null)
        {
            var deleteOptions = new CookieOptions
            {
                Expires = DateTime.UtcNow.AddDays(-1),
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Path = "/"
            };

            context.Response.Cookies.Append("jwtToken", "", deleteOptions);
        }

        _nav.NavigateTo("/welcome", true);
    }
}
