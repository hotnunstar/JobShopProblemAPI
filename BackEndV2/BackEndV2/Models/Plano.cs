namespace BackEndV2.Models
{
    public class Plano
    {
        #region Attributes
        int idJob, idOperacao, idSimulacao, idMaquina, idUtilizador, tempoInicial, unidadeTempo, tempoFinal, posOperacao;
        bool estado;
        #endregion

        #region Methods

        #region Constructors
        public Plano() { }
        public Plano(int idJob, int idOperacao, int idSimulacao, int idMaquina, int idUtilizador, int tempoInicial, int unidadeTempo, int tempoFinal, int posOperacao, bool estado)
        {
            this.idJob = idJob;
            this.idOperacao = idOperacao;
            this.idSimulacao = idSimulacao;
            this.idMaquina = idMaquina;
            this.idUtilizador = idUtilizador;
            this.tempoInicial = tempoInicial;
            this.unidadeTempo = unidadeTempo;
            this.tempoFinal = tempoFinal;
            this.posOperacao = posOperacao;
            this.estado = estado;
        }
        #endregion

        #region Properties
        public int IdJob { get { return idJob; } set { idJob = value; } }
        public int IdOperacao { get { return idOperacao; } set { idOperacao = value; } }
        public int IdSimulacao { get { return idSimulacao; } set { idSimulacao = value; } }
        public int IdMaquina { get { return idMaquina; } set { idMaquina = value; } }
        public int IdUtilizador { get { return idUtilizador; } set { idUtilizador = value; } }
        public int TempoInicial { get { return tempoInicial; } set { tempoInicial = value; } }
        public int UnidadeTempo { get { return unidadeTempo; } set { unidadeTempo = value; } }
        public int TempoFinal { get { return tempoFinal; } set { tempoFinal = value; } }
        public int PosOperacao { get { return posOperacao; } set { posOperacao = value; } }
        public bool Estado { get { return estado; } set { estado = value; } }
        #endregion

        #region Functions
        #endregion

        #region Overrides
        #endregion

        #region Destructor
        /// <summary>
        /// The destructor.
        /// </summary>
        ~Plano()
        {
        }
        #endregion

        #endregion
    }

    public struct googleOperation
    {
        public int machine { get; set; }
        public int duration { get; set; }

        public googleOperation(int machine, int duration)
        {
            this.machine = machine;
            this.duration = duration;
        }
    }

    public class AssignedTask : IComparable
    {
        public int jobID;
        public int taskID;
        public int start;
        public int duration;

        public AssignedTask(int jobID, int taskID, int start, int duration)
        {
            this.jobID = jobID;
            this.taskID = taskID;
            this.start = start;
            this.duration = duration;
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;

            AssignedTask otherTask = obj as AssignedTask;
            if (otherTask != null)
            {
                if (this.start != otherTask.start)
                    return this.start.CompareTo(otherTask.start);
                else
                    return this.duration.CompareTo(otherTask.duration);
            }
            else
                throw new ArgumentException("Object is not a Temperature");
        }
    }
}
