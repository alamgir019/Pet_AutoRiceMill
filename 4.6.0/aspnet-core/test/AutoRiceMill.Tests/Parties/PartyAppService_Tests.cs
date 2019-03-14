using AutoRiceMill.Parties;
using AutoRiceMill.Parties.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AutoRiceMill.Tests.Parties
{
    public class PartyAppService_Tests:AutoRiceMillTestBase
    {
        private readonly IPartyAppService _partyAppService;

        public PartyAppService_Tests()
        {
            _partyAppService = Resolve<IPartyAppService>();
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_All_Parties()
        {
            //Act
            var output = await _partyAppService.GetAll(new GetAllPartiesInput() { IsActive=true});

            //Assert
            output.Items.Count.ShouldBe(1);
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Filtered_Parties()
        {
            //Act
            var output = await _partyAppService.GetAll(new GetAllPartiesInput { IsActive = true });

            //Assert
            output.Items.ShouldAllBe(t => t.isActive == true);
        }
    }
}
