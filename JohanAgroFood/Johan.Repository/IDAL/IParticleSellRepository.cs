using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public interface IParticleSellRepository : IDisposable
    {
        List<tblSell> GetParticleInfo();

        tblSell SaveParticle(tblSell particleInfo);
        tblSell EditParticleSell(tblSell particleInfo);
        bool DeleteParticleSell(int pk);
        //List<object> GetParticleInfoRpt(tblSell particleRpt);


        List<STK_tblStock> GetStock();


        bool SaveParticleStock(STK_Balance particleStk);

        List<STK_Balance> GetParticleStock();

        bool DeleteParticleStock(STK_Balance particleStk);

        bool EditParticleStock(STK_Balance particleStk);

        List<object> GetParticleGeneralRpt(tblSell particleRpt);

        ReportViewModel ParticleGeneralViewModel(List<object> objLst, string from, string to);
    }
}
