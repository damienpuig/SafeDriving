using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using System.Web;
using SERVICES.RoleSvc;
using System.Web.Security;
using AutoMapper;

namespace SERVICES.UtilisateurSvc
{
    public class EmployeService : IUtilisateurService<POCO.Employe>, IEmployeService
    {
        public SafeDrivingEntities context { get; set; }
        public IRoleService RoleService { get; set; }

        public EmployeService(IRoleService roleService)
        {
            RoleService = roleService;
            context = SafeDrivingEntities.contexteDatas;
        }

        public string GetUtilisateurCourantPseudo()
        {
            string pseudo = HttpContext.Current.User.Identity.Name;
            return pseudo;
        }

        public Resultat<IList<POCO.Employe>> GetEntities()
        {
            return Resultat<IList<POCO.Employe>>.SafeExecute<EmployeService>(
                result =>
                {
                    var employes = context.Utilisateurs.OfType<Employe>().ToList();
                    result.Valeur = employes;
                });
        }

        public Resultat<POCO.Employe> GetEntityById(int id)
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
             result =>
             {
                 var employe = context.Utilisateurs.OfType<Employe>().Where(u => u.Id == id).First();
                 result.Valeur = employe;
             });
        }

        public Resultat<POCO.Employe> GetEntityByPseudo(string pseudo)
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
               result =>
               {
                   var employe = context.Utilisateurs.OfType<Employe>().Where(u => u.Pseudo == pseudo).First();
                   result.Valeur = employe;
               });
        }

        public Resultat<bool> IsPseudoUnique(string pseudo)
        {
            return Resultat<bool>.SafeExecute<EmployeService>(
                result =>
                {
                    int pseudoEnBase = (from p in context.Utilisateurs where p.Pseudo.Equals(pseudo) select p).Count();

                    if (pseudoEnBase == 0)
                    {
                        result.Valeur = true;
                    }
                    else
                    {
                        result.Valeur = false;
                    }
                });
        }

        public Resultat<IList<Employe>> GetEntitiesByRole(Privilege role)
        {
            return Resultat<IList<Employe>>.SafeExecute<EmployeService>(
               result =>
               {
                   int roleId = (int)role;
                   var employes = (from c in context.Utilisateurs.OfType<Employe>() where c.RoleId == roleId select c).ToList();
                   result.Valeur = employes;
               });
        }

        public Resultat<Employe> CreateEmploye(CreateClientCommand command)
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
                result =>
                {
                    //On encode le password 
                    command.Password = EncodePassword(command.Password);

                    //On mappe la commande en objet Client
                    var employe = Mapper.Map<CreateClientCommand, Employe>(command);

                    employe.Avatar = command.Avatar;

                    //On récupère le Role passé par le controller via la commande
                    var roleResult = RoleService.GetRoleByPrivilege(command.Role);

                    if (roleResult.IsValid)
                    {
                        employe.Role = roleResult.Valeur;
                        employe.RoleId = roleResult.Valeur.Id;
                        context.Utilisateurs.AddObject(employe);
                        context.SaveChanges();
                        result.Valeur = employe;
                    }
                });
        }

        public Resultat<Employe> UpdateEmploye(UpdateClientCommand command)
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
                result =>
                {
                
                });
        }

        public Resultat<Employe> ValidateEntity(string login, string password)
        {
            return Resultat<Employe>.SafeExecute<EmployeService>(
                result =>
                {
                    string CurrentPass = EncodePassword(password);
                    var employe = context.Utilisateurs.OfType<Employe>().Where(c => c.Pseudo == login).Where(c => c.Password == CurrentPass).First();
                    result.Valeur = employe;
                });
        }

        public Resultat<Employe> SignIn(Employe entityToSignIn, bool isCookiePersistent)
        {
            return Resultat<Employe>.SafeExecute<EmployeService>(
              result =>
              {
                  FormsAuthentication.SetAuthCookie(entityToSignIn.Pseudo, isCookiePersistent);

              });
        }

        Resultat<string> IUtilisateurService<Employe>.GetUtilisateurCourantPseudo()
        {
            return Resultat<string>.SafeExecute<EmployeService>(
                result =>
                {
                    string pseudo = HttpContext.Current.User.Identity.Name;
                    result.Valeur = pseudo;
                });
        }

        //Méthode pour encoder le mot de passe de l'employe
        private string EncodePassword(string password)
        {
            // calcule le sha1 du password puis le convertit en base64
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            var sha1Password = sha.ComputeHash(passwordBytes);
            var base64Password = Convert.ToBase64String(sha1Password);
            return base64Password;
        }



        // SAR

        public Resultat<POCO.Employe> RemoveEntityById(int Id)
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
      result =>
      {

      });
        }


        public Resultat<Employe> RemoveEntities()
        {
            return Resultat<POCO.Employe>.SafeExecute<EmployeService>(
result =>
{

});
        }


        public Resultat<Privilege> GetRoleByEntityId(int id)
        {
            return Resultat<Privilege>.SafeExecute<EmployeService>(
               result =>
               {

                   var roleId = context.Utilisateurs.OfType<Employe>().Where(c => c.Id == id).FirstOrDefault().RoleId;
                   var role = context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
                   var pr = (Privilege)role.Id;

                   result.Valeur = pr;
               });
        }


        public Resultat<IList<ClientNoteDto>> GetClientsNoteByFormateur(Employe employe)
        {
          return Resultat<IList<ClientNoteDto>>.SafeExecute<EmployeService>(
                result =>
                {
                    var sessions = context.Sessions.Where(s => s.EmployeId == employe.Id).Where(s=>s.HeureFin < DateTime.Now);

                    IList<ClientNoteDto> listClientDto = new List<ClientNoteDto>();

                    foreach (Session itemSession in sessions)
                    {

                        var HistoriqueSession = context.Utilisateurs_ClientSessions.Where(ut => ut.SessionId == itemSession.Id).Where(ut => ut.Note != null ).FirstOrDefault();
                        if (HistoriqueSession != null)
                        {
                            var client = context.Utilisateurs.OfType<Client>().Where(c => c.Id == HistoriqueSession.Utilisateurs_ClientId).FirstOrDefault();

                            ClientNoteDto clientNoteDtoitem = new ClientNoteDto();

                            clientNoteDtoitem.IdHistoriqueSession = HistoriqueSession.Id;
                            clientNoteDtoitem.Nom = client.Nom;
                            clientNoteDtoitem.Prenom = client.Prenom;
                            clientNoteDtoitem.NamedSession = itemSession.Nom;
                            clientNoteDtoitem.IdSession = itemSession.Id;
                            clientNoteDtoitem.Note = "";
                            clientNoteDtoitem.Date = itemSession.HeureFin.ToString();

                            listClientDto.Add(clientNoteDtoitem);
                        }
                    }

                    result.Valeur = listClientDto;
                });
        }



        public Resultat<ClientNoteDto> GetHistoriqueSession(Employe employe, int id)
        {
            return Resultat<ClientNoteDto>.SafeExecute<EmployeService>(
               result =>
               {
                   

                   ClientNoteDto listClientDto = new ClientNoteDto();


                       var HistoriqueSession = context.Utilisateurs_ClientSessions.Where(ut => ut.Id == id).FirstOrDefault();
                       var session = context.Sessions.Where(s => s.Id == HistoriqueSession.SessionId).FirstOrDefault();
                       var client = context.Utilisateurs.OfType<Client>().Where(c => c.Id == HistoriqueSession.Utilisateurs_ClientId).FirstOrDefault();

                       ClientNoteDto clientNoteDtoitem = new ClientNoteDto();

                       clientNoteDtoitem.IdHistoriqueSession = HistoriqueSession.Id;
                       clientNoteDtoitem.Nom = client.Nom;
                       clientNoteDtoitem.Prenom = client.Prenom;
                       clientNoteDtoitem.NamedSession = session.Nom;
                       clientNoteDtoitem.IdSession = session.Id;
                       clientNoteDtoitem.Note = "";
                       clientNoteDtoitem.Date = session.HeureFin.ToString();

                       result.Valeur = clientNoteDtoitem;

               });
        }


        public Resultat<bool> AddNoteToClientHistoricalSession(CreateClientNoteCommand command)
        {
            return Resultat<bool>.SafeExecute<EmployeService>(
                result =>
                {
                    var historicalSession = context.Utilisateurs_ClientSessions.Where(hs => hs.Id == command.IdHistoriqueSession).FirstOrDefault();
                    historicalSession.Note = (float)Convert.ToDouble(command.Note);

                    context.SaveChanges();

                    result.Valeur = true;

                });
        }
    }
}
