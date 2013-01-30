using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.MicroKernel.SubSystems.Configuration;
using System.Web.Mvc;
using SafeDriving.Controllers;
using Castle.MicroKernel;
using SERVICES.ArticleSvc;
using SERVICES.CircuitSvc;
using SERVICES.SessionSvc;
using SERVICES.TypeSessionSvc;
using SERVICES.UtilisateurSvc;
using POCO;
using SERVICES.VehiculeSvc;
using SERVICES.ImageSvc;
using SERVICES.RoleSvc;
using SERVICES.ForumSvc;
using SERVICES.MailSvc;
using SERVICES.PlanningSvc;
using SERVICES.ParcoursSvc;

namespace SafeDriving.IoC
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
            container.Register(Component.For<IArticleService>().ImplementedBy<ArticleService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ICircuitService>().ImplementedBy<CircuitService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ISessionService>().ImplementedBy<SessionService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<ITypeSessionService>().ImplementedBy<TypeSessionService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUtilisateurService<Client>, IClientService>().ImplementedBy<ClientService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IUtilisateurService<Employe>>().ImplementedBy<EmployeService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IVehiculeService>().ImplementedBy<VehiculeService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IRoleService>().ImplementedBy<RoleService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IImageService>().ImplementedBy<ImageService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IForumService>().ImplementedBy<ForumService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IMailService>().ImplementedBy<MailService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IPlanningService>().ImplementedBy<PlanningService>().LifeStyle.PerWebRequest);
            container.Register(Component.For<IParcoursService>().ImplementedBy<ParcoursService>().LifeStyle.PerWebRequest);
        }
    }
}