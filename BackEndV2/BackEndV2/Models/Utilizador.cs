
namespace BackEndV2.Models
{
    public class Utilizador
    {
        #region Attributes
        int id;             // Automático
        string username;    // Não nulo!
        string password;
        bool admin;         // (true - admin | false - user)
        bool estado;        // (true - ativo | false - desativo)
        #endregion

        #region Methods

        #region Constructors
        public Utilizador() { }
        public Utilizador(int id, string username, string password, bool admin, bool estado)
        {
            this.id = id;
            this.username = username;
            this.password = password;
            this.admin = admin;
            this.estado = estado;
        }
        #endregion

        #region Properties
        public int Id { get { return id; } set { id = value; } }
        public string Username { get { return username; } set { username = value; } }
        public string Password { get { return password; } set { password = value; } }
        public bool Admin { get { return admin; } set { admin = value; } }
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
        ~Utilizador()
        {
        }
        #endregion

        #endregion
    }

    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
