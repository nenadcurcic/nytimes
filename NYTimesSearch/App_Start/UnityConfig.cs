using NYTimesSearch.Controllers;
using NYTimesSearch.Services;
using System.Web.Mvc;
using Unity;
using Unity.Injection;
using Unity.Mvc5;

namespace NYTimesSearch
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.RegisterType<AccountController>(new InjectionConstructor());
            container.RegisterType<IDbService, DbService>();
            container.RegisterType<INewsService, NewsService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}