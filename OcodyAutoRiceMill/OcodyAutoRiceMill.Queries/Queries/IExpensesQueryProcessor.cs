using System.Linq;
using System.Threading.Tasks;
using OcodyAutoRiceMill.Api.Models.Expenses;
using OcodyAutoRiceMill.Data.Model;

namespace OcodyAutoRiceMill.Queries.Queries
{
    public interface IExpensesQueryProcessor
    {
        IQueryable<Expense> Get();
        Expense Get(int id);
        Task<Expense> Create(CreateExpenseModel model);
        Task<Expense> Update(int id, UpdateExpenseModel model);
        Task Delete(int id);
    }
}