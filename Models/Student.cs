using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Models
{
    public class Student
    {
        [Key]
        public int Id { get; set; }
      
        public string Name { get; set; }

        public int RoomNumber { get; set; }

        public string Department { get; set; }

        public DateTime AdmissionDate { get; set; }
    }
}
