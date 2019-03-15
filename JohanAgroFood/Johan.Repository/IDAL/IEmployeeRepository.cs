using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IEmployeeRepository : IDisposable
    {
        List<Employee> GetAllEmployee();
        Employee SaveEmp(Employee employee);
        Employee EditEmp(Employee employee);

        List<SalaryPayment> GetEmpSal(SalaryPayment salary);

        SalaryPayment EditSalary(SalaryPayment salary);

        SalaryPayment SaveSalary(SalaryPayment salary);
    }
}
