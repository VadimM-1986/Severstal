using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal.Model
{
    public class LoadReport
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string FathersName { get; set; }
        public string DepartmentName { get; set; }
        public int TaskNumber { get; set; }
    }
}
