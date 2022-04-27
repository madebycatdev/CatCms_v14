namespace OravPlugin.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public T Data { get; set; }

        public static Response<T> CreateSuccessResponse(T data)
        {
            return new Response<T>() { StatusCode = 0, StatusMessage = "Success", Data = data };
        }

        public static Response<T> CreateSuccessResponse()
        {
            return new Response<T>() { StatusCode = 0, StatusMessage = "Success" };
        }

        public static Response<T> CreateFailResponse(int statusCode, string statusMessage)
        {
            return new Response<T>() { StatusCode = statusCode, StatusMessage = statusMessage };
        }

        public static Response<T> CreateInternalErrorResponse()
        {
            return new Response<T>() { StatusCode = 500, StatusMessage = "Internal Error" };
        }

        public static Response<T> CreateInternalErrorResponse(string statusMessage)
        {
            return new Response<T>() { StatusCode = 500, StatusMessage = statusMessage };
        }

    }
}
