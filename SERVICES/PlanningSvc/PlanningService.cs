using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;
using AutoMapper;
using System.Collections.ObjectModel;
using SERVICES.RoleSvc;

namespace SERVICES.PlanningSvc
{
    public class PlanningService : IPlanningService
    {

        public SafeDrivingEntities context { get; set; }


        public PlanningService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }


        public Resultat<IList<OffresReference>> GetIListOffreByClient(POCO.Client client)
        {

            return Resultat<IList<OffresReference>>.SafeExecute<PlanningService>(
        result =>
       {

           IList<OffresReference> resultatOffresReferenceRequete = new List<OffresReference>();

           // On recupere tout les packs
           var OffersForCurrentUser = context.Offres.Where(o => o.ClientId == client.Id).ToList();


           foreach(Offre offre in OffersForCurrentUser)
           {
               var OffresReferences = context.OffresReferences.Where(or => or.Id == offre.OffresReferencesId).First();
               resultatOffresReferenceRequete.Add(OffresReferences);
           }
           

           result.Valeur = resultatOffresReferenceRequete;

       });

        }


        public Resultat<IList<TypeSession>> GetListTypeSessionByOffre(POCO.Client client, int offreId)
        {
            return Resultat<IList<TypeSession>>.SafeExecute<PlanningService>(
                       result =>
                       {

                           var currentClient = context.Utilisateurs.OfType<Client>().Where(u => u.Id == client.Id).First();

                           List<int> typeSession = new List<int>();

                           if ((bool)client.IsCoded)
                           {
                               typeSession = context.OffresReferencesTypeSessions
                              .Where(orts => orts.OffresReferencesId == offreId).Where(orts => orts.TypeSessionId != 5)
                              .Select(orts => orts.TypeSessionId).ToList();
                           }
                           else
                           {
                               typeSession = context.OffresReferencesTypeSessions
                               .Where(orts => orts.OffresReferencesId == offreId)
                               .Select(orts => orts.TypeSessionId).ToList();

                           }

                           IList<TypeSession> TypeSessionByOffre = new List<TypeSession>();


                           foreach (int TypeSession in typeSession)
                           {
                               
                               var typeSessionFinalObject = context.TypeSessions
                                   .Where(Ts => Ts.Id == TypeSession).FirstOrDefault();
                               TypeSessionByOffre.Add(typeSessionFinalObject);
                           }

                           result.Valeur = TypeSessionByOffre;
                       });

        }


        public Resultat<IDictionary<Offre, IList<TypeSession>>> GetAllTypeSessionByOffreList(IList<Offre> OffreList)
        {
            return Resultat<IDictionary<Offre, IList<TypeSession>>>.SafeExecute<PlanningService>(
                result =>
                {

                    IDictionary<Offre, IList<TypeSession>> OffreWithTypeSession = new Dictionary<Offre, IList<TypeSession>>();

                    foreach (Offre offre in OffreList)
                    {

                        var typeSessionquery = context.OffresReferencesTypeSessions
                            .Where(orts => orts.OffresReferencesId == offre.OffresReferencesId)
                            .Select(orts => orts.TypeSessionId).ToList();

                        IList<TypeSession> TypeSessionByOffre = new List<TypeSession>();


                        foreach (int TypeSession in typeSessionquery)
                        {
                            var typeSessionFinalObject = context.TypeSessions
                                .Where(Ts => Ts.Id == TypeSession).FirstOrDefault();
                            TypeSessionByOffre.Add(typeSessionFinalObject);
                        }

                        OffreWithTypeSession.Add(offre, TypeSessionByOffre);
                    }

                    result.Valeur = OffreWithTypeSession;

                });
        }


        public Resultat<IList<Session>> GetListFreesessionByTypeSessionAndDate(int typeSessionId, DateTime date)
        {
            return Resultat<IList<Session>>.SafeExecute<PlanningService>(
                     result =>
                     {

                         var EtatSession = context.EtatSessions.Where(Es => Es.Id == 4).FirstOrDefault();
                         var currentTypeSession = context.TypeSessions.Where(t => t.Id == typeSessionId).FirstOrDefault();

                         context.LoadProperty(currentTypeSession, ts => ts.Sessions);


                         //Comment Wherer sur les dates ??? .ToString("format") impossible
                         var queryAllListSession = context.Sessions
                             .Where(s => s.TypeSession.Id == currentTypeSession.Id)
                             .Where(s => s.EtatSessionsId == EtatSession.Id)
                             .ToList();


                         IList<Session> retourSessions = new List<Session>();

                         foreach (Session session in queryAllListSession)
                         {
                             if (session.HeureFin.Value.ToShortDateString() == date.ToShortDateString())
                             {
                                 retourSessions.Add(session);
                             }

                         }

                         result.Valeur = retourSessions;
                     });

        }


        public Resultat<bool> CreatePlanningInscription(CreatePlanningCommand command)
        {
            return Resultat<bool>.SafeExecute<PlanningService>(
                result =>
                {


                    var session = context.Sessions.Where(s => s.Id == command.SelectedSessionId).FirstOrDefault();

                    session.EtatSessionsId = 1;

                    var historiqueValidee = new Utilisateurs_ClientSessions();
                    historiqueValidee.Utilisateurs_ClientId = command.ClientId;
                    historiqueValidee.OffreId = command.SelectedOffreId;
                    historiqueValidee.SessionId = command.SelectedSessionId;

                    context.Utilisateurs_ClientSessions.AddObject(historiqueValidee);

                    context.SaveChanges();

                });
        }


        // Pour la direction

        public Resultat<Dictionary<int, string>> GetAllFormateur(Privilege role)
        {
            return Resultat<Dictionary<int, string>>.SafeExecute<PlanningService>(
                result =>
                {
                    int roleId = (int)role;
                    Dictionary<int, string> dicoFormateurs = new Dictionary<int, string>();

                    var formateurs = (from c in context.Utilisateurs.OfType<Employe>() where c.RoleId == roleId select c).ToList();

                    foreach (var c in formateurs)
                    {
                        dicoFormateurs.Add(c.Id, c.Nom);
                    }

                    result.Valeur = dicoFormateurs;
                });
        }


        public Resultat<IList<Session>> GetSessionFreeByFormateur(int formateurId)
        {
            return Resultat<IList<Session>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        public Resultat<IList<TypeSession>> GetAllTypeSession()
        {
            return Resultat<IList<TypeSession>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        //Pour afficher les véhicule disponibles
        public Resultat<IList<Vehicule>> GetVehiculeByDate(DateTime dateDebutSession)
        {
            return Resultat<IList<Vehicule>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        //Pour vérifier la disponibilité coré server
        public Resultat<IList<Vehicule>> GetVehiculeByDate(DateTime dateDebutSession, DateTime dateFinSession)
        {
            return Resultat<IList<Vehicule>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        ///Pour afficher les véhicules disponibles
        public Resultat<IList<Circuit>> GetCircuitByDate(DateTime dateDebutSession)
        {
            return Resultat<IList<Circuit>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        //Pour vérifier la diponibilté coté server
        public Resultat<IList<Circuit>> GetCircuitByDate(DateTime dateDebutSession, DateTime dateFinSession)
        {
            return Resultat<IList<Circuit>>.SafeExecute<PlanningService>(
                result =>
                {

                });
        }

        public Resultat<Session> CreateSession()
        {
            return Resultat<Session>.SafeExecute<PlanningService>(
                result =>
                { 

                });
        }

    }


}
