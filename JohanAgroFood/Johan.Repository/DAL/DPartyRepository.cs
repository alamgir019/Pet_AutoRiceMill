using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DPartyRepository:Disposable,IPartyRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DPartyRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<tblCommonElement> GetDistrict()
        {
            List<tblCommonElement> districtList = context.tblCommonElements.Where(z => z.elementCode == 1).ToList();
            return districtList;
        }
        public List<tblParty> GetParty()
        {
            //List<tblParty> partys = context.tblParties.ToList();
            //List<tblParty> results = new List<tblParty>();
            //foreach (var item in partys)
            //{
            //    results.Add(new tblParty() 
            //    { ID=item.ID, name=item.name, contactNo=item.contactNo, area=item.area,
            //     districtId=item.districtId, zoneId=item.zoneId, productId=item.productId, isCashParty=item.isCashParty});
            //}
            var results = from party in context.tblParties
                join eleCom in context.tblCommonElements on party.districtId equals eleCom.ID
                join product in context.tblProducts on party.productId equals product.ID
                select new
                {
                    party.ID,
                    party.name,
                    party.contactNo,
                    party.area,
                    eleCom.elementName,
                    product.productName,
                    party.isCashParty

                };
            List<tblParty> partyList=new List<tblParty>();
            
            foreach (var item in results)
            {
                tblParty objParty=new tblParty();
                objParty.ID = item.ID;
                objParty.name = item.name;
                objParty.contactNo = item.contactNo;
                objParty.area = item.area;
                //objParty.districtName = item.elementName;
                //objParty.zoneName = item.elementName;
                //objParty.zoneName = item.productName;
                objParty.isCashParty = item.isCashParty;
                partyList.Add(objParty);
            }
            
            
            return partyList;
        }
        public bool SaveParty(tblParty party)
        {
            context.tblParties.Add(party);
            return context.SaveChanges() > 0;
        }
        public bool EditParty(tblParty objEditParty)
        {
            var orgEditParty = context.tblParties.Where(ss => ss.ID == objEditParty.ID).FirstOrDefault();
            orgEditParty.name = objEditParty.name;
            orgEditParty.contactNo = objEditParty.contactNo;
            orgEditParty.area = objEditParty.area;
            orgEditParty.zoneId = objEditParty.zoneId;
            orgEditParty.districtId = objEditParty.districtId;
            orgEditParty.productId = objEditParty.productId;
            orgEditParty.isCashParty = objEditParty.isCashParty;

            return context.SaveChanges() > 0;
        }
        public bool Delete(int pk)
        {
            var orgDeleteParty = context.tblParties.Where(ss => ss.ID == pk).FirstOrDefault();


            context.tblParties.Remove(orgDeleteParty);
            return context.SaveChanges() > 0;
        }
    }
}
