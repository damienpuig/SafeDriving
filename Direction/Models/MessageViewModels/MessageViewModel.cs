using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;

namespace Direction.Models.MessageViewModels
{
    public class MessageViewModel
    {
        public int Id { get; set; }

        public string Contenu { get; set; }

        public string DateCreation { get; set; }

        public string NomCreateur { get; set; }

        public int IdCreateur { get; set; }

    }
}