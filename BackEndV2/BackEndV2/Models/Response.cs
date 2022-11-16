namespace BackEndV2.Models
{
    public class Response
    {
        string response;
        int statusCode;
        public Response() { }
        public string _response { get { return response; } set { response = value; } }
        public int _statusCode { get { return statusCode; } set { statusCode = value; } }
    }
    public class Token
    {
        string token;
        public Token() { }
        public string _token { get { return token; } set { token = value; } }
       
    }
}
