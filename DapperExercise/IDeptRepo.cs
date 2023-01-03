using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExercise
{
    internal interface IDeptRepo
    {
        public IEnumerable<Department> GetAllDepartments(); //Stubbed out method
    }
}
