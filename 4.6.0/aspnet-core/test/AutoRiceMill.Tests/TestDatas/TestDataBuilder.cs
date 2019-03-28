using AutoRiceMill.EntityFrameworkCore;
using AutoRiceMill.Parties;
using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Tests.TestDatas
{
    public class TestDataBuilder
    {
        private readonly AutoRiceMillDbContext _context;

        public TestDataBuilder(AutoRiceMillDbContext context)
        {
            _context = context;

        }

        public void Build()
        {
            var neo = new Party() {Area = "Dhaka", ContactNo = "01", isActive = true, isCashParty = false, Name = "neo" };
            _context.Parties.Add(neo);
            _context.SaveChanges();

            //_context.Parties.AddRange(
            //    new Party() {Id= neo.Id,Area="Dhaka",ContactNo="01",isActive=true,isCashParty=false,Name="neo"}
            //    //new Party() { Name = "alamgir" } 
            //    );
        }
    }
}
