using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security;

namespace SportsResultNotifier
{
    public class MailSender
    {
        public string SmtpAddress {  get; set; }
        public int PortNumber {  get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public string SenderAddress { get; set; }

        public string[] ReceiverAddresses { get; set; }

        public SecureString Password {  get; set; }

        public bool EnableSSL { get; set; }

        public MailSender(string smtpAddress, int portNumber, string subject, string body, string senderAddress, string[] receiverAddresses, SecureString password, bool enableSSL = true)
        {
            SmtpAddress = smtpAddress;
            PortNumber = portNumber;
            Subject = subject;
            Body = body;
            SenderAddress = senderAddress;
            ReceiverAddresses = receiverAddresses;
            Password = password;
            EnableSSL = enableSSL;
        }

        public void SendEmail()
        {
            using(MailMessage mail = new MailMessage()) 
            {
                mail.From = new MailAddress(SenderAddress);
                foreach(var receiver in ReceiverAddresses)
                {
                    mail.To.Add(receiver);
                }
                mail.Subject = Subject;
                mail.Body = Body;
                mail.IsBodyHtml = true;

               // mail.Attachments.Add(new Attachment("D:\\TestFile.txt"))

                using(SmtpClient smtp = new SmtpClient(SmtpAddress, PortNumber))
                {
                    smtp.Credentials = new NetworkCredential(SenderAddress, Password);
                    smtp.EnableSsl = EnableSSL;
                    try
                    {
                        smtp.Send(mail);
                        Console.WriteLine("Email sent successfully.");

                    }catch(Exception ex)
                    {
                        Console.WriteLine($"Error sending email: {ex.Message}");
                    }


                }

            }
        }
       
    }
}
