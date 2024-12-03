﻿using System.Net.Sockets;
using System.Text;
using Module.Feedback.Application.Services;
using SharedKernel.Enums.Features.Vote;

namespace Module.Feedback.Infrastructure.Proxy;

public class EmailNotificationProxy: IEmailNotificationProxy
{
    async Task IEmailNotificationProxy.SendNotificationAsync(IEnumerable<string> emailsTo, string emailFrom, Domain.Feedback feedback)
    {
        var client = new TcpClient("localhost", 2525);
        await using var stream = client.GetStream();
        using var reader = new StreamReader(stream, Encoding.ASCII);
        await using var writer = new StreamWriter(stream, Encoding.ASCII);
        
        writer.AutoFlush = true;
        Console.WriteLine(await reader.ReadLineAsync());

        var upVoteCount = feedback.Votes.Count(vote => vote.VoteScale == VoteScale.UpVote);
        var downVoteCount = feedback.Votes.Count(vote => vote.VoteScale == VoteScale.DownVote);
        var commentCount = feedback.Comments.Count + feedback.Comments.Select(c => c.SubComments).Count();

        foreach (var emailTo in emailsTo)
        {
            await WriteMailContent(writer, reader, feedback, emailFrom, emailTo, upVoteCount, downVoteCount, commentCount);
        }
        
        client.Close();
    }

    private async Task WriteMailContent(StreamWriter writer, StreamReader reader, Domain.Feedback feedback,
        string emailFrom, string emailTo, int upVoteCount, int downVoteCount, int commentCount)
    {
        await writer.WriteLineAsync("EHLO localhost");
        Console.WriteLine("1 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync($"MAIL FROM: <{emailFrom}>");
        Console.WriteLine("2 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync($"RCPT TO: <{emailTo}>");
        Console.WriteLine("3 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync("DATA");
        Console.WriteLine("4 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync("Subject: Forums Aktivitet på Evaluering");
        Console.WriteLine("5 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync($"From: {emailFrom}");
        Console.WriteLine("6 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync($"To: {emailTo}");
        Console.WriteLine("7 " + await reader.ReadLineAsync());
        await writer.WriteLineAsync("Content-Type: text/html; charset=UTF-8"); // Set content type
        await writer.WriteLineAsync("");
        await writer.WriteLineAsync("<body>"); 
        await writer.WriteLineAsync($"En Evalureing i forummet <b>{feedback.Room.Title}</b> har haft forhøjet aktivitet.<br>");
        await writer.WriteLineAsync("Der er tale om følgende Evaluering:<br>");
        await writer.WriteLineAsync($"<b>Title:</b>{feedback.Title}<br>");
        await writer.WriteLineAsync($"<b>Problem:</b> {feedback.Problem}<br>");
        await writer.WriteLineAsync($"<b>Løsning:</b> {feedback.Solution}<br>");
        await writer.WriteLineAsync("<br>");
        await writer.WriteLineAsync($"Evalueringen har haft: <b>{upVoteCount}</b> Up Votes, <b>{downVoteCount}</b> Down Votes, <b>{commentCount}</b> Comments");
        Console.WriteLine(await reader.ReadLineAsync());
        await writer.WriteLineAsync(".");
        await writer.WriteLineAsync("</body>");
        Console.WriteLine(await reader.ReadLineAsync());
        await writer.WriteLineAsync("QUIT");
        Console.WriteLine(await reader.ReadLineAsync());
    }
}