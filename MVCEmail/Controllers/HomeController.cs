using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCEmail.Models;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MVCEmail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        static string alp;
        // Random sayı üretip tuttuk!
        public static string Randomsayi(string sayi)
        {
            
            Random rnd = new Random();
            sayi =  rnd.Next(1000, 9000).ToString();
            alp = sayi;
            return alp;
        }
        
                  

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Contact(EmailFormModel model,string sayi) // statik oluşturacak metodu yazdık!
        {
          

            if (ModelState.IsValid)
            {
                var body = "<p>EntryCode: {0} </p>";
                var message = new MailMessage();
                
               
                

                message.To.Add(new MailAddress(model.FromEmail));  // replace with valid value 
                message.From = new MailAddress("testonay1@hotmail.com");  // replace with valid value 
                message.Subject = "Securty Code";
                message.Body = string.Format(body,Randomsayi(sayi)); /*model.Message*/ // random sayi cağırdık!
                message.IsBodyHtml = true;

                using (var smtp = new SmtpClient()) // bu kısım nerden gönderilcek ile ilgili
                {
                    var credential = new NetworkCredential
                    {
                        UserName = "testonay1@hotmail.com",  // geçerli mail adresi ile değiştirin // cahange your Email!
                        Password = "ewqewqcweqesw"  // geçerli şifre ile değiştirin // change your pass!
                    };
                    smtp.Credentials = credential;
                    smtp.Host = "smtp-mail.outlook.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                    return RedirectToAction("Sent");

                }
            }

            return View(model);
        }
        public ActionResult Sent()
        {

            return View();
        }
       [HttpPost] // post edildi Sent Sayfasına
        public ActionResult Sent(EmailFormModel model)
        {
            string girilensayi;// text'te girilen sayiyi aldık!
            girilensayi =(model.EntryCode);


            if (alp == girilensayi)// girilen sayi ile tutulan sayiyi esitledik 
            {
                return RedirectToAction("About");
            }
            else
            {
                return RedirectToAction("Index");
            }

         



        }

    }
}