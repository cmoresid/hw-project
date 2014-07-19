using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Ninject;
using Ninject.Parameters;
using Ninject.Syntax;
using CompanyABC.Domain.Repositories;

namespace CompanyABC.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel _kernel;

        public NinjectDependencyResolver()
        {
            _kernel = new StandardKernel();
            CreateBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void CreateBindings()
        {
            _kernel.Bind<IProductRepository>().To<EFProductRepository>();
        }
    }
}