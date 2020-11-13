using FluentEmail.Core;
using FluentEmail.Core.Models;
using FluentEmail.Mailgun;
using FluentEmail.Mailgun.HttpHelpers;
using FluentEmail.Razor;
using MailGunIntegration.Override;
using OnlineAuction.Core.Extensions;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;

namespace MailGunIntegration
{
    public interface IMailGunIntegration
    {
        ApiResponse<MailgunResponse> SendMail(string apiDomain, string apiKey, string fromEmail, string fromName,
            string toEmail, string subject, string body, bool isHtml,
            string customKey = null, List<Attachment> attachmentList = null);
        IRestResponse SendGeneralMail(string apiDomain, string apiKey, string title, string toName, string toSurname, string toEmail, Dictionary<string, string> parameters);
    }

    public class MailGunIntegration : IMailGunIntegration
    {
        public ApiResponse<MailgunResponse> SendMail(string apiDomain, string apiKey, string fromEmail,
            string fromName, string toEmail, string subject, string body,
            bool isHtml, string customKey = null, List<Attachment> attachmentList = null)
        {
            try
            {
                var sender = new OverrideMailGunSender(apiDomain, apiKey);

                Email.DefaultRenderer = new RazorRenderer();
                var email = Email.From(fromEmail, fromName)
                .To(toEmail)
                .Subject(subject)
                .Body(body, isHtml);

                if (customKey != null)
                    email.Tag(customKey);

                if (attachmentList != null)
                    email.Attach(attachmentList);

                var response = sender.Send(email);

                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IRestResponse SendGeneralMail(string apiDomain, string apiKey, string title, string toName, string toSurname, string toEmail, Dictionary<string, string> parameters)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.eu.mailgun.net/v3/");
            client.Authenticator = new HttpBasicAuthenticator("api", apiKey);

            RestRequest request = new RestRequest();

            request.AddParameter("domain", apiDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";

            request.AddParameter("from", $"Online Auction <sistem@postaci.netkasam.com>");
            request.AddParameter("to", $"{toName} {toSurname} <{toEmail}>");
            request.AddParameter("subject", title);
            request.AddParameter("template", "info");
            request.AddParameter("h:X-Mailgun-Variables", StringExtensions.DictionaryToJsonString(parameters));

            request.Method = Method.POST;

            return client.Execute(request);
        }
    }
}
