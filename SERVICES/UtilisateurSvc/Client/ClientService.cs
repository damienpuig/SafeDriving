using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using AutoMapper;
using SERVICES.RoleSvc;
using System.Web.Security;
using System.Web;

namespace SERVICES.UtilisateurSvc
{
   public class ClientService : IUtilisateurService<Client>, IClientService
    {
       public SafeDrivingEntities context { get; set; }
       public IRoleService RoleService { get; set; }

       public ClientService(IRoleService roleService)
       {
           RoleService = roleService;
           context = SafeDrivingEntities.contexteDatas;
       }


       public Resultat<Client> CreateClient(CreateClientCommand createClientCommande)
       {
           return Resultat<Client>.SafeExecute<ClientService>(
              result =>
              {
                      //On encode le password 
                      createClientCommande.Password = EncodePassword(createClientCommande.Password);
                        
                      //On mappe la commande en objet Client
                      var client = Mapper.Map<CreateClientCommand, Client>(createClientCommande);
                        
                      client.Avatar = createClientCommande.Avatar;
                        
                      //On récupère le Role passé par le controller via la commande
                      var roleResult = RoleService.GetRoleByPrivilege(createClientCommande.Role);
                        
                      if(roleResult.IsValid)
                      {
                          client.Role = roleResult.Valeur;
                          client.RoleId = roleResult.Valeur.Id;
                          client.IsCoded = createClientCommande.IsCoded;
                          context.Utilisateurs.AddObject(client);
                          context.SaveChanges();
                          result.Valeur = client;
                      }
              });
       }

       Resultat<Client> IClientService.UpdateEntity(UpdateClientCommand updateClientCommand)
       {
            return Resultat<Client>.SafeExecute<ClientService>(
            result =>
            {
                //On mappe la commande en objet Client
                var client = Mapper.Map<UpdateClientCommand, Client>(updateClientCommand);

                //On récupère mongo; THIBAULT ON JOUE A LA SALE ?
                var pseudo = GetUtilisateurCourantPseudo();
                if (pseudo.IsValid)
                {
                    var savedClient = GetEntityByPseudo(pseudo.Valeur);

                    // UPDATE DEGUELASSE
                    savedClient.Valeur.Adresse = client.Adresse;
                    savedClient.Valeur.CodePostal = client.CodePostal;
                    savedClient.Valeur.DateNaissance = client.DateNaissance;
                    savedClient.Valeur.Mail = client.Mail;
                    savedClient.Valeur.Nom = client.Nom;
                    savedClient.Valeur.Prenom = client.Prenom;
                    savedClient.Valeur.Pseudo = client.Pseudo;
                    savedClient.Valeur.Telephone = client.Telephone;
                    savedClient.Valeur.Ville = client.Ville;

                    //On a pas le temps de jouer ae automapper..
                    if (updateClientCommand.MotDePasse != "INCHANGE")
                    {
                        savedClient.Valeur.Password = client.Password;
                    }

                    context.SaveChanges();
                }
            });
       }

       //Méthode pour encoder le mot de passe du client
        private string EncodePassword(string password)
       {
           // calcule le sha1 du password puis le convertit en base64
           var passwordBytes = Encoding.UTF8.GetBytes(password);
           System.Security.Cryptography.SHA1 sha = new System.Security.Cryptography.SHA1CryptoServiceProvider();
           var sha1Password = sha.ComputeHash(passwordBytes);
           var base64Password = Convert.ToBase64String(sha1Password);
           return base64Password;
       }

        public Resultat<IList<Client>> GetEntities()
        {
            return Resultat<IList<Client>>.SafeExecute<ClientService>(
                result =>
                {
                    var clients = context.Utilisateurs.OfType<Client>().ToList();
                    result.Valeur = clients;
                });
        }

        public Resultat<Client> GetEntityById(int id)
        {
            return Resultat<Client>.SafeExecute<ClientService>(
               result =>
               {
                   var client = context.Utilisateurs.OfType<Client>().Where(u => u.Id == id).First();
                   result.Valeur = client;
               });
        }

        public Resultat<Client> GetEntityByPseudo(string pseudo)
        {
            return Resultat<Client>.SafeExecute<ClientService>(
               result =>
               {
                   var client = context.Utilisateurs.OfType<Client>().Where(u => u.Pseudo == pseudo).First();
                   result.Valeur = client;
               });
        }

        public Resultat<IList<Client>> GetEntitiesByRole(Privilege role)
        {
            int roleId = (int)role;

            return Resultat<IList<Client>>.SafeExecute<ClientService>(
               result =>
               {
                   var clients = (from c in context.Utilisateurs.OfType<Client>() where c.RoleId == roleId select c).ToList();
                   result.Valeur = clients;
               });
        }
        
        public Resultat<Client> RemoveEntityById(int id)
        {
            return Resultat<Client>.SafeExecute<ClientService>(
              result =>
              {
                  var client = context.Utilisateurs.OfType<Client>().Where(u => u.Id == id).First();
                  context.DeleteObject(client);
                  context.SaveChanges();
              });
        }

        public Resultat<Client> RemoveEntities()
        {
            return Resultat<Client>.SafeExecute<ClientService>(
            result =>
            {
                var clients = context.Utilisateurs.OfType<Client>().ToList();
            
                foreach (Client item in clients)
                {
                    context.DeleteObject(item);
                }
            
                context.SaveChanges();
            });
        }

        public Resultat<bool> IsPseudoUnique(string pseudo)
        {
            return Resultat<bool>.SafeExecute<ClientService>(
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

        public Resultat<IList<Client>> GetClientsByBirthDate(DateTime BirthDate)
        {
            return Resultat<IList<Client>>.SafeExecute<ClientService>(
             result =>
             {

             });
        }


        public Resultat<Client> ValidateEntity(string login, string password)
        {
            return Resultat<Client>.SafeExecute<ClientService>(
                result =>
                {
                    string CurrentPass = EncodePassword(password);
                    var client = context.Utilisateurs.OfType<Client>().Where(c => c.Pseudo == login).Where(c => c.Password == CurrentPass).First();
                    result.Valeur = client;
                });
        }


        public Resultat<Client> SignIn(Client entityToSignIn, bool isCookiePersistent)
        {
         return   Resultat<Client>.SafeExecute<ClientService>(
                result =>
                {
                    FormsAuthentication.SetAuthCookie(entityToSignIn.Pseudo, isCookiePersistent);
                });
        }



        public Resultat<string> GetUtilisateurCourantPseudo()
        {
            return Resultat<string>.SafeExecute<ClientService>(
                result =>
                {
                    string pseudo = HttpContext.Current.User.Identity.Name;
                    result.Valeur = pseudo;
                });

        }


        public Resultat<Privilege> GetRoleByEntityId(int id)
        {
            return Resultat<Privilege>.SafeExecute<ClientService>(
                result =>
                {

                    var roleId = context.Utilisateurs.OfType<Client>().Where(c => c.Id == id).FirstOrDefault().RoleId;
                    var role = context.Roles.Where(r => r.Id == roleId).FirstOrDefault();
                    var pr = (Privilege)role.Id;

                    result.Valeur = pr;
                });
        }

        public Resultat<bool> MigrateToCompteClient(Client clientToMigrate)
        {
            return Resultat<bool>.SafeExecute<ClientService>(
                result =>
                {
                    var roleResult = RoleService.GetRoleByPrivilege(Privilege.Client);
                    clientToMigrate.Role = roleResult.Valeur;
                    context.SaveChanges();

                });
        }
    }
}
