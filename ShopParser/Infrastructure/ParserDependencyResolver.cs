using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using ShopParser.Interfaces;
using ShopParser.Services;

namespace ShopParser.Infrastructure
{
    public class ParserDependencyResolver:IDependencyResolver
    {
        private IKernel kernel;
        public ParserDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            kernel.Bind<ILinkParser>().To<CanUaLinkParser>();
            kernel.Bind<IParsingService>().To<ParsingService>().WithConstructorArgument("parser",x=>x.Kernel.Get<ILinkParser>());
        }
    }
}