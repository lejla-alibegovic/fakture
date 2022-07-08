using faktura.Services.IServices;
using faktura.Services.Mappers;
using faktura.Services.Services;
using faktura.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Lifetime;
using Unity.Mvc5;

namespace faktura.Web.App_Start
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            container.RegisterType<IFakturaService, FakturaService>();
            container.RegisterType<IFakturaMapper, FakturaMapper>(new SingletonLifetimeManager());
            container.RegisterType(typeof(IUserStore<>), typeof(UserStore<>));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            container.RegisterType<AccountController>(new InjectionConstructor());
        }
    }
}