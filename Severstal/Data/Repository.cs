using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Interop.Word;
using Severstal.Core;
using Severstal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Severstal.Data
{
    public class Repository : IRepository
    {
        private readonly AppContext _context;

        public Repository(AppContext context)
        {
            _context = context;
        }

        // Соединение с БД
        public async Task<List<LoadReport>> GetTaskLoadReportAsync()
        {
            var result = await (
                from employee in _context.Employees
                join department in _context.Departments on employee.DepartmentNumber equals department.DepartmentId
                join objective in _context.Objectives on employee.TabelNumber equals objective.TabelNumber
                group new { employee, department } by new { employee.Surname, employee.Name, employee.FathersName, department.DepartmentName } into grouped
                select new LoadReport
                {
                    Surname = grouped.Key.Surname,
                    Name = grouped.Key.Name,
                    FathersName = grouped.Key.FathersName,
                    DepartmentName = grouped.Key.DepartmentName,
                    TaskNumber = grouped.Count()
                }
            ).ToListAsync();

            return result;
        }
    }
}
