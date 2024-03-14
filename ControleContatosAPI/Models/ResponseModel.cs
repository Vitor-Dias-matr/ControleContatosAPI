namespace ControleContatosAPI.Models
{
    public class ResponseModel
    {
        public object data { get; set; }
        public StatusResponseModel status { get; set; }
    }

    public class StatusResponseModel
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
