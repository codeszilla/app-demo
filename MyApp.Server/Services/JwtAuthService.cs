using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

public class JwtAuthService
{

    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IJSRuntime _js;
    private readonly NavigationManager _nav;

    public JwtAuthService(IHttpContextAccessor httpContextAccessor,
                          IJSRuntime js,
                          NavigationManager nav)
    {
        _httpContextAccessor = httpContextAccessor
            ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _js = js ?? throw new ArgumentNullException(nameof(js));
        _nav = nav ?? throw new ArgumentNullException(nameof(nav));
    }

    public bool IsAuthenticated()
    {
        var context = _httpContextAccessor.HttpContext;
        if (context == null)
        {
         
            return false;
        }

        var cookieName = "jwtToken";
        var cookie = context.Request.Cookies[cookieName];

        if (string.IsNullOrEmpty(cookie))
        {
  
            return false;
        }

        try
        {
            var token = new JwtSecurityToken(jwtEncodedString: cookie);
            return token != null;
        }
        catch
        {
     
            return false;
        }


    }
 
}
