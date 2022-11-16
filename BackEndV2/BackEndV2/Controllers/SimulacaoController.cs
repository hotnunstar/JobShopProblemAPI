#nullable disable

namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SimulacaoController : ControllerBase
    {
        private readonly DBContext _context;

        public SimulacaoController(DBContext context)
        {
            _context = context;
        }

        #region Obter lista de Simulacões
        // GET: api/Simulacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Simulacao>>> GetSimulacoes()
        {
            return await _context.Simulacao.ToListAsync();
        }
        #endregion

        #region Alterar nome de uma Simulacao
        // PUT: api/Simulacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutSimulacao(Simulacao simulacao)
        {
            Response response = new();
            var aux = _context.Simulacao.AsNoTracking().SingleOrDefault(e => e.Id == simulacao.Id);

            if (aux == default) 
            {
                response._statusCode = 404;
                response._response = "Simulação não encontrada";
                return Ok(response);
            }

            _context.Entry(simulacao).State = EntityState.Modified;

            if (VerificaNulo(simulacao.Nome))
            {
                response._statusCode = 405;
                response._response = "Inserir nome da simulação";
                return Ok(response);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SimulacaoExists(simulacao.Id))
                {
                    response._statusCode = 400;
                    response._response = "Erro na edição do job";
                    return Ok(response);
                }
                else throw;
            }
            response._statusCode = 202;
            response._response = "Nome alterado com sucesso!";
            return Ok(response);
        }
        #endregion

        #region Inserir Simulacao
        // POST: api/Simulacao
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Simulacao>> PostSimulacao(Simulacao simulacao)
        {
            Response response = new();
            if (SimulacaoNameExists(simulacao.Nome))
            {
                response._statusCode = 409;
                response._response = "Já existe uma simulação com esse nome associado";
                return Ok(response);
            }
            if (VerificaNulo(simulacao.Nome))
            {
                response._statusCode = 404;
                response._response = "Inserir nome da simulação";
                return Ok(response); ;
            }
            else
            {
                _context.Simulacao.Add(simulacao);
                await _context.SaveChangesAsync();
                response._statusCode = 201;
                response._response = "Simulação inserida com sucesso";
                return Ok(response);
            }
        }
        #endregion

        #region Eliminar uma Simulacao
        // DELETE: api/Simulacao
        [HttpDelete("/{IdSimulacao}")]
        public async Task<IActionResult> DeleteSimulacao(int IdSimulacao)
        {
            Response response = new();
            var itens = _context.Plano.Where(c => c.IdSimulacao == IdSimulacao).ToList();
            var item = await _context.Simulacao.FindAsync(IdSimulacao);
            if (item == null) 
            {
                response._statusCode = 404;
                response._response = "Simulação não encontrada";
                return Ok(response);
            }
            else if(itens.Count > 0)
            {
                foreach (var obj in itens)
                {
                    _context.Plano.Remove(obj);
                }

                _context.Simulacao.Remove(item);
                await _context.SaveChangesAsync();

                response._statusCode = 201;
                response._response = "Simulação eliminada com sucesso";
                return Ok(response);
            }
            else
            {
                _context.Simulacao.Remove(item);
                await _context.SaveChangesAsync();

                response._statusCode = 201;
                response._response = "Simulação eliminada com sucesso";
                return Ok(response);
            }
        }
        #endregion

        private bool SimulacaoExists(int id) { return _context.Simulacao.Any(e => e.Id == id); }
        private bool SimulacaoNameExists(string nome) { return _context.Simulacao.Any(e => e.Nome == nome); }
        private static bool VerificaNulo(string texto) { return (String.IsNullOrEmpty(texto)); }
    }
}
