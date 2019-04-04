using Abp.Application.Services.Dto;
using Abp.Runtime.Validation;
using AutoRiceMill.Parties;
using AutoRiceMill.Parties.Dtos;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async System.Threading.Tasks.Task Should_Create_Party_With_Name()
        {
            await _partyAppService.Create(new CreatePartyInput { Name = "My first party" });
            UsingDbContext(context =>
            {
                var party1 = context.Parties.FirstOrDefault(p => p.Name == "My first party");
                party1.ShouldNotBeNull();
            });
        }
        [Fact]
        public async System.Threading.Tasks.Task Should_Create_Party_With_AllProperty()
        {
            await _partyAppService.Create(new CreatePartyInput {
                Name="Party2", Area="Area2",ContactNo="Contact1"
            });
            UsingDbContext(context=>
            {
                var party1 = context.Parties.FirstOrDefault(p=>p.Name=="Party2" && p.Area=="Area2");
                party1.ShouldNotBeNull();
            });
        }
        [Fact]
        public async System.Threading.Tasks.Task Should_Not_Create_Party_Without_Name()
        {
            await Assert.ThrowsAsync<AbpValidationException>(async () =>
            {
                await _partyAppService.Create(new CreatePartyInput
                {
                    Area = "Area2",
                    ContactNo = "Contact1"
                });
            });
        }

        [Fact]
        public void Should_Change_Party()
        {
            //We can work with repositories instead of DbContext
            var partyRepository = LocalIocManager.Resolve<IPartyRepository>();

            //Obtain test data
            var party = GetParty("neo");
            party.ShouldNotBe(null);
            UpdatePartyInput newParty = new UpdatePartyInput
            {
                Id = party.Id,
                Name = "vio",
                Area = "cio",
                ContactNo = "01",
                IsActive = true
            };
            //Run SUT
            _partyAppService.UpdateParty(newParty);
            //partyRepository.Get(party.Id).Name.ShouldBe(newParty.Name);
            //Check result
            var upadateParty = partyRepository.Get(party.Id);
            upadateParty.Name.ShouldBe(newParty.Name);
            upadateParty.Area.ShouldBe(newParty.Area);
            upadateParty.ContactNo.ShouldBe(newParty.ContactNo);
            upadateParty.isActive.ShouldBe(newParty.IsActive);
        }

        private Party GetParty(string name)
        {
            var party= UsingDbContext(context => context.Parties.FirstOrDefault(p => p.Name == name));
            //party.ShouldNotBeNull();
            return party;
        }

        [Fact]
        public async System.Threading.Tasks.Task Should_Get_All_Parties()
        {
            //Act
            var output = await _partyAppService.GetAll(new GetAllPartiesInput() { IsActive = true });

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


        [Fact]
        public async System.Threading.Tasks.Task Should_Get_Party_Details()
        {
            //Obtain test data
            var party = GetParty("neo");
            party.ShouldNotBe(null);
            //Act
            var output = await _partyAppService.Get(new EntityDto { Id = party.Id });
            //Assert
            output.ShouldNotBe(null);
        }
        [Fact]
        public async System.Threading.Tasks.Task Should_Delete_Party()
        {
            //Obtain test data
            var party = GetParty("neo");
            party.ShouldNotBe(null);
            //Act
            await _partyAppService.Delete(new EntityDto() { Id=party.Id});
            //Assert
            GetParty("neo").ShouldBe(null);
        }
    }
}
