using System;
using System.IO;

namespace SafeMooney.Services.Email.Models
{
    public class MessageBuilderContext
    {
        public String SenderName { get; set; }
        public StreamReader TemplateFile { get; set; }
        public String Refer { get; set; }

    }
}
