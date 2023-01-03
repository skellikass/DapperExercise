using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperExercise
{
    internal class DapperDeptRepo : IDeptRepo
    {
        private readonly IDbConnection _connection;
        public DapperDeptRepo(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Department> GetAllDepartments()
        {
            return _connection.Query<Department>("SELECT * FROM departments;");
        }
        public void InsertDepartment(string newDeptName)
        {
            _connection.Execute("INSERT INTO DEPARTMENTS (Name) VALUES (@departmentName);",
             new { departmentName = newDeptName });
        }
    }
}
