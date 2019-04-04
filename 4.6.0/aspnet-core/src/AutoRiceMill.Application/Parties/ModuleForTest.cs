using System;
using System.Collections.Generic;
using System.Text;

namespace AutoRiceMill.Parties
{
    public class ModuleForTest: AutoRiceMillAppServiceBase
    {
        public virtual void GetMessage()
        {
            Logger.Info("I am Module Class");
        }

    }
    public class SubModule1 : ModuleForTest
    {
        public override void GetMessage()
        {
            Logger.Info("I am sub Module 1");
        }
        public void GetInfo()
        {
            Logger.Info("I am sub Module 1 info");
        }
    }
    public class SubModule2 : ModuleForTest
    {
        public override void GetMessage()
        {
            Logger.Info("I am sub Module 2");
        }
        public void GetInfo()
        {
            Logger.Info("I am sub Module 2 Info");
        }
    }
    public class Calling
    {
        ModuleForTest module = new ModuleForTest();
        SubModule1 subModule1 = new SubModule1();
        SubModule2 subModule2 = new SubModule2();
        public void call1() {
            module.GetMessage();
            subModule1.GetMessage();
            subModule2.GetMessage();
        }
        public void call2()
        {
            module = subModule1;
            module.GetMessage();
        }
        public void call3() {
            //subModule1 = module;
            subModule1.GetMessage();
            subModule1.GetInfo();
        }
        public void call4() {
            //subModule1 = subModule2;
            subModule1.GetMessage();
        }
    }
}
