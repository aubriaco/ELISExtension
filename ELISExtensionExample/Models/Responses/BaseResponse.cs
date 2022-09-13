namespace ELISExtension.Models.Responses
{
    public class BaseResponse
    {
        public int errorCode { get; set; }
        public string errorMessage { get; set; }

        public BaseResponse()
        {
            errorCode = 0;
            errorMessage = null;
        }

        public BaseResponse(int errorCode, string errorMessage)
        {
            this.errorCode = errorCode;
            this.errorMessage = errorMessage;
        }
    }
}
