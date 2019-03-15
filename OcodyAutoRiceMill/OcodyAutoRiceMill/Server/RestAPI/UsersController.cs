using System;
using System.Linq;
using System.Threading.Tasks;
using OcodyAutoRiceMill.Api.Models.Common;
using OcodyAutoRiceMill.Api.Models.Expenses;
using OcodyAutoRiceMill.Api.Models.Users;
using OcodyAutoRiceMill.Data.Access.Constants;
using OcodyAutoRiceMill.Data.Model;
using OcodyAutoRiceMill.Filters;
using OcodyAutoRiceMill.Maps;
using OcodyAutoRiceMill.Queries.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OcodyAutoRiceMill.Server.RestAPI
{
    [Route("api/[controller]")]
    [Authorize(Roles = Roles.AdministratorOrManager)]
    public class UsersController : Controller
    {
        private readonly IUsersQueryProcessor _query;
        private readonly IAutoMapper _mapper;

        public UsersController(IUsersQueryProcessor query, IAutoMapper mapper)
        {
            _query = query;
            _mapper = mapper;
        }

        [HttpGet]
        [QueryableResult]
        public IQueryable<UserModel> Get()
        {
            var result = _query.Get();
            var models = _mapper.Map<User, UserModel>(result);
            return models;
        }

        [HttpGet("{id}")]
        public UserModel Get(int id)
        {
            var item = _query.Get(id);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        //[HttpPost]
        //[ValidateModel]
        //public async Task<UserModel> Post([FromBody]CreateUserModel requestModel)
        //{
        //    var item = await _query.Create(requestModel);
        //    var model = _mapper.Map<UserModel>(item);
        //    return model;
        //}

        [HttpPost("{id}/password")]
        [ValidateModel]
        public async Task ChangePassword(int id,[FromBody]ChangeUserPasswordModel requestModel)
        {
            await _query.ChangePassword(id, requestModel);
        }

        [HttpPut("{id}")]
        [ValidateModel]
        public async Task<UserModel> Put(int id, [FromBody]UpdateUserModel requestModel)
        {
            var item = await _query.Update(id, requestModel);
            var model = _mapper.Map<UserModel>(item);
            return model;
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _query.Delete(id);
        }
    }
}