using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Rokhsare.Control.Base
{
    public class SMTPServer
    {
        public string ServerUrl { get; set; }
        public int ServerPort { get; set; }
        public bool isSSL { get; set; }
    }
    public class EmailAccount
    {
        string _replyTo = string.Empty;
        public string ReplyTo
        {
            get
            {
                if (!string.IsNullOrEmpty(_replyTo))
                    return _replyTo;
                return Email;
            }
            set
            {
                _replyTo = value;
            }
        }
        public string Email { get; set; }
        public string Passwrod { get; set; }
    }
    public class SendMail
    {
        public static bool SendSystemMailByGeneralSMTP(SMTPServer smtpServer, EmailAccount accountInfo, string pTo, string pSubject, string pBody, bool isHtml = true, string pAttachmentPath = "", bool async = false)
        {
            return SendbySMTP(smtpServer, accountInfo, pTo, pSubject, pBody, isHtml, pAttachmentPath, System.Text.Encoding.UTF8, false, async);
        }

        public static bool SendbySMTP(SMTPServer smtpServer, EmailAccount accountInfo,
          string pTo, string pSubject, string pBody, bool isHtml,
          string pAttachmentPath, System.Text.Encoding encoding,
            bool isSSL = false, bool async = false
            )
        {
            if (string.IsNullOrEmpty(pTo) || string.IsNullOrWhiteSpace(pTo))
                return false;
            var ea = pTo.Split(';').Where(u => u.Length > 5).ToList();
            MailMessage myMail = new MailMessage(accountInfo.Email, ea[0], pSubject, pBody);
            for (int i = 1; i < ea.Count; i++)
                myMail.To.Add(ea[i]);

            myMail.BodyEncoding = encoding;// System.Text.Encoding.UTF8;
            myMail.IsBodyHtml = isHtml;
            myMail.ReplyToList.Add(new MailAddress(accountInfo.ReplyTo));
            if (pAttachmentPath.Trim() != "")
            {
                //myMail.Attachments.Add(pAttachmentPath);
            }
            System.Net.Mail.SmtpClient sc = new SmtpClient(smtpServer.ServerUrl, smtpServer.ServerPort);
            sc.Timeout = 20000;
            sc.Credentials = new System.Net.NetworkCredential(accountInfo.Email, accountInfo.Passwrod);
            sc.EnableSsl = isSSL;
            try
            {
                if (async)
                {
                    object userState = myMail;
                    sc.SendAsync(myMail, userState);
                }
                else
                    sc.Send(myMail);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
    public class SendMailViaNoreplyAtIrTextBook
    {
        public static bool Send(string pTo, string pSubject, string pBody)
        {
            var smtp = new SMTPServer() { isSSL = false, ServerPort = 25, ServerUrl = "188.75.91.140" };
            var emailAcc = new EmailAccount() { Email = "noreply@irtextbook.com", Passwrod = "nikimoshi610" };
            return SendMail.SendSystemMailByGeneralSMTP(smtp, emailAcc, pTo, pSubject, pBody);
        }
    }
}
