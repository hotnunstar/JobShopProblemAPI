namespace BackEndV2.Models
{
    public class Simulacao
    {
        #region Attributes
        int id;             // Automático
        string nome;        // Não nulo
        #endregion

        #region Methods

        #region Constructors
        public Simulacao() { }
        public Simulacao(int id, string nome)
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
        ~Simulacao()
        {
        }
        #endregion

        #endregion
    }
}
