#nullable disable
namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class JobController : ControllerBase
    {
        private readonly DBContext _context;

        public JobController(DBContext context)
        {
            _context = context;
        }

        #region Obter lista de jobs
        // GET: api/Job
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJob()
        {
            return await _context.Job.ToListAsync();
        }
        #endregion

        #region Alterar nome de um job
        // PUT: api/Job/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutJob(Job job)
        {
            Response response = new();
            var aux = _context.Job.AsNoTracking().SingleOrDefault(e => e.Id == job.Id);
            
            if (aux == default) 
            { 
                response._statusCode = 404;
                response._response = "Job não encontrado";
                return Ok(response); 
            }

            _context.Entry(job).State = EntityState.Modified;

            if (VerificaNulo(job.Nome))
            {
                response._statusCode = 405;
                response._response = "Inserir nome do job";
                return Ok(response);
            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobExists(job.Id))
                {
                    response._statusCode = 404;
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

        #region Inserir Job
        // POST: api/Job
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Job>> PostJob(Job job)
        {
            Response response = new();

            if (JobNameExists(job.Nome))
            {
                response._statusCode = 409;
                response._response = "Já existe um job com esse nome associado";
                return Ok(response);
            }
            if (VerificaNulo(job.Nome))
            {
                response._statusCode = 404;
                response._response = "Inserir nome do job";
                return Ok(response);
            }
            else
            {
                _context.Job.Add(job);
                await _context.SaveChangesAsync();
                response._statusCode = 201;
                response._response = "Job inserido com sucesso";
                return Ok(response);
            }
        }
        #endregion

        #region Eliminar um Job
        // DELETE: api/Job/5
        [HttpDelete]
        public async Task<IActionResult> DeleteJob(Job job)
        {
            Response response = new();
            var item = await _context.Job.FindAsync(job.Id);
            if (item == null) 
            {
                response._statusCode = 404;
                response._response = "Job não encontrado";
                return Ok(response);
            }

            _context.Job.Remove(item);
            await _context.SaveChangesAsync();

            response._statusCode = 201;
            response._response = "Job eliminado com sucesso";
            return Ok(response);
        }
        #endregion

        private bool JobExists(int id) { return _context.Job.Any(e => e.Id == id); }
        private bool JobNameExists(string nome) { return _context.Job.Any(e => e.Nome == nome); }
        private static bool VerificaNulo(string texto) { return (String.IsNullOrEmpty(texto)); }
    }
}
