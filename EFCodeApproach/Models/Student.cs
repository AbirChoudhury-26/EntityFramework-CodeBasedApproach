using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCodeApproach.Models
{
    public class Student
    { 

        [Key]
        public int Id { get; set; }

        
        [Column("StudentName",TypeName ="varchar(100)")]
		[System.ComponentModel.DataAnnotations.Required]
		public string Name { get; set; }  // This Column field is used for specifying with which name we wanto specify our field names and their data types also


		[Column("Gender", TypeName = "varchar(20)")]
        [System.ComponentModel.DataAnnotations.Required]
        public string Gender { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public int? Age { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public int? Standard { get; set; }
    }
}
