﻿using RealEstate.Application.EmailService.Entities;

namespace RealEstate.Application.EmailService
{
    public interface IGmailClient
    {
        Task SendMail(string reciever, string subject, string body, List<string> attachments = null);
        Task SendMail(MailRecipientDTO Receiver, string subject, string body, List<string> attachments = null);
        Task SendMail(List<MailRecipientDTO> Receivers, string subject, string body, List<string> attachments = null);
    }
}
