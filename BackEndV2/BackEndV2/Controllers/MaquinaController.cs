#nullable disable

namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MaquinaController : ControllerBase
    {
        private readonly DBContext _context;

        public MaquinaController(DBContext context)
        {
            _context = context;
        }

        #region Obter lista de Maquinas
        // GET: api/Maquina
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Maquina>>> GetMaquina()
        {
            return await _context.Maquina.ToListAsync();
        }
        #endregion

        #region Alterar nome de uma Maquina
        // PUT: api/Maquina
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutMaquina(Maquina maquina)
        {
            Response response = new();
            var aux = _context.Maquina.AsNoTracking().SingleOrDefault(e => e.Id == maquina.Id);

            if (aux == default) 
            {
                response._statusCode = 404;
                response._response = "Máquina não encontrada";
                return Ok(response); 
            }

            _context.Entry(maquina).State = EntityState.Modified;

            if (VerificaNulo(maquina.Nome))
            {
                response._statusCode = 405;
                response._response = "Inserir nome da máquina";
                return Ok(response);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MaquinaExists(maquina.Id))
                {
                    response._statusCode = 404;
                    response._response = "Erro na edição da máquina";
                    return Ok(response);
                }
                else throw;
            }
            return Ok(maquina);
        }
        #endregion

        #region Inserir Maquina
        // POST: api/Maquina
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Maquina>> PostMaquina(Maquina maquina)
        {
            Response response = new();

            if (MaquinaNameExists(maquina.Nome))
            {
                response._statusCode = 409;
                response._response = "Já existe uma máquina com esse nome associado";
                return Ok(response);
            }
            if (VerificaNulo(maquina.Nome))
            {
                response._statusCode = 404;
                response._response = "Inserir nome da máquina";
                return Ok(response);
            }
            else
            {
                _context.Maquina.Add(maquina);
                await _context.SaveChangesAsync();
                response._statusCode = 201;
                response._response = "Máquina inserida com sucesso";
                return Ok(response);
            }
        }
        #endregion

        #region Eliminar uma Maquina
        // DELETE: api/Maquina
        [HttpDelete]
        public async Task<IActionResult> DeleteMaquina(Maquina maquina)
        {
            Response response = new();
            var item = await _context.Maquina.FindAsync(maquina.Id);
            if (item == null) 
            {
                response._statusCode = 404;
                response._response = "Máquina não encontrada";
                return Ok(response);
            }

            _context.Maquina.Remove(item);
            await _context.SaveChangesAsync();

            response._statusCode = 201;
            response._response = "Máquina eliminada com sucesso";
            return Ok(response);
        }
        #endregion

        private bool MaquinaExists(int id) { return _context.Job.Any(e => e.Id == id); }
        private bool MaquinaNameExists(string nome) { return _context.Job.Any(e => e.Nome == nome); }
        private static bool VerificaNulo(string texto) { return (String.IsNullOrEmpty(texto)); }
    }
}
