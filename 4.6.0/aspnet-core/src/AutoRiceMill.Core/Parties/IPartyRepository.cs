using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties
{
    public interface IPartyRepository : IRepository<Party, int>
    {
        /// <summary>
        /// Gets all tasks with <see cref="Task.AssignedPerson"/> is retrived (Include for EntityFramework, Fetch for NHibernate)
        /// filtered by given conditions.
        /// </summary>
        /// <param name="assignedPersonId">Optional assigned person filter. If it's null, not filtered.</param>
        /// <param name="state">Optional state filter. If it's null, not filtered.</param>
        /// <returns>List of found tasks</returns>
        //List<Party> GetAllWithPeople(int? assignedPersonId);
    }
}
