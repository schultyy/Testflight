using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Testflight.Core.Build;
using Testflight.Core.Publish;
using Testflight.DataAccess;
using Testflight.Scheduling;
using Unity.Mvc4;

namespace Testflight.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();    
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IMongoSession, MongoSession>();
            container.RegisterType<IFilesystemProvider, FilesystemProvider>();
            container.RegisterType<IBuilderCapability, MSBuild>();
            container.RegisterType<Builder>();
            container.RegisterInstance<IScheduler>(new Scheduler());
        }
    }
}