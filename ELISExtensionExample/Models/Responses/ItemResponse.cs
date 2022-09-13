namespace ELISExtension.Models.Responses
{
    public class ItemResponse : BaseResponse
    {
        public object item { get; set; }

        public ItemResponse(object item)
        {
            this.item = item;
        }
    }
}
