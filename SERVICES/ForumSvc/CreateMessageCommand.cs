using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SERVICES.ForumSvc
{
    public class CreateMessageCommand
    {
        public int TopicId { get; set; }

        public int CreateurId { get; set; }

        public DateTime DateCreation { get; set; }

        public string Contenu { get; set; }
    }
}
