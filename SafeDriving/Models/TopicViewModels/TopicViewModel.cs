using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SafeDriving.Models.MessageViewModels;
using System.Drawing;

namespace SafeDriving.Models.TopicViewModels
{
    public class TopicViewModel
    {
        public int Id { get; set; }

        public string Nom { get; set; }

        public string DateCreation { get; set; }

        public string Contenu { get; set; }

        public string NomCreateur { get; set; }

        public int IdCreateur { get; set; }

        public IList<MessageViewModel> Messages { get; set; }

        public string MessageNewTopic { get; set; }

    }
}