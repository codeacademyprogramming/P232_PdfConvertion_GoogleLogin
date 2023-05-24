namespace Api.Common
{
    public class RestExceptionResponse
    {
        public List<KeyValuePair<string, string>> Errors { get; set; } = new List<KeyValuePair<string, string>>();
        public string Message { get; set; }
    }
}
