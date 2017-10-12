using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SocialServicesLibrary.Email.Models
{
    public class MessageBuilderContext
    {
        public String SenderName { get; set; }
        public StreamReader TemplateFile { get; set; }
        public String Refer { get; set; }

    }
}
