#nullable disable
namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UtilizadorController : ControllerBase
    {
        private readonly DBContext _context;

        public UtilizadorController(DBContext context)
        {
            _context = context;
        }

        #region Criar utilizadores, atribuindo password (apenas administradores podem criar)
        // POST: api/Utilizador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        [HttpPost]
        public async Task<ActionResult<Utilizador>> PostUtilizador(Utilizador utilizador)
        {
            Response response = new();
            if (VerificaNulo(utilizador.Password))
            {
                response._statusCode = 406;
                response._response = "Deve inserir uma password";
                return Ok(response);
            }
            if (string.IsNullOrEmpty(utilizador.Password) || utilizador.Password.Length <= 6)
            {
                response._statusCode = 406;
                response._response = "A password deverá conter mais de 6 caracteres";
                return Ok(response);
            }
            if (VerificaNulo(utilizador.Username))
            {
                response._statusCode = 406;
                response._response = "Deve inserir um username!";
                return Ok(response);
            }
            if (utilizador.Username.Contains(' '))
            {
                response._statusCode = 406;
                response._response = "Username não deve conter espaços!";
                return Ok(response);
            }
            utilizador.Estado = true;

            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);

            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrado";
                return Ok(response);
            }
            if (registo.Admin == false)
            {
                response._statusCode = 401;
                response._response = "Não tem permissões para criar novos utilizadores";
                return Ok(response);
            }
            if (registo.Admin == true)
            {
                var user = await _context.Utilizador.SingleOrDefaultAsync(e => e.Username == utilizador.Username);

                if (user != default)
                {
                    response._statusCode = 409;
                    response._response = "Username já registado!";
                    return Ok(response);
                }
                else
                {
                    _context.Utilizador.Add(utilizador);
                    await _context.SaveChangesAsync();
                    response._statusCode = 201;
                    response._response = "Utilizador registado com sucesso";
                    return Ok(response);
                }
            }
            response._statusCode = 409;
            response._response = "Erro no registo do utilizador";
            return Ok(response);
        }
        #endregion

        #region Alterar a password
        // PUT: api/Utilizador
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut] //("OldPassword={OldPassword}&NewPassword={NewPassword}")
        public async Task<IActionResult> PutUtilizador(string OldPassword, string NewPassword)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrado";
                return Ok(response);
            }

            if (!string.Equals(registo.Password, OldPassword))
            {
                response._statusCode = 406;
                response._response = "Password antiga incorreta!";
                return Ok(response);
            }
            if (string.Equals(registo.Password, NewPassword))
            {
                response._statusCode = 406;
                response._response = "Password nova igual à antiga!";
                return Ok(response);
            }
            if (string.IsNullOrEmpty(NewPassword) || NewPassword.Length <= 6)
            {
                response._statusCode = 406;
                response._response = "A password deverá ter 6 ou mais carateres!";
                return Ok(response);
            }
            else
            {
                _context.Entry(registo).State = EntityState.Modified;
                registo.Password = NewPassword;
                try { await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (registo == default)
                    {
                        response._statusCode = 400;
                        response._response = "Erro na alteração da password!";
                        return Ok(response);
                    }
                    else throw;
                }
            }
            response._statusCode = 200;
            response._response = "Password alterada com sucesso!";
            return Ok(response);
        }
        #endregion

        #region Remover utilizador (apenas administradores podem remover)
        // PUT: api/Utilizador
        [HttpPut("{estado}")]
        public async Task<IActionResult> RemoveUtilizador(bool estado,Utilizador utilizador)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrado";
                return Ok(response);
            }
            if (registo.Admin == false)
            {
                response._statusCode = 401;
                response._response = "Não tem permissões para remover utilizadores!";
                return Ok(response);
            }
            else
            {
                if (utilizador.Estado == true) utilizador.Estado = false;
                else utilizador.Estado = true;
                _context.Entry(utilizador).State = EntityState.Modified;

                try { await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException)
                {
                    if (registo == default)
                    {
                        response._statusCode = 400;
                        response._response = "Erro na remoção do utilizador!";
                        return Ok(response);
                    }
                    else throw;
                }
            }
            response._statusCode = 200;
            response._response = "Utilizador desativado com sucesso!";
            return Ok(response);
        }
        #endregion

        #region Obter lista de utilizadores (apenas para administradores)
        // GET: api/Utilizador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizador>>> GetUtilizador()
        {
            Response response = new();
            List<Response> responses = new List<Response>();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrado";
                responses.Add(response);
                return Ok(responses);
            }
            if (registo.Admin == false)
            {
                response._statusCode = 401;
                response._response = "Não tem permissões para obter a lista de utilizadores!";
                responses.Add(response);
                return Ok(responses);
            }
            else return await _context.Utilizador.ToListAsync();
        }
        #endregion

        private Utilizador GetRegistoUtilizador (int id) { return _context.Utilizador.SingleOrDefault(e => e.Id == id); }
        private int GetUtilizadorID() { return int.Parse(this.User.Claims.First(i => i.Type == "Id").Value); }
        private static bool VerificaNulo(string texto) { return (String.IsNullOrEmpty(texto)); }
    }
}
