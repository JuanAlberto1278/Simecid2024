using Azure.Communication.Email;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp
{
    public class ApptAlertManager
    {
        public async Task<string> SendAppointmentAlert(string emailAddress)
        {
            string connectionString = "endpoint=https://emailotpcommunication.unitedstates.communication.azure.com/;accesskey=fUSkmhkbbVbswwMw55/GkM6SV4KmWLND0FFG1bQ9m7rMe2gTUi3OSyO8DNLbr40Tjid0RqLTr5dBBeevwwlKGA==";

            EmailClient emailClient = new EmailClient(connectionString);
            EmailContent emailContent = new EmailContent("SIMECID: Please Review Your Pending Appointments");
            emailContent.PlainText = "Dear user, This is a friendly reminder to review your pending appointments and ensure everything is in order.\n" +
            "Your schedule is important to us, and we want to ensure that you are prepared for any upcoming appointments you may have.\n" +
            "Please take a moment to log in to your account or check your calendar to confirm the details of your appointments." +
            "Thank you for your attention to this matter.\n" +
            "Best regards,\n" +
            "Your SIMECID Team";




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
