using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public string TabelNumber {  get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public DateTime BirthDate { get; set; }
        public byte DepartmentNumber { get; set; }
    }
}
