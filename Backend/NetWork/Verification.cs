using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Backend.NetWork
{
    public static class Verification
    {
        private static string Code;
        
        public static void send(string ReciverEmail)
        {
            try
            {
                Code = new Random().Next(10000, 100000).ToString();

                MailMessage mail = new MailMessage("restaurantmanagement.ma@gmail.com", ReciverEmail);
                mail.Subject = "Verification";
                mail.Body = "Code for verify : " + Code;

                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com", 587);
                smtpServer.Credentials = new NetworkCredential("restaurant.management.ap@gmail.com", "bfcn xyfe pggm udju") as ICredentialsByHost;
                smtpServer.EnableSsl = true;

                smtpServer.Send(mail);
                smtpServer.Dispose();
            }
            catch
            {
                throw new Exception("Your internet is not connected");
            }
        }

        public static bool verify(string InputCode)
            => Code == InputCode;
    }
}
