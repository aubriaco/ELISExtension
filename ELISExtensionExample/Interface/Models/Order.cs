namespace ELISExtension.Interface.Models
{
    public class Order : Base
    {
        public int id { get; set; }
        public string oid { get; set; }
        public int patientId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string birthDate { get; set; }
        public string specimenId { get; set; }

        public Address address { get; set; }
        public DateTime finalDate { get; set; }
        public DateTime releasedDate { get; set; }
        public DateTime collectedDate { get; set; }
        public DateTime receivedDate { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime statusDate { get; set; }
        public string status { get; set; }
    
        public bool resulted { get; set; }
        public bool final { get; set; }
        public int issueCount { get; set; }
    }
}
