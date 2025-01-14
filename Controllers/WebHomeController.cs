using Microsoft.AspNetCore.Mvc;
using ScopeIndia.Models;
using MimeKit;
using MimeKit.Text;


using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace ScopeIndia.Controllers
{
    public class WebHomeController : Controller
    {
        private readonly IConfiguration _configuration;
       public WebHomeController(IConfiguration configuration) 
       { 
            _configuration = configuration;
       }
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Contact(ContactModel cfvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cfvm);
            }

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(cfvm.UserEmail));
                email.To.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
                email.Subject = $"Subject: {cfvm.Subject}";
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"From: {cfvm.UserEmail}\nSubject: {cfvm.Subject}\nBody: {cfvm.Message}"
                };

                
                var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("jijoynpnlr@gmail.com", "ljyxpmpxgzdhcisg");
                smtp.Send(email);
                smtp.Disconnect(true);
                ViewBag.Email = "Email sent successfully";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Email = "Error sending email: " + ex.Message;
            }

            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }
    }
}

