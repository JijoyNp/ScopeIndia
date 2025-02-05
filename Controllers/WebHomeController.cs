using Microsoft.AspNetCore.Mvc;
using ScopeIndia.Models;
using MimeKit;
using MimeKit.Text;
using System;


using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using ScopeIndia.Data;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

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
            {

                 return View(sm);

            }
            sm.Avatarpath = Upload(sm.Avatar);
            sm.AllHobbies=String.Join(",",sm.Hobbies);
            _student.Insert(sm);

            string url= "https://localhost:7175/WebHome/Registration";

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(sm.Email));
                email.To.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
                email.Subject = $"Registration";
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"{url}"
                };


                var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("jijoynpnlr@gmail.com", "ljyxpmpxgzdhcisg");
                smtp.Send(email);
                smtp.Disconnect(true);
                ViewBag.Email = "Email sent successfully";
                return View("Home");
            }
            catch (Exception ex)
            {
                ViewBag.Email = "Error sending email: " + ex.Message;
            }


            return View();
        }

        public string? Upload(IFormFile myfile)
        {
            string? upload_path = null;

            string File_Name = myfile.FileName;
            File_Name = Path.GetFileName(File_Name);
            string upload_folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads");
            if (!Directory.Exists(upload_folder))
            {
                Directory.CreateDirectory(upload_folder);
            }

            upload_path = Path.Combine(upload_folder, File_Name);
            try
            {
                if (!File_Name.Except(upload_path).Any())
                {
                    var uploadsteam = new FileStream(upload_path, FileMode.Create);
                    myfile.CopyTo(uploadsteam);
                    ViewBag.AvatarMessage = "Upload Success";
                }
            }
            catch (IOException FailureMessage)
            {
                ViewBag.AvatarMessage = "Upload Failure. Please try again";


            }

            return File_Name;
        }

        [HttpGet]
        public IActionResult Login(StudentModel sm)
        {
            return View();
        }

        

    }
}

