using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using Direction.Controllers;
using SERVICES.ForumSvc;
using SERVICES.ImageSvc;
using POCO;
using SERVICES.UtilisateurSvc;
using SERVICES.RoleSvc;
using SERVICES.ArticleSvc;
using SERVICES.ExamenSvc;
using SERVICES.PlanningSvc;
using SERVICES.SessionSvc;

namespace Direction.IoC
{
    public class RegisterController : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            ControllersRegistration(container);
            ServicesRegistration(container);
        }

        private ConfigureDelegate ConfigureLifeStyle()
        {
            return c => c.LifeStyle.Transient;
        }



        private void ControllersRegistration(IWindsorContainer container)
        {
            container.Register(AllTypes.FromThisAssembly().BasedOn<IController>().If(Component.IsInSameNamespaceAs<HomeController>()).If(t => t.Name.EndsWith("Controller")).Configure(ConfigureLifeStyle()));
        }

        private void ServicesRegistration(IWindsorContainer container)
        {
            container.Register(Component.For<IForumService>().ImplementedBy<ForumService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IImageService>().ImplementedBy<ImageService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUtilisateurService<Client>, IClientService>().ImplementedBy<ClientService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUtilisateurService<Employe>, IEmployeService>().ImplementedBy<EmployeService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IArticleService>().ImplementedBy<ArticleService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IExamenService>().ImplementedBy<ExamenService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IPlanningService>().ImplementedBy<PlanningService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ISessionService>().ImplementedBy<SessionService>().LifeStyle.PerWebRequest);

        }
    }
}