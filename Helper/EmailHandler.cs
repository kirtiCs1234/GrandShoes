using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace Helper
{
    public class EmailHandler
    {
        
        /// <summary>
        /// send emails function
        /// </summary>
        /// <param name="recipients"></param>
        /// <param name="copy_recipients"></param>
        /// <param name="blind_copy_recipients"></param>
        /// <param name="Subject"></param>
        /// <param name="body"></param>
        /// <param name="file_attachments"></param>
        /// <returns>it will return 1 if the mail will send otherwise it will return 0</returns>
        public static bool SendMail(string Recipients, string RecipientsCC, string RecipientsBCC
                 , string Subject, string Body, string Attachments, HttpPostedFileBase[] uploadedFile)
        {
            try
            {
                string MailServerIP = ConfigurationManager.AppSettings["MailServerIP"].ToString();
                int Port = Convert.ToInt32(ConfigurationManager.AppSettings["OUT_PORT"]);
                string system_email_sender_email = ConfigurationManager.AppSettings["sender_email"].ToString();
                string system_email_sender_password = ConfigurationManager.AppSettings["system_email_password"].ToString();

                MailMessage mailMessage = new MailMessage();                                          //object of MailMessage Class

                foreach (var recipient in Recipients.Split(','))                                      //to add multiple addresses
                {
                    if (recipient.IndexOf("=") > 0)
                        mailMessage.To.Add(new MailAddress(recipient.Split('=')[1], recipient.Split('=')[0]));
                    else
                        mailMessage.To.Add(new MailAddress(recipient));
                }

                if (!string.IsNullOrEmpty(RecipientsCC))                                            //to add multiple CC addresses
                {
                    foreach (var recipient in RecipientsCC.Split(','))
                    {
                        if (recipient.IndexOf("=") > 0)
                            mailMessage.CC.Add(new MailAddress(recipient.Split('=')[1], recipient.Split('=')[0]));
                        else
                            mailMessage.CC.Add(new MailAddress(recipient));
                    }
                }

                if (!string.IsNullOrEmpty(RecipientsBCC))                                             //to add multiple BCC addresses
                {
                    foreach (var recipient in RecipientsBCC.Split(','))
                    {
                        if (recipient.IndexOf("=") > 0)
                            mailMessage.Bcc.Add(new MailAddress(recipient.Split('=')[1], recipient.Split('=')[0]));
                        else
                            mailMessage.Bcc.Add(new MailAddress(recipient));
                    }
                }

                if (system_email_sender_email.IndexOf("=") > 0)
                    mailMessage.From = new MailAddress(system_email_sender_email.Split('=')[1], system_email_sender_email.Split('=')[0]);
                else
                    mailMessage.From = new MailAddress(system_email_sender_email);

             


                mailMessage.Subject = Subject;
                mailMessage.Body = Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                if (!string.IsNullOrEmpty(Attachments))                                         //check if attachment file is not null
                {
                    var items = Attachments.Split(',');
                    foreach(var item in items)
                    {
                          mailMessage.Attachments.Add(new Attachment(item));
                    }
                }
                if (uploadedFile!=null)
                {
                    foreach(var item in uploadedFile)
                    {
                        mailMessage.Attachments.Add(new Attachment(item.InputStream, item.FileName));

                    }
                }

                SmtpClient smtpClient = new SmtpClient(MailServerIP, Port)    //object of SmtpClient
                {
                    EnableSsl = true,
                    DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(system_email_sender_email, system_email_sender_password)    //pass login credentials

                };
                smtpClient.Send(mailMessage);
                return true;
                //return "1";
            }
            catch (Exception ex)
            {
                File.WriteAllText(HttpContext.Current.Server.MapPath("~/error.txt"), ex.Message + "---"+ex.StackTrace);
                //return "1
                return false;
            }
        }
    }
}
