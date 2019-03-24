using System;
using System.Linq;
using System.Threading.Tasks;
using OcodyAutoRiceMill.Api.Models.Expenses;
using OcodyAutoRiceMill.Data.Model;
using OcodyAutoRiceMill.Filters;
using OcodyAutoRiceMill.Maps;
using OcodyAutoRiceMill.Queries.Queries;
using Microsoft.AspNetCore.Mvc;

namespace OcodyAutoRiceMill.Server.RestAPI
{
    [Route("api/[controller]")]
    public class ExpensesController : Controller
    {
        private readonly IExpensesQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public ExpensesController(IExpensesQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<ExpenseModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<Expense, ExpenseModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public ExpenseModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<ExpenseModel>(item);
            return model;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<ExpenseModel> Post([FromBody]CreateExpenseModel requestModel)
        {
            var item = await _query.Create(requestModel);
            var model = _mapper.Map<ExpenseModel>(item);
            return model;
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<ExpenseModel> Put(int id, [FromBody]UpdateExpenseModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<ExpenseModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}