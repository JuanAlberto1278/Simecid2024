using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.Communication.Email;

namespace CoreApp
{
    public class EmailManager
    {
        public async Task<string> SendEmail(string emailAddress, string otp) 
        {
            string connectionString = "endpoint=https://emailotpcommunication.unitedstates.communication.azure.com/;accesskey=fUSkmhkbbVbswwMw55/GkM6SV4KmWLND0FFG1bQ9m7rMe2gTUi3OSyO8DNLbr40Tjid0RqLTr5dBBeevwwlKGA==";

            EmailClient emailClient = new EmailClient(connectionString);
            EmailContent emailContent = new EmailContent("OTP Verification"); //Subject
            emailContent.PlainText = "\nHere is your verification code:" + otp;



            List<EmailAddress> emailAddresses = new List<EmailAddress> { new EmailAddress(emailAddress, "Suscriptor de ISA-CLINIC") };
            EmailRecipients emailRecipients = new EmailRecipients(emailAddresses);


            EmailMessage emailMessage = new EmailMessage("DoNotReply@66e6180a-61c2-48bc-a646-7eb37607e7d8.azurecomm.net", emailRecipients, emailContent);

            EmailSendOperation emailSendOperation = await emailClient.SendAsync(
                                                    WaitUntil.Completed,
                                                                emailMessage, CancellationToken.None);
            EmailSendResult statusMonitor = emailSendOperation.Value;

            Console.WriteLine($"Email Sent. Status = {emailSendOperation.Value.Status}");

            return emailSendOperation.Value.Status.ToString();
        }
    }
}
