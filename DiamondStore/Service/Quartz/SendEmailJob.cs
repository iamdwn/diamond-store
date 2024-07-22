using System;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.Extensions.Logging;
using Quartz;
using Service.Services;

namespace Service.Services.Quartzs
{
    public class SendEmailJob : IJob
    {
        private readonly ILogger<SendEmailJob> _logger;
        private readonly IEmailService _emailService;
        private readonly IEmailQueue _emailQueue;

        public SendEmailJob(ILogger<SendEmailJob> logger, IEmailService emailService, IEmailQueue emailQueue)
        {
            _logger = logger;
            _emailService = emailService;
            _emailQueue = emailQueue;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var recipients = await _emailQueue.GetQueue();
                string baseUrl = "https://localhost:7283/Account/Active";
                if (recipients == null || !recipients.Any())
                {
                    _logger.LogWarning("No email addresses found in the queue.");
                    return;
                }

                while (recipients.Any())
                {
                    var recipient = await _emailQueue.DequeueEmailAsync();
                    string activationUrl = $"{baseUrl}?email={recipient}";
                    if (recipient != null)
                    {
                        var subject = "Active Link";
                        var body = $"https://localhost:7028/Account/Active?email={recipient}";
                        //var body = $@"<!DOCTYPE html>
                        //                <html>
                        //                <head>
                        //                    <title>Account Activation</title>
                        //                    <style>
                        //                        body {{
                        //                            font-family: Arial, sans-serif;
                        //                            line-height: 1.6;
                        //                            margin: 0;
                        //                            padding: 0;
                        //                            background-color: #f4f4f4;
                        //                        }}
                        //                        .container {{
                        //                            max-width: 600px;
                        //                            margin: 0 auto;
                        //                            padding: 20px;
                        //                            background-color: #ffffff;
                        //                            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                        //                        }}
                        //                        .btn {{
                        //                            display: inline-block;
                        //                            padding: 10px 20px;
                        //                            color: #ffffff;
                        //                            background-color: #007bff;
                        //                            text-decoration: none;
                        //                            border-radius: 5px;
                        //                        }}
                        //                    </style>
                        //                </head>
                        //                <body>
                        //                    <div class='container'>
                        //                        <h1>Account Activation</h1>
                        //                        <p>Dear User,</p>
                        //                        <p>Thank you for registering with us. Please click the button below to activate your account:</p>
                        //                        <p><a href='{activationUrl}' class='btn'>Activate Account</a></p>
                        //                        <p>If you did not register for this account, please ignore this email.</p>
                        //                        <p>Best regards,<br>Your Company</p>
                        //                    </div>
                        //                </body>
                        //                </html>
                        //                ";
                        await _emailService.SendEmailAsync(recipient, subject, body);
                        _logger.LogInformation($"Email sent to {recipient}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in executing SendEmailJob: {ex.Message}");
            }
        }

        private string GenerateOtp(string email) => BCrypt.Net.BCrypt.HashPassword(email);
    }
}
