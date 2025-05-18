using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkersManagement.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string EmployeeId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        public string Position { get; set; }
        public DateTime DOB { get; set; }
        public string Phone { get; set; }
        public string Education { get; set; }
    }
}
