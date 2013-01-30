using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ParcoursSvc
{
    public class ParcoursService : IParcoursService
    {

        public SafeDrivingEntities context { get; set; }

        public ParcoursService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }


        public Resultat<ParcoursDto> GetParcoursSessionByClient(POCO.Client client)
        {
            return Resultat<ParcoursDto>.SafeExecute<ParcoursService>(
                  result =>
                  {
                      ParcoursDto currentParcoursDto = new ParcoursDto();

                      currentParcoursDto.GraphDictionnary = new Dictionary<OffreReferenceDto, IList<SessionHistoriqueDto>>();

                      context.LoadProperty(client, c => c.Offres);
                      var offres = context.Utilisateurs.OfType<Client>()
                          .Where(c => c.Id == client.Id)
                          .Select(c => c.Offres).First();

                      foreach (Offre offre in offres)
                      {
                          var offreReferences = context.OffresReferences.Where(or => or.Id == offre.OffresReferencesId).FirstOrDefault();
                          var offreSession = context.Utilisateurs_ClientSessions.Where(o => o.OffreId == offre.OffresReferencesId).Where(o => o.Utilisateurs_ClientId == client.Id).Where(o => o.Note != null).ToList<Utilisateurs_ClientSessions>();

                          IList<SessionHistoriqueDto> currentListHistoricalSessionDto = new List<SessionHistoriqueDto>();

                          foreach (Utilisateurs_ClientSessions currentUtilisateurSession in offreSession)
                          {

                              var ReelSession = context.Sessions.Where(s => s.Id == currentUtilisateurSession.SessionId).FirstOrDefault();
                              if (ReelSession.HeureFin.HasValue)
                              {
                                  currentListHistoricalSessionDto.Add(new SessionHistoriqueDto() { Date = (DateTime)ReelSession.HeureFin, Note = Convert.ToDouble(currentUtilisateurSession.Note) });
                              }
 
                          }

                          currentParcoursDto.GraphDictionnary.Add(new OffreReferenceDto() { Name = offreReferences.Nom }, currentListHistoricalSessionDto);
                      }
                      result.Valeur = currentParcoursDto;
                  });
        }
    }
}
