using System.Net.Mail;

public interface IEmailQueue
{
    Task EnqueueEmailAsync(string recipientEmail);
    Task<string?> DequeueEmailAsync();
    Task<IEnumerable<string>> GetQueue();
}
