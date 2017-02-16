using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using Ninject;

namespace Eventer.Web.Api.Utility.Ninject
{
    public class NinjectControllerActivator : IHttpControllerActivator
    {
        private readonly IKernel _kernel;
        private HttpRequestMessage _requestMessage;

        public NinjectControllerActivator(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            _kernel.Rebind<HttpRequestMessage>()
                .ToConstant(request);

            var controller = (IHttpController)_kernel.GetService(controllerType);

            _requestMessage = request;
            _requestMessage.RegisterForDispose(
                new Release(() => _kernel.Release(controller)));

            return controller;
        }

        private class Release : IDisposable
        {
            private readonly Action _release;

            public Release(Action release)
            {
                _release = release;
            }

            public void Dispose()
            {
                _release();
            }
        }
    }
}