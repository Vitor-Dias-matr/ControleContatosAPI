namespace ControleContatosAPI.ViewModel
{
    public class ResponseViewModel
    {
        public object data { get; set; }
        public StatusResponseViewModel status { get; set; }
    }

    public class StatusResponseViewModel
    {
        public int code { get; set; }
        public string message { get; set; }
    }
}
