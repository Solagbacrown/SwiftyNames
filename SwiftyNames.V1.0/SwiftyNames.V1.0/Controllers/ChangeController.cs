using SwiftyNames.V1._0.Models;
using SwiftyNames.V1._0.Persistants;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net.Mail;
using System.Net;

namespace SwiftyNames.V1._0.Controllers
{
    public class ChangeController : Controller
    {
        SwiftyNamesEntities Db = new SwiftyNamesEntities();


        [HttpGet]
        public ActionResult NewsSearch()
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

            List<NewsPapersPrice> objlist = Db.NewsPapersPrices.OrderBy(x => x.NewsPaper).ToList();

            foreach (var a in objlist)
            {
                ChangeName model = new ChangeName();
                model.Id = a.Id;
                model.NewsPaperName = NewsContentbyId(a.Id);
                model.PriceTagName = NewsContentbyPrice(a.Id);
                model.PaperImageName = NewsContentbyImage(a.Id);
                model.PaperSlogan = a.PaperSlogan;
                list.Add(model);

            }
            return list;
        }
        private NewsPapersPrice NewsContent(int NewsId)
        {
            var query = from category in Db.NewsPapersPrices
                        where category.Id == NewsId
                        select category;
            var content = query.ToList<NewsPapersPrice>();
            return content.SingleOrDefault();
        }
        private string NewsContentbyImage(int NewsId)
        {
            var query = from category in Db.NewsPapersPrices
                        where category.Id == NewsId
                        select category;
            var content = query.ToList<NewsPapersPrice>();
            return content.SingleOrDefault().PaperImage;
        }
        private string NewsContentbyId(int NewsId)
        {
            var query = from category in Db.NewsPapersPrices
                        where category.Id == NewsId
                        select category;
            var content = query.ToList<NewsPapersPrice>();
            return content.SingleOrDefault().NewsPaper;
        }
        private string NewsContentbyPrice(int NewsId)
        {
            var query = from category in Db.NewsPapersPrices
                        where category.Id == NewsId
                        select category;
            var content = query.ToList<NewsPapersPrice>();
            return content.SingleOrDefault().PriceTag;
        }
        [HttpPost]
        public ActionResult NewsSearch(ChangeName mode)
        {

            List<ChangeName> list = new List<ChangeName>();
            var r = Db.NewsPapersPrices.ToList();
            foreach (var a in r)
            {
                ChangeName model = new ChangeName();
                model.Id = a.Id;
                model.NewsPaper = a.NewsPaper;
                model.PriceTag = a.PriceTag;
                model.PaperImage = a.PaperImage;
                model.PaperSlogan = a.PaperSlogan;
                list.Add(model);

            }
            mode.newslist = list;

            return View(mode);
        }
     
        //
        // GET: /Change/
        [HttpGet]
        public ActionResult Change(int? id=null)
        {
            if (id == null)
            {
                return RedirectToAction("NewsSearch", "Change");
            }
            ChangeName model = new ChangeName();

            model.Day = Day(model);
            model.NewsPapers = NewsPaper(model);
           model.NewsPaperId = id.Value;
            var property = Db.NewsPapersPrices.Where(x=>x.Id==id).SingleOrDefault();
            var session = Guid.NewGuid();
            model.SessionKey = session.ToString();
            ViewBag.Image = property.PaperImage;
            ViewBag.price = property.PriceTag; 
          


            return View(model);

        }

        private IEnumerable<SelectListItem> NewsPaper(ChangeName model)
        {
            List<NewsPapersPrice> dailies = new List<NewsPapersPrice>();
            dailies = Db.NewsPapersPrices.ToList();
            var list = from s in dailies
                       select new SelectListItem
                       {
                           Selected = s.Id.ToString() == model.NewsPaperId.ToString(),
                           Text = s.NewsPaper ,
                           Value = s.Id.ToString(),

                       };

            return list;
        }
        private IEnumerable<SelectListItem> Day(ChangeName model)
        {
            List<DayType> days = new List<DayType>();
            days = Db.DayTypes.ToList();
            var list = from s in days
                       select new SelectListItem
                       {
                           Selected = s.DayTypeId.ToString() == model.DayId.ToString(),
                           Text = s.Day,
                           Value = s.DayTypeId.ToString(),

                       };

            return list;
        }
       


        [HttpPost]
        public ActionResult Change(ChangeName model, HttpPostedFileBase uploadedFile, HttpPostedFileBase IdentityFile)
        {

            

             var validImageTypes = new string[]
            {
            "image/gif",
            "image/jpeg",
            "image/pjpeg",
            "image/png",
            "application/pdf",
           "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
       
             };

           
          
            if (model != null && uploadedFile != null && IdentityFile != null)
            {
                //if (model != null)
                // {
                // if (uploadedFile.ContentLength == 0)
                // {
                //TempData["FileError"] = "Please upload the your marriage certificate or sworn affidavit(you can use your phone to take the pictures of doucument)";
                //    return RedirectToAction("Change");
                //  }
                //if (IdentityFile.ContentLength == 0)
                // {
                //TempData["FileError"] = "Please upload any means of identification(you can use your phone to take the pictures of document";
                //     return RedirectToAction("Change");
                // }

                CustomerDetail customerDetail = new CustomerDetail();
                customerDetail.InfoId = model.InfoId;
                customerDetail.Address = model.Address;
                customerDetail.Email = model.Email;
                customerDetail.Phone = model.Phone;
                customerDetail.SessionKey = model.SessionKey;
                customerDetail.NewsPaperId = model.Id;
                customerDetail.DayId = model.DayId;
                customerDetail.Date = DateTime.Now;
                customerDetail.OldFullName = model.OldFullName;
                customerDetail.NewFullName = model.NewFullName;
                Db.CustomerDetails.Add(customerDetail);



                Upload files = new Upload();


                if (!validImageTypes.Contains(uploadedFile.ContentType) || !validImageTypes.Contains(IdentityFile.ContentType))
                {
                    ModelState.AddModelError("", "Please choose either a GIF, JPG or PNG image.");
                    TempData["Picture"] = "You need to upload either a GIF, JPG, or PNG image.";
                    return RedirectToAction("Change");

                }
                else
                {
                    string filename = System.IO.Path.GetFileName(uploadedFile.FileName);
                    string physicalPath = Server.MapPath("~/App_Data/filer/" + filename);
                    uploadedFile.SaveAs(physicalPath);
                    files.NameOfFile = filename;

                    string Idfilename = System.IO.Path.GetFileName(IdentityFile.FileName);
                    string IdentityFilePath = Server.MapPath("~/App_Data/IdentityDoc/" + Idfilename);
                    IdentityFile.SaveAs(IdentityFilePath);
                    files.IdentityFile = Idfilename;

                    files.SessionKey = model.SessionKey;
                    Db.Uploads.Add(files);

                }
                try
                {
                    Db.SaveChanges();
                    TempData["Success"] = "You have successfully changed your name";
                    ToAdminEmail(model);
                    return RedirectToAction("Change");

                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            Trace.TraceInformation("Property: {0} Error: {1}",
                                                    validationError.PropertyName,
                                                    validationError.ErrorMessage);
                        }
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "This field is required");

            }
            TempData["Success"] = "You have successfully changed your name";
           
            return RedirectToAction("Change");

        }
        


        public void ToAdminEmail(ChangeName model)
        {


            string MessageFrom = string.Format("Dear {0}, You have sucessfully changed your name from {0} to {1}:<br/>", model.OldFullName, model.NewFullName);

            var myMessage = new MailMessage();
            myMessage.To.Add(new MailAddress("ayodeleenitilo@yahoo.com"));  // replace with valid value 
            myMessage.Bcc.Add(new MailAddress("ayodeleeenitilo@gmail.com"));
            myMessage.Bcc.Add(new MailAddress(model.Email));
            myMessage.From = new MailAddress("festusoluyide@gmail.com");  // replace with valid value
            myMessage.Subject = "CHANGE OF NAME";
            myMessage.Body = MessageFrom;

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

       
    }
}

    


   
