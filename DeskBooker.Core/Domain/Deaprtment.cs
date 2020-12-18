using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeskBooker.Core.Domain
{
    // `dept_no` char(4) NOT NULL,
    //   `dept_name` varchar(40) NOT NULL,
    //   PRIMARY KEY (`dept_no`),
    //   UNIQUE KEY `dept_name` (`dept_name`)
    [Table("departments")]
    public class Department {
        // private EmployeesContext context;

        [Key]
        [Required]
        [StringLength(4)]
        public string dept_no { get; set; }

        [Required]
        [StringLength(40)]
        public string dept_name { get; set; }

        public Department()
        {            
        }
    }
}