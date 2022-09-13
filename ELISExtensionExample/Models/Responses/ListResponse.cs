namespace ELISExtension.Models.Responses
{
    public class ListResponse : BaseResponse
    {
        public object list { get; set; }

        public ListResponse(object list)
        {
            this.list = list; 
        }
    }
}
