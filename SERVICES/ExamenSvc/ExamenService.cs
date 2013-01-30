using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POCO;

namespace SERVICES.ExamenSvc
{
    public class ExamenService : IExamenService
    {
        public SafeDrivingEntities context { get; set; }

        public ExamenService()
        {
            context = SafeDrivingEntities.contexteDatas;
        }

        private Client GetClientById(int clientId)
        {
            var client = (from c in context.Utilisateurs.OfType<Client>() where c.Id == clientId select c).First();
            return client;
        }

        public IList<Session> GetSessionByTypeSessionId(int typeSessionId)
        {
            var sessions = (from s in context.Sessions where s.TypeSessionId == typeSessionId select s).ToList();
            return sessions;
        }

        //On récupère la liste des historique pour une session donnée
        public IList<Utilisateurs_ClientSessions> GetUtilisateurs_ClientSessionsBySessionId(IList<Session> listeSession)
        {
            var listeClientSession = new List<Utilisateurs_ClientSessions>();
            foreach (var session in listeSession)
            {
                var clientsessions = (from c in context.Utilisateurs_ClientSessions where c.SessionId == session.Id select c).ToList();
                foreach (var item in clientsessions)
                {
                    listeClientSession.Add(item);
                }
            }

            return listeClientSession;
        }

        //On récupère la liste des clients via une liste d'historique, on retourne une liste de clientDto
        //Ces clientsDto possède une liste de leur notes
        public IList<ClientDto> GetClientByHistorique(IList<Utilisateurs_ClientSessions> listeClientSession)
        {
            var listeClientDto = new List<ClientDto>();

            // Nous sert de référence pour la comparaison
            var listeClient = new List<Client>();

            //On parcours l'historique
            foreach (var historique in listeClientSession)
            {
                //On récupère le client de la ligne de l'historique
                var client = GetClientById(historique.Utilisateurs_ClientId);

                //Si il est dans la liste de client référence
                if (listeClient.Contains(client))
                {
                    //Alors on parcours la liste de client Dto
                    foreach (var clientDto in listeClientDto)
                    {
                        //Si la liste de ClientDto contient un clientDto possédant l'id du client en cours
                        if (clientDto.Id == client.Id)
                        {
                            //Alors on peut lui ajouter cette note, uniquement si elle a une valeur
                            if(historique.Note.HasValue)
                                clientDto.ListeNote.Add(historique.Note.Value);
                        }
                    }
                }
                //Sinon, on ajoute le client a la liste de client, on crée un clientDto et on l'ajoute a la liste de ClientDto
                else
                {
                    listeClient.Add(client);
                    var clientDto = new ClientDto { Adresse = client.Adresse, CodePostal = client.CodePostal, DateNaissance = client.DateNaissance, Id = client.Id, Ville=client.Ville, Telephone=client.Telephone, Prenom=client.Prenom, Nom=client.Nom, Mail=client.Mail, ListeNote= new List<float>() };
                    if (historique.Note.HasValue)
                        clientDto.ListeNote.Add(historique.Note.Value);
                    listeClientDto.Add(clientDto);
                }
            }

            return listeClientDto;
        }

        public IList<ClientDto> GetMoyenneForClientDto(IList<ClientDto> listeClientDto)
        {
            foreach (var clientDto in listeClientDto)
            {
                //Calcul de la moyenne
                clientDto.NoteMoyenne = clientDto.ListeNote.Sum() / clientDto.ListeNote.Count();
            }

            return listeClientDto;
        }

    }
}
