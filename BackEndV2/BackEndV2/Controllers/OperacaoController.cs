#nullable disable

namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OperacaoController : ControllerBase
    {
        private readonly DBContext _context;

        public OperacaoController(DBContext context)
        {
            _context = context;
        }

        #region Obter lista de operações
        // GET: api/Operacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacao>>> GetOperacoes()
        {
            return await _context.Operacao.ToListAsync();
        }
        #endregion

        #region Obter determinada operação
        // GET: api/Operacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacao>> GetOperacao(int id)
        {
            Response response = new();
            var operacao = await _context.Operacao.FindAsync(id);

            if (operacao == null)
            {
                response._statusCode = 404;
                response._response = "Operação não encontrada";
                return Ok(response);
            }
            else return operacao;
        }
        #endregion

        #region Alterar nome de uma operação
        // PUT: api/Operacao/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacao(int id, Operacao operacao)
        {
            Response response = new();
            var aux = _context.Operacao.AsNoTracking().SingleOrDefault(e => e.Nome == operacao.Nome);

            if (aux == default) 
            {
                response._statusCode = 404;
                response._response = "Operação não encontrada";
                return Ok(response); 
            }

            operacao.Id = id;
            _context.Entry(operacao).State = EntityState.Modified;

            if (VerificaNulo(operacao.Nome))
            {
                response._statusCode = 405;
                response._response = "Inserir nome da operação";
                return Ok(response);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperacaoExists(id))
                {
                    response._statusCode = 404;
                    response._response = "Erro na edição da operação";
                    return Ok(response);
                }
                else throw;
            }
            response._statusCode = 202;
            response._response = "Nome alterado com sucesso!";
            return Ok(response);
        }
        #endregion

        #region Inserir operação
        // POST: api/Operacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Operacao>> PostOperacao(Operacao operacao)
        {
            Response response = new();

            if (OpNameExists(operacao.Nome))
            {
                response._statusCode = 409;
                response._response = "Já existe uma operação com esse nome associado";
                return Ok(response);
            }
            if (VerificaNulo(operacao.Nome))
            {
                response._statusCode = 404;
                response._response = "Inserir nome da operação";
                return Ok(response);
            }
            else
            {
                _context.Operacao.Add(operacao);
                await _context.SaveChangesAsync();
                response._statusCode = 201;
                response._response = "Operação inserida com sucesso";
                return Ok(response);
            }
        }
        #endregion

        #region Eliminar uma operação
        // DELETE: api/Operacao/5
        [HttpDelete]
        public async Task<IActionResult> DeleteOperacao(Operacao operacao)
        {
            Response response = new();
            var item = await _context.Operacao.FindAsync(operacao.Id);
            if (item == null)
            {
                response._statusCode = 404;
                response._response = "Operação não encontrada";
                return Ok(response);
            }

            _context.Operacao.Remove(item);
            await _context.SaveChangesAsync();

            response._statusCode = 201;
            response._response = "Operação eliminada com sucesso";
            return Ok(response);
        }
        #endregion

        private bool OperacaoExists(int id) { return _context.Operacao.Any(e => e.Id == id);}
        private static bool VerificaNulo(string texto) { return (String.IsNullOrEmpty(texto)); }
        private bool OpNameExists(string nome) { return _context.Operacao.Any(e => e.Nome == nome); }
    }
}
