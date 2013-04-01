using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mime;
using System.Net.Mail;

namespace ZRZLib.Net
{
    public class MailSender
    {
        String account;
        String password;
        String server;
        Int32 port;
        String nameDisplay;

        /*    String[] correos;
            String subject;
            String mensaje;*/

        public MailSender(String _account, String _password, String _server, Int32 _port, String _nameDisplay)
        {
            account = _account;
            password = _password;
            server = _server;
            port = _port;
            nameDisplay = _nameDisplay;
        }

        public MailSender(String _account, String _password, String _server, Int32 _port)
        {
            account = _account;
            password = _password;
            server = _server;
            port = _port;
            nameDisplay = _account;
        }
        public bool enviarMail(String[] To, String subject, String message)
        {
            return enviarMail(To, null, subject, message, null);
        }

        public bool enviarMail(String[] To, String subject, String message, String[] attachments)
        {
            return enviarMail(To, null, subject, message, attachments);
        }
        public bool enviarMail(String[] To, String[] cc, String subject, String message)
        {
            return enviarMail(To, cc, subject, message, null);
        }

        public bool enviarMail(String[] To, String[] cc, String subject, String message, String[] attachments)
        {
            StringBuilder sbBody = new StringBuilder();
            try
            {
                MailMessage mailer = new MailMessage();
                if (To != null)
                {
                    if (To.Length > 0)
                    {
                        foreach (String to in To)
                        {
                            mailer.To.Add(to);
                        }
                    }
                    else
                    {
                        throw new Exception("There are not address");
                    }
                }

                else
                {
                    throw new Exception("There are not address");
                }
                if (cc != null)
                {
                    if (cc.Length > 0)
                    {
                        foreach (String to in cc)
                        {
                            mailer.CC.Add(to);
                        }
                    }
                    else
                    {
                        throw new Exception("There are not address for CC");
                    }
                }
                if (attachments != null)
                {
                    foreach (string file in attachments)
                    {
                        Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                        // Add time stamp information for the file.
                        ContentDisposition disposition = data.ContentDisposition;
                        disposition.CreationDate = System.IO.File.GetCreationTime(file);
                        disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                        disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                        // Add the file attachment to this e-mail message.
                        mailer.Attachments.Add(data);
                    }
                }
                //message.To.Add(email);
                // message.CC.Add(Cc);
                //message.Bcc.Add("eliasvm@asdsad.com");
                //sbBody.Append(mensaje);
                mailer.From = new System.Net.Mail.MailAddress(account, account);
                mailer.Subject = subject;
                mailer.Body = message;
                mailer.IsBodyHtml = true;

                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
                smtp.Host = server;
                smtp.Port = port;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential(account, password);
                smtp.Send(mailer);
                return true;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error al intentar enviar email", ex);
            }
        }


    }
}
