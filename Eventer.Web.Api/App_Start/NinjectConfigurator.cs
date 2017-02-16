using System.Reflection;
using System.Web.Http.Dispatcher;
using Eventer.Dal.Context;
using Eventer.Mapper.ModelFacotry.ApiActivity;
using Eventer.Mapper.ModelFacotry.Category;
using Eventer.Mapper.ModelFacotry.City;
using Eventer.Mapper.ModelFacotry.Client;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Mapper.ModelFacotry.ErrorLog;
using Eventer.Mapper.ModelFacotry.Event;
using Eventer.Mapper.ModelFacotry.RefreshToken;
using Eventer.Mapper.ModelFacotry.State;
using Eventer.Mapper.RoleModelFactory;
using Eventer.Domain.Entity;
using Eventer.Repository.UoW;
using Eventer.Service.ApiActivity;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Service.Client;
using Eventer.Service.Client.Interface;
using Eventer.Service.ErrorLog;
using Eventer.Service.ErrorLog.Interface;
using Eventer.Service.EventerService;
using Eventer.Service.EventerService.Interface;
using Eventer.Service.RefreshToken;
using Eventer.Service.RefreshToken.Interface;
using Eventer.Service.RoleManager;
using Eventer.Service.RoleService;
using Eventer.Utility.CustomLogger;
using Eventer.Utility.EventQueryStringPatternMatch;
using Eventer.Utility.HashGenerator;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Ninject;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject;
using Ninject.Web.Common;

namespace Eventer.Web.Api
{
    public class NinjectConfigurator
    {
        public static StandardKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            RegisterServices(kernel);
            return kernel;
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IHttpControllerActivator>().To<NinjectControllerActivator>();

            kernel.Bind<EventerDbContext>().ToSelf();
            kernel.Bind<IUserStore<User>>().To<UserStore<User>>().InRequestScope().WithConstructorArgument("context", kernel.Get<EventerDbContext>());
            kernel.Bind<UserManager<User>>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();

            kernel.Bind<IRoleStore<IdentityRole, string>>().To<RoleStore<IdentityRole, string, IdentityUserRole>>().InRequestScope().WithConstructorArgument("context", kernel.Get<EventerDbContext>());
            kernel.Bind<RoleManager<IdentityRole>>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationRoleManager>().ToSelf().InRequestScope();

            kernel.Bind<ICustomException>().To<CustomException>().InRequestScope();
            kernel.Bind<IGenerator>().To<Generator>().InRequestScope();
            kernel.Bind<IPatternMatching>().To<PatternMatching>().InRequestScope();
            kernel.Bind<IPatternMatchingCount>().To<PatternMatchingCount>().InRequestScope();
            kernel.Bind<ICustomLogger>().To<CustomLogger>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();

            kernel.Bind<IApiActivityLogService>().To<ApiActivityLogService>().InRequestScope();
            kernel.Bind<ICategoryService>().To<CategoryService>().InRequestScope();
            kernel.Bind<ICityService>().To<CityService>().InRequestScope();
            kernel.Bind<IClientService>().To<ClientService>().InRequestScope();
            kernel.Bind<IErrorLogService>().To<ErrorLogService>().InRequestScope();
            kernel.Bind<IEventService>().To<EventService>().InRequestScope();
            kernel.Bind<IRefreshTokenService>().To<RefreshTokenService>().InRequestScope();
            kernel.Bind<IRoleService>().To<RoleService>().InRequestScope();
            kernel.Bind<IStateService>().To<StateService>().InRequestScope();


            kernel.Bind<IModelFactory>().To<ApiActivityLogFactory>().InRequestScope().Named("ApiActivityLogFactory");
            kernel.Bind<IModelFactory>().To<CategoryFactory>().InRequestScope().Named("CategoryFactory");
            kernel.Bind<IModelFactory>().To<CityFactory>().InRequestScope().Named("CityFactory");
            kernel.Bind<IModelFactory>().To<ClientFactory>().InRequestScope().Named("ClientFactory");
            kernel.Bind<IModelFactory>().To<ErrorLogFactory>().InRequestScope().Named("ErrorLogFactory");
            kernel.Bind<IModelFactory>().To<EventFactory>().InRequestScope().Named("EventFactory");
            kernel.Bind<IModelFactory>().To<RefreshTokenFactory>().InRequestScope().Named("RefreshTokenFactory");
            kernel.Bind<IModelFactory>().To<StateFactory>().InRequestScope().Named("StateFactory");
            kernel.Bind<IRoleModelFactory>().To<RoleModelFactory>().InRequestScope();
        }
    }
}