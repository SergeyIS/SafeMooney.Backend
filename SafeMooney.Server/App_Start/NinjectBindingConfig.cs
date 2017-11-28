using Ninject.Modules;
using SafeMooney.DAL;
using SafeMooney.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SafeMooney.Server
{
    public class NinjectBindingConfig : NinjectModule
    {
        public override void Load()
        {
            Bind<IDataStorage>().To<DataStorage>();
        }
    }
}