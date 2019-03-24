using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Reporting.WebForms;

namespace Johan.Repository
{
    public class DEmployeeRepository : Disposable, IEmployeeRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DEmployeeRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {

            this.context = context;
        }
        public List<Employee> GetAllEmployee()
        {
            var data = context.Employees.ToList();
            return data;
        }


        // just try to avoid opening values
        public Employee SaveEmp(Employee employee)
        {
            try
            {
                long maxId = context.Employees.Select(p => p.ID).DefaultIfEmpty(0).Max();
                employee.ID = ++maxId;
                context.Employees.Add(employee);
                                              
                return context.SaveChanges() > 0 ? employee : null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }

        public Employee EditEmp(Employee employee)
        {
            var orgEmp = context.Employees.Where(ss => ss.ID == employee.ID).FirstOrDefault();

            #region edit employee
            orgEmp.empName = employee.empName;
            orgEmp.active = employee.active;
            orgEmp.address = employee.address;
            orgEmp.contact = employee.contact;
            orgEmp.currentSalary = employee.currentSalary;
            orgEmp.joiningDate = employee.joiningDate;
            orgEmp.designation = employee.designation;
            #endregion

            return context.SaveChanges() > 0 ? employee :null;
        }

        public List<SalaryPayment> GetEmpSal(SalaryPayment salary)
        {
            try
            {
                var result = context.SalaryPayments.Where(ss => ss.empId == salary.empId && ss.month == salary.month && ss.year == salary.year).ToList();
                return result;
            }
            catch (Exception exc)
            {   
                throw exc;
            }
        }

        public SalaryPayment EditSalary(SalaryPayment salary)
        {
            var orgSal = context.SalaryPayments.Where(ss => ss.ID == salary.ID).FirstOrDefault();

            #region edit employee
            orgSal.empId = salary.empId;
            orgSal.month = salary.month;
            orgSal.year = salary.year;
            orgSal.paidAmount = salary.paidAmount;
            orgSal.date = salary.date;
            #endregion

            return context.SaveChanges() > 0 ? salary : null;
        }

        public SalaryPayment SaveSalary(SalaryPayment salary)
        {
            try
            {
                long maxId = context.SalaryPayments.Select(p => p.ID).DefaultIfEmpty(0).Max();
                salary.ID = ++maxId;
                context.SalaryPayments.Add(salary);

                return context.SaveChanges() > 0 ? salary : null;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
        }


    }
}