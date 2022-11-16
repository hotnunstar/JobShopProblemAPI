namespace BackEndV2.Models
{
    public class Operacao
    {
        #region Attributes
        int id;           // Automático
        string nome;
        #endregion

        #region Methods
        #region Constructors
        public Operacao() { }
        public Operacao(int id, string nome)
        {
            this.id = id;
            this.nome = nome;
        }
        #endregion

        #region Properties
        public int Id { get { return id; } set { id = value; } }
        public string Nome { get { return nome; } set { nome = value; } }
        #endregion

        #region Functions
        #endregion

        #region Overrides
        #endregion

        #region Destructor
        /// <summary>
        /// The destructor.
        /// </summary>
        ~Operacao()
        {
        }
        #endregion

        #endregion
    }
}
