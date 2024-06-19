using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Net.Mail;

namespace Sample_AP.Controllers;

[ApiController]
[Route("api/")]
public class MailController : ControllerBase
{
    public MailController() { }

    [HttpGet]
    [Route("mail/")]
    public IActionResult GetOrderByID(string email)
    {
        // SMTP Server
        string smtpAddress = "smtp.your-email-provider.com";
        int portNumber = 587; // 通常是 25, 465 或 587
        bool enableSSL = true;

        // 發送和收件
        string emailFrom = "your-email@example.com";
        string password = "your-email-password";
        string emailTo = "recipient-email@example.com";
        string subject = "Hello";
        string body = "Hello, This is Email sending test using .Net";

        using (MailMessage mail = new MailMessage())
        {
            mail.From = new MailAddress(emailFrom);
            mail.To.Add(emailTo);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            // 如果有附件，可以使用下面的代碼添加附件
            // mail.Attachments.Add(new Attachment("path/to/your/file.txt"));

            using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
            {
                smtp.Credentials = new NetworkCredential(emailFrom, password);
                smtp.EnableSsl = enableSSL;
                try
                {
                    smtp.Send(mail);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error sending email: " + ex.Message);
                }
            }
        }

        return Ok($"信件已發送至{email}");
    }
}
