using Microsoft.IdentityModel.Tokens;

namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTTokenController : ControllerBase
    {
        public IConfiguration _configuration;
        public readonly DBContext _context;

        public JWTTokenController(IConfiguration configuration, DBContext context)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string Username, string Password)
        {
            Response response = new()
            {
                _statusCode = 404,
                _response = "Credenciais Inválidas"
            };
            if (string.IsNullOrEmpty(Username)) return Ok(response);
            if (string.IsNullOrEmpty(Password)) return Ok(response);

            if (Username != null && Password != null)
            {
                var userData = await GetUtilizador(Username, Password);
                var jwt = _configuration.GetSection("Jwt").Get<Jwt>();

                if (userData != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", userData.Id.ToString()),
                        new Claim("Username", userData.Username),
                        new Claim("Password", userData.Password),
                        new Claim("Admin", userData.Admin.ToString()),
                        new Claim("Estado", userData.Estado.ToString())
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signIn);
                    if (userData.Estado == false)
                    {
                        response._statusCode = 401;
                        response._response = "O utilizador está desativado";
                        return Ok(response);
                    }
                    var auxToken = new JwtSecurityTokenHandler().WriteToken(token);
                    Token rspToken = new()
                    {
                        _token = auxToken
                    };
                    return Ok(rspToken);
                }
                else return Ok(response);
            }
            else return Ok(response);
        }

        [HttpGet]
        public async Task<Utilizador> GetUtilizador(string username, string password)
        {
            return await _context.Utilizador.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
        }
    }
}
