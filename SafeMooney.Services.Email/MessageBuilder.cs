﻿using NVelocity;
using NVelocity.App;
using SafeMooney.Services.Email.Models;
using System;
using System.IO;
using System.Linq;
using System.Runtime.Caching;

namespace SafeMooney.Services.Email
{
    public class MessageBuilder
    {
        private MessageBuilderContext _context;

        public MessageBuilder(MessageBuilderContext context)
        {
            if (context == null || context.TemplateFile == null)
                throw new ArgumentNullException("email context has incorrect value");

            _context = context;
        }
        public String GetMessage()
        {
            String emailMessage = null;
            Velocity.Init();
            VelocityContext context = new VelocityContext();
            context.Put("context", new { SenderName = _context.SenderName, Refer = _context.Refer });

            using (StringWriter writer = new StringWriter())
            {
                try
                {
                    Velocity.Evaluate(context, writer, String.Empty, _context.TemplateFile);
                    emailMessage = writer.ToString();
                }
                catch(Exception e)
                {
                    writer.Close();
                    throw;
                }       
            }

            return emailMessage;
        }

        public static Object GetMessageTemplate(String name)
        {
            if (!MemoryCache.Default.Any(c => c.Key.Equals(name)))
                throw new FileNotFoundException("email template is not found");

            CacheItem citem = MemoryCache.Default.GetCacheItem(name);
            if (citem == null || citem.Value == null)
                throw new FileNotFoundException("email template is not found");

            return citem.Value;
        }

        public static void SetMessageTemplate(String name, Object value)
        {
            CacheItemPolicy policy = new CacheItemPolicy() { Priority = CacheItemPriority.NotRemovable };
            MemoryCache.Default.Set(new CacheItem(name, value), policy);
        }
    }
}
