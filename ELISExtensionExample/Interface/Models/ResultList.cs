namespace ELISExtension.Interface.Models
{
    public class ResultList : Base
    {
        public Order order { get; set; }
        public List<Result> results { get; set; } 
    }
}
