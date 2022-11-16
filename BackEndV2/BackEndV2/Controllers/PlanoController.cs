#nullable disable
using Google.OrTools.Sat;

namespace BackEndV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PlanoController : ControllerBase
    {
        private readonly DBContext _context;

        public PlanoController(DBContext context)
        {
            _context = context;
        }
        
        #region Construção da tabela de produção
        // POST: api/Plano
        [HttpPost]
        public async Task<ActionResult<Plano>> PostPlano(Plano plano)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrador";
                return Ok(response);
            }

            if (plano.UnidadeTempo == 0)
            {
                response._statusCode = 409;
                response._response = "Deve inserir o tempo de trabalho da máquina!";
                return Ok(response);
            }
            if (await _context.Simulacao.FirstOrDefaultAsync(x => x.Id == plano.IdSimulacao) == default)
            {
                response._statusCode = 404;
                response._response = "Simulação não existe!";
                return Ok(response);  
            }
            if (await _context.Job.FirstOrDefaultAsync(x => x.Id == plano.IdJob) == default)
            {
                response._statusCode = 404;
                response._response = "Job não existe!";
                return Ok(response);
            }
            if (await _context.Operacao.FirstOrDefaultAsync(x => x.Id == plano.IdOperacao) == default)
            {
                response._statusCode = 404;
                response._response = "Operação não existe!";
                return Ok(response);
            }
            if (await _context.Maquina.FirstOrDefaultAsync(x => x.Id == plano.IdMaquina) == default)
            {
                response._statusCode = 404;
                response._response = "Máquina não existe!";
                return Ok(response);
            }
            plano.IdUtilizador = registo.Id;
            plano.Estado = true;

            var checkPlano = _context.Plano.AsNoTracking().FirstOrDefault(c => c.IdSimulacao == plano.IdSimulacao &&  c.IdJob == plano.IdJob && c.IdOperacao == plano.IdOperacao);
            if(checkPlano == default)
            {
                var query = _context.Plano.AsQueryable();
                query = query.Where(c => c.IdUtilizador == registo.Id && c.IdSimulacao == plano.IdSimulacao && c.IdJob == plano.IdJob);
                var itens = await query.ToListAsync();

                plano.PosOperacao = 1;
                if(itens.Count > 0)
                {
                    foreach (var item in itens)
                    {
                        if (plano.PosOperacao <= item.PosOperacao) plano.PosOperacao = item.PosOperacao + 1;
                    }
                }
                
                _context.Plano.Add(plano);
                try { await _context.SaveChangesAsync(); }
                catch (DbUpdateException)
                {
                    response._statusCode = 409;
                    response._response = "Erro na inserção!";
                    return Ok(response);
                }
                response._statusCode = 200;
                response._response = "Inserido com sucesso";
                return Ok(response);
            }
            else
            {
                response._statusCode = 409;
                response._response = $"O job {plano.IdOperacao} da simulação {plano.IdSimulacao} já contém a operação {plano.IdOperacao}";
                return Ok(response);
            }
        }
        #endregion
        
        #region Alterar o valor de determinada operação
        // PUT: api/Plano
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("/AlterarValor")]
        public async Task<IActionResult> PutPlano(Plano plano)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrador";
                return Ok(response);
            }

            var item = await _context.Plano.AsNoTracking().FirstOrDefaultAsync (c => c.IdUtilizador == registo.Id && c.IdSimulacao == plano.IdSimulacao && c.IdJob == plano.IdJob && c.IdOperacao == plano.IdOperacao);
            if (item == null)
            {
                response._statusCode = 404;
                response._response = "Operação não encontrada";
                return Ok(response);
            }

            if(plano.UnidadeTempo == 0)
            {
                response._statusCode = 409;
                response._response = "Deve inserir o tempo de execução da máquina";
                return Ok(response);
            }

            item.IdMaquina = plano.IdMaquina;
            item.UnidadeTempo = plano.UnidadeTempo;

            _context.Entry(item).State = EntityState.Modified;

            try { await _context.SaveChangesAsync(); }
            catch (DbUpdateConcurrencyException) 
            {
                response._statusCode = 409;
                response._response = "Erro na inserção!";
                return Ok(response);
            }
            response._statusCode = 200;
            response._response = "Dados alterados com sucesso!";
            return Ok(response);
        }
        #endregion
        
        #region Consultar tabela através de operação
        // GET: api/Plano
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlano(int IdSimulacao, int IdJob, int IdOperacao)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrador";
                return Ok(response);
            }

            var item = await _context.Plano.AsNoTracking().FirstOrDefaultAsync(c => c.IdUtilizador == registo.Id && c.IdSimulacao == IdSimulacao && c.IdJob == IdJob && c.IdOperacao == IdOperacao);
            if (item == null)
            {
                response._statusCode = 404;
                response._response = "Plano não encontrado";
                return Ok(response);
            }
            return Ok(item);
        }
        #endregion

        #region Inserir tempo inicial manualmente
        [HttpPut("{IdSimulacao}")]
        public async Task<IActionResult> PutTempoManual(int IdSimulacao, List<Plano> Planos)
        {
            Response response = new();
            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrador";
                return Ok(response);
            }

            Plano item = new();
            List<Plano> List = Planos;
            int maxTemp = 0, tempo = 0;

            foreach (var plano in List)
            {
                item = await _context.Plano.AsNoTracking().FirstOrDefaultAsync
                    (c => c.IdUtilizador == registo.Id && 
                    c.IdSimulacao == IdSimulacao && 
                    c.IdJob == plano.IdJob && 
                    c.IdOperacao == plano.IdOperacao);
                if (item == null)
                {

                    response._statusCode = 409;
                    response._response = "Erro na leitura do plano";
                    return Ok(response);
                }
                else plano.TempoFinal = plano.TempoInicial + plano.UnidadeTempo;
            }

            foreach (var plano_ant in List)
            {
                maxTemp += plano_ant.UnidadeTempo;
                foreach(var plano_prox in List)
                {
                    if ((plano_prox.PosOperacao == (plano_ant.PosOperacao + 1)) && (plano_prox.IdJob == plano_ant.IdJob))
                    {
                        if (plano_prox.TempoInicial < plano_ant.TempoFinal)
                        {
                            response._statusCode = 409;
                            response._response= $"A operação {plano_prox.IdOperacao} do job {plano_prox.IdJob} começa antes da operação anterior terminar!";
                            return Ok(response);
                        } 
                    }
                }
            }

            var nmaq = _context.Maquina.Count();
            if (nmaq == 0)
            {
                response._statusCode = 404;
                response._response = "Não existem máquinas inseridas em sistema!";
                return Ok(response);
            }
            if(List.Count > 1)
            {
                int[,] planArray = new int[nmaq+1, maxTemp+1];

                foreach (var plano in List)
                {
                    tempo = plano.UnidadeTempo;
                    while ((planArray[plano.IdMaquina, plano.TempoInicial] == 0) && (tempo > 0))
                    {
                        planArray[plano.IdMaquina, plano.TempoInicial] = 1;
                        plano.TempoInicial++;
                        tempo--;
                    }
                    if (tempo != 0)
                    {
                        response._statusCode = 409;
                        response._response = $"Não é possivel atribuir a operação {plano.IdOperacao} do job {plano.IdJob}, pois a máquina {plano.IdMaquina} está a ser utilizada!";
                        return Ok(response);
                    }
                }
            }

            foreach (var plano in List)
            {
                _context.Entry(plano).State = EntityState.Modified;

                try { await _context.SaveChangesAsync(); }
                catch (DbUpdateConcurrencyException) 
                {
                    response._statusCode = 409;
                    response._response = "Erro na inserção!";
                    return Ok(response);
                }

                if (plano.TempoFinal > item.TempoFinal) item = plano;
            }
            response._statusCode = 200;
            response._response = $"A operação {item.IdOperacao} do job {item.IdJob} termina em ultimo lugar às {item.TempoFinal} unidades de tempo";
            return Ok(response);
        }
        #endregion

        #region Inserir tempos automaticamente
        [HttpPut("/automatico/{IdSimulacao}")]
        public async Task<IActionResult> PutTempoAutomatico(int IdSimulacao)
        {
            Response response = new();
            List<Plano> Planos = new();
            Planos = await _context.Plano.Where(c => c.IdSimulacao == IdSimulacao).ToListAsync();
            if(Planos.Count == 0)
            {
                response._statusCode = 404;
                response._response = "Simulação não encontrada";
                return Ok(response);
            }

            var id = GetUtilizadorID();
            var registo = GetRegistoUtilizador(id);
            if (registo == default)
            {
                response._statusCode = 404;
                response._response = "Utilizador não encontrador";
                return Ok(response);
            }

            Dictionary<int, List<Plano>> dictJobs = new Dictionary<int, List<Plano>>();
            int macNumber = 0;
            Dictionary<int, int> dictMac = new Dictionary<int, int>(); // Chave -> Id real da máquina | Valor -> Id "virtual" para o algoritmo da google
            Dictionary<int, int> dictJob = new Dictionary<int, int>(); // Chave -> "virtual" do job para o algoritmo da google | Valor -> Id real do job

            foreach (Plano plano in Planos)
            {
                List<Plano> planoJob = dictJobs.GetValueOrDefault(plano.IdJob, new List<Plano>());
                planoJob.Add(plano);
                dictJobs[plano.IdJob] = planoJob;
            }

            List<List<googleOperation>> allJobs = new List<List<googleOperation>>();
            foreach (int key in dictJobs.Keys.OrderBy(job => job))
            {
                List<googleOperation> job = new List<googleOperation>();
                foreach (Plano plano in dictJobs.GetValueOrDefault(key, new List<Plano>()))
                {
                    int nrMac;
                    bool res = dictMac.TryGetValue(plano.IdMaquina, out nrMac);
                    if (res == false) nrMac = macNumber++;
                    job.Add(new googleOperation(nrMac, plano.UnidadeTempo));
                    dictMac[plano.IdMaquina] = nrMac;
                }
                dictJob[allJobs.Count] = key;
                allJobs.Add(job);
            }

            int numMachines = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    numMachines = Math.Max(numMachines, 1 + task.machine);
                }
            }
            int[] allMachines = Enumerable.Range(0, numMachines).ToArray();

            // Computes horizon dynamically as the sum of all durations.
            int horizon = 0;
            foreach (var job in allJobs)
            {
                foreach (var task in job)
                {
                    horizon += task.duration;
                }
            }

            // Creates the model.
            CpModel model = new CpModel();

            Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>> allTasks =
                new Dictionary<Tuple<int, int>, Tuple<IntVar, IntVar, IntervalVar>>(); // (start, end, duration)
            Dictionary<int, List<IntervalVar>> machineToIntervals = new Dictionary<int, List<IntervalVar>>();
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                for (int taskID = 0; taskID < job.Count(); ++taskID)
                {
                    var task = job[taskID];
                    String suffix = $"_{jobID}_{taskID}";
                    IntVar start = model.NewIntVar(0, horizon, "start" + suffix);
                    IntVar end = model.NewIntVar(0, horizon, "end" + suffix);
                    IntervalVar interval = model.NewIntervalVar(start, task.duration, end, "interval" + suffix);
                    var key = Tuple.Create(jobID, taskID);
                    allTasks[key] = Tuple.Create(start, end, interval);
                    if (!machineToIntervals.ContainsKey(task.machine))
                    {
                        machineToIntervals.Add(task.machine, new List<IntervalVar>());
                    }
                    machineToIntervals[task.machine].Add(interval);
                }
            }

            // Create and add disjunctive constraints.
            foreach (int machine in allMachines)
            {
                model.AddNoOverlap(machineToIntervals[machine]);
            }

            // Precedences inside a job.
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                for (int taskID = 0; taskID < job.Count() - 1; ++taskID)
                {
                    var key = Tuple.Create(jobID, taskID);
                    var nextKey = Tuple.Create(jobID, taskID + 1);
                    model.Add(allTasks[nextKey].Item1 >= allTasks[key].Item2);
                }
            }

            // Makespan objective.
            IntVar objVar = model.NewIntVar(0, horizon, "makespan");

            List<IntVar> ends = new List<IntVar>();
            for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
            {
                var job = allJobs[jobID];
                var key = Tuple.Create(jobID, job.Count() - 1);
                ends.Add(allTasks[key].Item2);
            }
            model.AddMaxEquality(objVar, ends);
            model.Minimize(objVar);

            // Solve
            CpSolver solver = new CpSolver();
            CpSolverStatus status = solver.Solve(model);
            Console.WriteLine($"Solve status: {status}");

            if (status == CpSolverStatus.Optimal || status == CpSolverStatus.Feasible)
            {
                Console.WriteLine("Solution:");

                Dictionary<int, List<AssignedTask>> assignedJobs = new Dictionary<int, List<AssignedTask>>();
                for (int jobID = 0; jobID < allJobs.Count(); ++jobID)
                {
                    var job = allJobs[jobID];
                    for (int taskID = 0; taskID < job.Count(); ++taskID)
                    {
                        var task = job[taskID];
                        var key = Tuple.Create(jobID, taskID);
                        int start = (int)solver.Value(allTasks[key].Item1);
                        if (!assignedJobs.ContainsKey(task.machine))
                        {
                            assignedJobs.Add(task.machine, new List<AssignedTask>());
                        }
                        assignedJobs[task.machine].Add(new AssignedTask(jobID, taskID, start, task.duration));
                    }
                }

                // Create per machine output lines.
                String output = "";
                foreach (int machine in allMachines)
                {
                    // Sort by starting time.
                    assignedJobs[machine].Sort();
                    String solLineTasks = $"Machine {machine}: ";
                    String solLine = "           ";

                    foreach (var assignedTask in assignedJobs[machine])
                    {
                        Plano value = Planos.Where(x => x.IdJob == dictJob[assignedTask.jobID] && x.PosOperacao == assignedTask.taskID + 1).FirstOrDefault();
                        if (value == null)
                        {
                            response._statusCode = 400;
                            response._response = "Erro na atribuição automática dos tempos";
                            return Ok(response);
                        }
                        value.TempoInicial = assignedTask.start;
                        value.TempoFinal = assignedTask.start + assignedTask.duration;
                        String name = $"job_{assignedTask.jobID}_task_{assignedTask.taskID}";
                        // Add spaces to output to align columns.
                        solLineTasks += $"{name,-15}";

                        String solTmp = $"[{assignedTask.start},{assignedTask.start + assignedTask.duration}]";
                        // Add spaces to output to align columns.
                        solLine += $"{solTmp,-15}";
                    }
                    output += solLineTasks + "\n";
                    output += solLine + "\n";
                }
                // Finally print the solution found.
                Console.WriteLine($"Optimal Schedule Length: {solver.ObjectiveValue}");
                Console.WriteLine($"\n{output}");

                Plano item = new();

                foreach (Plano plano in Planos)
                {
                    _context.Entry(plano).State = EntityState.Modified;

                    try { await _context.SaveChangesAsync(); }
                    catch (DbUpdateConcurrencyException)
                    {
                        response._statusCode = 406;
                        response._response = "Erro na inserção dos planos na base de dados";
                        return Ok(response); 
                    }
                    if (plano.TempoFinal > item.TempoFinal) item = plano;
                }
                response._statusCode = 200;
                response._response = $"A operação {item.IdOperacao} do job {item.IdJob} termina em ultimo lugar às {item.TempoFinal} unidades de tempo";
                return Ok(response); 
            }
            else
            {
                response._statusCode = 417; // Expectation Failed
                response._response = "Não foi encontrada uma solução para o problema apresentado";
                return Ok(response);
            }
        }
        #endregion

        #region Download tabela de produção
        [HttpGet("files/tabela/{IdSimulacao}")]
        public async Task<ActionResult> DownloadTabela(int IdSimulacao)
        {
            Response response = new();
            var query = _context.Plano.AsQueryable();
            query = query.Where(plano => plano.IdSimulacao == IdSimulacao);
            var itens = await query.ToListAsync();
            if (itens == null || itens == default)
            {
                response._statusCode = 409;
                response._response = "Erro na obtenção da simulação";
                return Ok(response);
            }

            await WriteTabela(IdSimulacao, itens);
            var filePath = "Download/TabelaProducao.txt";
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
        public static async Task WriteTabela(int IdSimulacao, List<Plano> itens)
        {
            Response response = new();
            string text = $"Simulacao {IdSimulacao}\n\n";
            List<Plano> sortList = itens.OrderBy(o => o.IdJob).ToList();

            if(sortList.Count > 0)
            {
                Plano previous = default;

                foreach (Plano item in sortList)
                {
                    if(previous == null) text += $"JOB {item.IdJob}: ({item.IdMaquina},{item.UnidadeTempo})";
                    else if (previous.IdJob != item.IdJob || previous == null) text += $"\nJOB {item.IdJob}: ({item.IdMaquina},{item.UnidadeTempo})";
                    else text += $" ({item.IdMaquina},{item.UnidadeTempo})";
                    previous = item;
                }
            }
            using StreamWriter write = new StreamWriter("Download/TabelaProducao.txt");
            write.WriteLine(text);
        }
        #endregion

        #region Download plano de produção
        [HttpGet("files/plano/{IdSimulacao}")]
        public async Task<ActionResult> DownloadPlano(int IdSimulacao)
        {
            Response response = new();
            var query = _context.Plano.AsQueryable();
            query = query.Where(plano => plano.IdSimulacao == IdSimulacao);
            var itens = await query.ToListAsync();
            if (itens == null || itens == default)
            {
                response._statusCode = 409;
                response._response = "Erro na obtenção da simulação";
                return Ok(response);
            }
            
            await WritePlano(IdSimulacao, itens);
            var filePath = "Download/PlanoProducao.txt";
            var bytes = await System.IO.File.ReadAllBytesAsync(filePath);
            return File(bytes, "text/plain", Path.GetFileName(filePath));
        }
        public static async Task WritePlano(int IdSimulacao, List<Plano> itens)
        {
            string text = $"Simulacao {IdSimulacao}\n\n";
            foreach (var iten in itens)
            {
                text = text + $"JOB: {iten.IdJob} | OP: {iten.PosOperacao} | MAQ: {iten.IdMaquina} | TI: {iten.TempoInicial} | DUR: {iten.UnidadeTempo} | TF: {iten.TempoFinal}\n";
            }
            using StreamWriter write = new StreamWriter("Download/PlanoProducao.txt");
            write.WriteLine(text);
        }
        #endregion

        #region Obter todas as posições de determinada simulação
        [HttpGet("{IdSimulacao}")]
        public async Task<ActionResult<IEnumerable<Plano>>> GetPlanos(int IdSimulacao)
        {
            Response response = new();
            var query = _context.Plano.AsQueryable();
            query = query.Where(plano => plano.IdSimulacao == IdSimulacao);
            var itens = await query.ToListAsync();
            if (itens == null || itens == default)
            {
                response._statusCode = 409;
                response._response = "Erro na obtenção da simulação";
                return Ok(response);
            }

            return Ok(itens);
        }
        #endregion

        #region Obter todas as simulações
        // GET: api/Plano/simulacoes
        [HttpGet("/simulacoes")]
        public async Task<ActionResult<IEnumerable<Plano>>> GetAllSimulacoes()
        {
          
            Response aux = new();
            var simulacoes = await _context.Plano.ToListAsync();
            if(simulacoes.Count == 0)
            {
                aux._statusCode = 404;
                aux._response = "Não foram encontradas simulações";
               
                return Ok(aux);
            }
            else return Ok(simulacoes);
        }
        #endregion

        private int GetUtilizadorID() { return int.Parse(this.User.Claims.First(i => i.Type == "Id").Value); }
        private Utilizador GetRegistoUtilizador(int id) { return _context.Utilizador.SingleOrDefault(e => e.Id == id); }
    }
}
