using Microsoft.AspNetCore.Mvc;
using ScopeIndia.Models;
using MimeKit;
using MimeKit.Text;



using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using ScopeIndia.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using ScopeIndia.wwwroot.Extensions;



namespace ScopeIndia.Controllers
{
   
    public class WebHomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IStudent _student;
        private readonly ICourse _course;


        

        public WebHomeController(IConfiguration configuration, IStudent student,ICourse course)
        {

            _configuration = configuration;
            _student = student;
            _course = course;

        }

        //private bool CheckSession()
        //{
        //    string Cookies = HttpContext.Request.Cookies["Cookies"];
        //    string CookiesId = HttpContext.Request.Cookies["UserId"];
        //    string Session = HttpContext.Session.GetString("Session");

        //    if (Session != null || Cookies != null)
        //    {
        //        if (Session == null)
        //        {
        //            HttpContext.Session.SetString("Session", Cookies);
        //            HttpContext.Session.SetString("SessionId", CookiesId);
        //        }
        //        ViewBag.Session = Session;
        //        return true;
        //    }
        //    else
        //    {
        //        ViewBag.SessionOut = "Session Ends. Please login";
        //        return false;
        //    }
        //}
        //private bool CheckSession()
        //{
        //    string Cookies = HttpContext.Request.Cookies["Cookies"];
        //    string CookiesId = HttpContext.Request.Cookies["UserId"];
        //    string Session = HttpContext.Session.GetString("Session");

        //    if (Session != null || Cookies != null)
        //    {
        //        if (Session == null)
        //        {
        //            HttpContext.Session.SetString("Session", Cookies);
        //            HttpContext.Session.SetString("SessionId", CookiesId);
        //        }
        //        ViewBag.Session = Session;
        //        return true;
        //    }
        //    else
        //    {
        //        ViewBag.SessionOut = "Session Ends. Please login";
        //        return false;
        //    }
        //}
        private bool CheckSession()
        {
            string cookieValue = Request.Cookies["UserEmail"];
            Console.WriteLine("Cookie Value: " + cookieValue);
            //string cookieId = HttpContext.Request.Cookies["UserId"];
            //string sessionValue = HttpContext.Session.GetString("Session");

            if (cookieValue!= null)
            {

                HttpContext.Session.SetString("UserEmail", cookieValue);
                    //HttpContext.Session.SetString("SessionId", cookieId);
                    ViewBag.Session = cookieValue; // session is just set now
                

                return true;
            }
            else
            {
                ViewBag.SessionOut = "Session Ends. Please login";
                return false;
            }
        }
        public IActionResult Home()
        {
            CheckSession();
            return View();
        }
        public IActionResult AboutUs()
        {
            CheckSession();
            return View();
        }
        public IActionResult Contact()
        {
            CheckSession();
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
                ViewBag.contactMessageSuccess= "Email sent successfully";
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.contactMessageFail= "Error sending email: " + ex.Message;
            }

            return View();
        }

        public IActionResult Registration()
        {
            CheckSession();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Registration(StudentModel sm)
        {
            //if (sm == null || !_student.IsEmailExists(sm.Email))
            //{
            //    ViewBag.ValidEmail = "Enter a valid Email address";
            //    return View();
            //}
            if (!ModelState.IsValid)
            {

                return View(sm);

            }
            sm.Avatarpath = Upload(sm.Avatar);
            sm.AllHobbies = String.Join(",", sm.Hobbies);
            _student.Insert(sm);
            StudentModel student1 = _student.GetByEmail(sm.Email);
            string url = $" https://localhost:7175/WebHome/EmailVerification/{student1.Id}";

            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
                email.To.Add(MailboxAddress.Parse(sm.Email));
                email.Subject = $"Registration";
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"\n\nClick on url to confirm registration:{url}"
                };


                var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("jijoynpnlr@gmail.com", "ljyxpmpxgzdhcisg");
                smtp.Send(email);
                smtp.Disconnect(true);
                ViewBag.Email = "Email sent successfully. Check your email!";
                return View();
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



        public IActionResult Login()
        {
            //ModelState.Clear();
            return View();
        }





        [Route("/WebHome/EmailVerification/{id}")]
        public IActionResult EmailVerification(int id)
        {

            StudentModel student = _student.GetById(id);

            if (student == null)
            {
                return RedirectToAction("Login");
            }
            if (string.IsNullOrEmpty(student.Password))
            {
                student.Password = "defaultPassword"; // Set a default value if necessary
            }
            student.IsVerified = true;
            _student.Update(student);
            //Sent email method............
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
            email.To.Add(MailboxAddress.Parse(student.Email));

            email.Body = new TextPart(TextFormat.Plain)
            {
                Text = "Your email has been successfully verified"
            };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("jijoynpnlr@gmail.com", "ljyxpmpxgzdhcisg");
            smtp.Send(email);
            smtp.Disconnect(true);

            StudentModel student2 = _student.GetById(id);
            var checkverification = student2.IsVerified;
            ViewBag.EmailVerification = "Email verified successfully";
            TempData["EmailVerification"] = ViewBag.EmailVerification;
            return View("Login");
        }
        public string GetOTP()
        {
            Random random = new Random();
            var otp = random.Next(1000, 9999);
            return otp.ToString();
        }

        public IActionResult FirstTimeLogin()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult FirstTimeLogin(string email1)
        {
            if (string.IsNullOrEmpty(email1))
            {
                ViewBag.ErrorMessage = "Email is required!";
                return View();
            }
            StudentModel st = _student.GetByEmail(email1);
            if (st == null || !_student.IsEmailExists(email1))
            {
                ViewBag.ValidEmail = "Enter a valid Email address";
                return View();
            }

            else
            {
                string otp = GetOTP();
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("jijoynpnlr@gmail.com"));
                email.To.Add(MailboxAddress.Parse(email1));
                email.Subject = $"Creating new user";
                email.Body = new TextPart(TextFormat.Plain)
                {
                    Text = $"\n\nYour OTP is {otp}."
                };


                var smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("jijoynpnlr@gmail.com", "ljyxpmpxgzdhcisg");
                smtp.Send(email);
                smtp.Disconnect(true);
                TempData["OTP"] = otp;
                TempData["Email"] = email1;
                return View("OtpVerification");
            }
        }

        public IActionResult OtpVerification()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OtpVerification(string otp)
        {
            string mailOTP = otp;
            string checkotp = TempData["OTP"].ToString();
            try
            {
                if (mailOTP == checkotp)
                {
                    if (TempData.ContainsKey("Email"))
                    {
                        TempData["PasswordCreation"] = TempData["Email"].ToString();
                        TempData.Keep("PasswordCreation"); // Preserve email for future requests
                    }
                    else
                    {
                        return RedirectToAction("Login");
                    }
                    return RedirectToAction("PasswordCreation");
                }
                else
                {
                    return RedirectToAction("Login");
                }
                
            }
            catch(Exception e)
            {
                ViewBag.otpmessage = $"OTP entered doesn't match ,try again .{e}";
            }
            return View();
        }


        public IActionResult PasswordCreation()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PasswordCreation(NewUserModel num)
        {
            // Retrieve email from TempData
            if (!TempData.ContainsKey("PasswordCreation") || TempData["PasswordCreation"] == null)
            {
                TempData["PasswordError"] = "Session expired. Please request a new OTP.";
                return RedirectToAction("FirstTimeLogin");
            }

            string email = TempData["PasswordCreation"].ToString();
            

            //// Validate model state
            //if (!ModelState.IsValid)
            //{
            //    return View(num);
            //}

            // Ensure passwords match
            if (num.Password != num.ConfirmPassword)
            {
                ViewBag.PasswordError = "Passwords do not match. Please try again.";
                return View(num);
            }

            // Retrieve the student by email
            StudentModel student = _student.GetByEmail(email);
            if (student == null)
            {
                TempData["PasswordError"] = "User not found. Please register.";
                return RedirectToAction("Registration");
            }

            // Hash the password before saving
            // student.Password = BCrypt.Net.BCrypt.HashPassword(num.Password);
            student.Password = num.Password;

            // Update the user's password in the database
            _student.Update(student);

            // Provide success feedback and redirect
            TempData["PasswordSuccess"] = "Password set successfully! You can log in now.";
            return RedirectToAction("Login");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel login)
        
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            StudentModel student = _student.GetByEmail(login.Email);
            if (student == null || login.Password != student.Password)
            {
                ViewBag.LoginError = "Invalid email or password.";
                return View();
            }

            // Set session
            HttpContext.Session.SetInt32("UserId", student.Id);
            HttpContext.Session.SetString("UserEmail", student.Email);
            HttpContext.Session.SetString("UserName", student.FirstName);
            HttpContext.Session.SetString("UserAvatar", student.Avatarpath);

            Console.WriteLine("UserId: " + login.RememberMe);

            // Set cookies if Remember Me is checked
            if (login.RememberMe)
            {
                CookieOptions options = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(7),
                    IsEssential = true, // Optional: allows cookie even if consent isn't given
                    HttpOnly = true,    // Optional: makes cookie inaccessible via JavaScript
                    Secure = true,      // Optional: use only on HTTPS
                    SameSite = SameSiteMode.Strict // or Lax, depending on use
                };

                Response.Cookies.Append("UserEmail", login.Email, options);
                //Response.Cookies.Append("UserId", student.Id.ToString(), options); // if needed
                //Response.Cookies.Append("UserName", student.FirstName, options);   // if needed

            }

            // Check course selection count
            int selectedCount = _course.GetStudentCourseCount(student.Id);
            if (selectedCount >= 1)
            {
                return RedirectToAction("StudentCourseList");
            }

            return RedirectToAction("StudentDashboard");
        }


        public IActionResult ForgotPassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ForgotPassword(NewUserModel num)
        {

            if (!TempData.ContainsKey("PasswordCreation") || TempData["PasswordCreation"] == null)
            {
                ViewBag.PasswordError = "Session expired. Please request a new OTP.";
                return RedirectToAction("ForgotPassword");
            }

            string email = TempData["PasswordCreation"].ToString();

            if (!ModelState.IsValid)
            {
                return View(num);
            }

            // Ensure passwords match (assuming ConfirmPassword exists in NewUserModel)
            if (num.Password != num.ConfirmPassword)
            {
                ViewBag.PasswordError = "Passwords do not match. Please try again.";
                return View();
            }

            // Replace the line causing the error with the correct BCrypt usage


            // Find user by email and update the password
            StudentModel student = _student.GetByEmail(email);
            if (student == null)
            {
                ViewBag.PasswordMatch = "User not found. Please register";
                return RedirectToAction("Registration");
            }

            // Save hashed password
            // student.Password = BCrypt.Net.BCrypt.HashPassword(num.Password);
            student.Password = num.Password;
            _student.Update(student);// Update user in database


            ViewBag.PasswordSuccess = "Password set successfully! You can log in now.";
            return RedirectToAction("Login");
        }
        public IActionResult StudentDashboard()
        {
            CheckSession();
            // Check session
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UserEmail")))
            {
                return RedirectToAction("Login");
            }

            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Avatar = HttpContext.Session.GetString("UserAvatar");

            // Fetch all courses from the database
            List<CourseModel> courses = _course.GetAll();
            return View(courses);
        }

        [HttpPost]
        public IActionResult StudentDashboard(int courseId)
        {

            int? studentId = HttpContext.Session.GetInt32("UserId");
            var avatar = HttpContext.Session.GetString("UserAvatar");
            var email = HttpContext.Session.GetString("UserEmail");
            if (studentId == null)
                {
                    return RedirectToAction("Login");
                }
                StudentModel sm=new StudentModel();
                sm.CourseId = String.Join(",", courseId.ToString());
                _student.Update(sm);

            int selectedCount = _course.GetStudentCourseCount(studentId.Value);
                if (selectedCount >= 1)
                {
                    TempData["CourseCount"] = 1;
                    TempData["Alert"] = "You already joined for a course. After completing the selected one, you can join another.";
                    return RedirectToAction("StudentCourseList");
                }

                _course.AddCourseToStudent(studentId.Value, courseId);
                return RedirectToAction("StudentCourseList");


        }



        public IActionResult StudentCourseList()
        {
            int? studentId = HttpContext.Session.GetInt32("UserId");
            
            if (studentId == null)
            {
                return RedirectToAction("Login");
            }

            ViewBag.UserEmail = HttpContext.Session.GetString("UserEmail");
            ViewBag.UserName = HttpContext.Session.GetString("UserName");
            ViewBag.Avatar = HttpContext.Session.GetString("UserAvatar");

            ViewBag.Alert = TempData["Alert"];

            List<CourseModel> selectedCourses = _course.GetSelectedCourses(studentId.Value);
            return View(selectedCourses);
        }
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();

            Response.Cookies.Delete("Cookies");
            Response.Cookies.Delete("CookiesId");
            Response.Cookies.Delete("UserName");

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }


        public IActionResult EditProfile( )
        {

            /*int? studentId = HttpContext.Session.GetInt32("StudentId");
            if (studentId == null)
            {
                return RedirectToAction("StudentDashboard");
            }

            var student = _student.GetById(studentId.Value);
            if (student == null)
            {
                return NotFound();
            }*/

            CheckSession();

            return View();
        }

        [HttpPost]
        public IActionResult EditProfile(StudentModel model, IFormFile avatar)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            StudentModel existingStudent = _student.GetByEmail(HttpContext.Session.GetString("UserEmail"));

            existingStudent.FirstName = model.FirstName;
            existingStudent.LastName = model.LastName;
            existingStudent.Email = model.Email;
            existingStudent.PhNo = model.PhNo;
            existingStudent.Country = model.Country;
            existingStudent.State = model.State;
            existingStudent.City = model.City;
            existingStudent.DOB = model.DOB;
            

            // Save updated data
            _student.Update(existingStudent);

            TempData["Success"] = "Profile updated successfully!";
            return RedirectToAction("StudentDashboard");
        }
        

    }
}

