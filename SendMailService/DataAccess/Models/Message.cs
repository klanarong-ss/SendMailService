﻿using MailKit.Net.Smtp;
using MimeKit;

namespace EmailService.SendMailService.DataAccess.Models {
    public class Message
    {
        //public List<MailboxAddress> To { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public IFormFileCollection Attachments { get; set; }

    }
}