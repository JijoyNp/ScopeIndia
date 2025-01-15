using Microsoft.AspNetCore.Mvc;
using ScopeIndia.Models;
using MimeKit;
using MimeKit.Text;


using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using ScopeIndia.Data;

namespace ScopeIndia.Controllers
{
    public class WebHomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudent _student;
       public WebHomeController(IConfiguration configuration,IStudent student) 
       { 
            _configuration = configuration;
            _student = student;
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
        public IActionResult Contact(ContactModel cm)
        {
            if (!ModelState.IsValid)
            {
                return View(cm);
            }

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(cm.UserEmail));
                email.To.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
                email.Subject = $"Subject: {cm.Subject}";
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"From: {cm.UserEmail}\nSubject: {cm.Subject}\nBody: {cm.Message}"
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(StudentModel sm)
        {
            if (!ModelState.IsValid)
                 return View(sm);
            _student.Insert(sm);
            return View();
        }
    }
}

