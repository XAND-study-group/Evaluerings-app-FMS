using System.Net.Sockets;
using System.Text;
using MailKit.Net.Smtp;
using MimeKit;
using Module.Feedback.Application.Services;
using SharedKernel.Dto.Features.School.User.Query;
using SharedKernel.Enums.Features.Evaluering.Feedback;

namespace Module.Feedback.Infrastructure.Proxy;

public class EmailNotificationProxy : IEmailNotificationProxy
{
    async Task IEmailNotificationProxy.SendNotificationAsync(IEnumerable<GetEmailsByUserIdsResponse> emailsTo,
        string emailFrom,
        Domain.Entities.Feedback feedback)
    {
        using var client = new SmtpClient();
        client.ServerCertificateValidationCallback = (s, c, h, e) => true; // Accept any certificate
        
        await client.ConnectAsync("fakemailserver", 1025, false);  // Use container name and port
        
        foreach (var emailTo in emailsTo) await WriteMailContent(client, feedback, emailFrom, emailTo.Value);
        
        await client.DisconnectAsync(true);
        
        feedback.ChangeNotificationStatus(NotificationStatus.Sent);
    }

    private async Task WriteMailContent(SmtpClient client, Domain.Entities.Feedback feedback,
        string emailFrom, string emailTo)
    {
        var emailBody = $"<body>\r\n" +
                        $"En Evalureing i forummet <b>{feedback.Room.Title}</b> har haft forhøjet aktivitet.<br>\r\n" +
                        $"Der er tale om følgende Evaluering:<br>\r\n" +
                        $"<b>Title:</b>{feedback.Title}<br>\r\n" +
                        $"<b>Problem:</b> {feedback.Problem}<br>\r\n" +
                        $"<b>Løsning:</b> {feedback.Solution}<br>\r\n" +
                        $"<br>\r\n" +
                        $"Evalueringen har haft: <b>{feedback.GetUpVoteCount()}</b> Up Votes, <b>{feedback.GetDownVoteCount()}</b> Down Votes, <b>{feedback.GetCommentsCount()}</b> Comments\r\n" +
                        $"</body>\r\n";
        
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(emailFrom));
        message.To.Add(MailboxAddress.Parse(emailTo));
        message.Subject = "Forums Aktivitet på Evaluering";
        message.Body = new TextPart("html") { Text = emailBody };
        
        await client.SendAsync(message);
    }
}