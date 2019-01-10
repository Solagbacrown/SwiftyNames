using SwiftyNames.V1._0.Models;
using SwiftyNames.V1._0.Persistants;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SwiftyNames.V1._0.Controllers
{
    public class HomeController : Controller
    {
        SwiftyNamesEntities Db = new SwiftyNamesEntities();

        public ActionResult PublicNewsSearch()
        {

            ChangeName model = new ChangeName();
            var list = NewsContent();
            model.newslist = list;
            ViewBag.Id = model.Id;
            return View(model);
        }

        public List<ChangeName> NewsContent()
        {
            List<ChangeName> list = new List<ChangeName>();

            List<NewsPapersPrice> noticelist = Db.NewsPapersPrices.OrderBy(x => x.NewsPaper).ToList();

            foreach (var a in noticelist)
            {
                ChangeName model = new ChangeName();
                model.Id = a.Id;
                model.NewsPaperName = a.NewsPaper;
                model.NoticePrice = a.NoticePrice;
                model.PaperImageName = a.PaperImage;
                model.PaperSlogan = a.PaperSlogan;

                list.Add(model);

            }
            return list;
        }
        [HttpPost]
        public ActionResult PublicNewsSearch(ChangeName mode)
        {

            List<ChangeName> list = new List<ChangeName>();
            var r = Db.NewsPapersPrices.ToList();
            foreach (var a in r)
            {
                ChangeName model = new ChangeName();
                model.Id = a.Id;
                model.NewsPaper = a.NewsPaper;
                model.NoticePrice = a.NoticePrice;
                model.PaperImage = a.PaperImage;
                model.PaperSlogan = a.PaperSlogan;
                list.Add(model);

            }
            mode.newslist = list;

            return View(mode);
        }
        [HttpGet]
        public ActionResult PublicNotice(int? id = null)
        {
            if (id == null)
            {
                return RedirectToAction("PublicNewsSearch", "Home");
            }
            ChangeName model = new ChangeName();

            model.Day = Day(model);
            model.NoticePaperId = id.Value;
            var property = Db.NewsPapersPrices.Where(x => x.Id == id).SingleOrDefault();
         //   var session = Guid.NewGuid();
           // model.SessionKey = session.ToString();
            ViewBag.Image = property.PaperImage;
            ViewBag.price = property.NoticePrice; ;



            return View(model);

        }

       
        private IEnumerable<SelectListItem> Day(ChangeName model)
        {
            List<DayType> days = new List<DayType>();
            days = Db.DayTypes.ToList();
            var list = from s in days
                       select new SelectListItem
                       {
                           Selected = s.DayTypeId.ToString() == model.NoticeDayId.ToString(),
                           Text = s.Day,
                           Value = s.DayTypeId.ToString(),

                       };

            return list;
        }



        [HttpPost]
        public ActionResult PublicNotice (ChangeName model)
        {

            
            if (model != null)
            {
                

                PublicNotice publicNotice = new PublicNotice();
                publicNotice.NoticeId = model.NoticeId;
                publicNotice.NoticeName = model.NoticeName;
                publicNotice.NoticeEmail = model.NoticeEmail;
                publicNotice.NoticePhone = model.NoticePhone;
                publicNotice.NoticeWord = model.NoticeWord;
                publicNotice.NoticePaperId = model.Id;
                publicNotice.NoticeDayId = model.NoticeDayId;
                publicNotice.NoticeDate = DateTime.Now;
                Db.PublicNotices.Add(publicNotice);
                
                Db.SaveChanges();
                TempData["Success"] = "You have successfully made your public notice";

                    return RedirectToAction("PublicNotice");

             
            }
            else
            {
                ModelState.AddModelError("", "This field is required");

            }
            TempData["Success"] = "You have successfully made your public notice";
            return RedirectToAction("PublicNotice");

        }


     
        public ActionResult Blog()
        {
            return View();
        }
       
        public ActionResult Index()
        {
            ContactModel model = new ContactModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ContactModel model)
        {
            ToAdminEmail(model);
            return View();
        }
        public void ToAdminEmail(ContactModel model)
        {


            string MessageFrom = string.Format("You have a new message from {0} with Phone number {1} and Email {2}, See the message below:<br/>" ,model.Name, model.Phone, model.Email);

            var myMessage = new MailMessage();
            myMessage.To.Add(new MailAddress("ayodeleenitilo@yahoo.com"));  // replace with valid value 
            myMessage.Bcc.Add(new MailAddress("ayodeleenitilo@gmail.com"));
            myMessage.From = new MailAddress("festusoluyide@gmail.com");  // replace with valid value
            myMessage.Subject = model.Subject;
            myMessage.Body = MessageFrom + "<br/>" + model.Message;

            myMessage.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "festusoluyide@gmail.com",  // replace with valid value
                    Password = "7343good"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Send(myMessage);
            }
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
    }
}
