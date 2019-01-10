using SwiftyNames.V1._0.Models;
using SwiftyNames.V1._0.Persistants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;


namespace SwiftyNames.V1._0.Controllers
{
    
    public class NamesCatalogController : Controller
    {
        

        SwiftyNamesEntities context = new SwiftyNamesEntities();
        //private Logics logic;
        // GET: NamesCatelog

        
       
        

        public ActionResult CatalogView()
        {
            ChangeName model = new ChangeName();
            var list = CatalogReview();
            model.CatalogList = list;
            return View(model);
           
        }
        public List<ChangeName> CatalogReview()
        {
            List<ChangeName> list = new List<ChangeName>();

            List<CustomerDetail> Namelist = context.CustomerDetails.OrderByDescending(x => x.InfoId).ToList();
            foreach (var x in Namelist)
            {
                ChangeName model = new ChangeName();
                model.InfoId = x.InfoId;
                model.Date = x.Date;
                model.Phone = x.Phone;
                model.SessionKey = x.SessionKey;
                model.NewFullName = x.NewFullName;
                model.OldFullName = x.OldFullName;
                model.Address = x.Address;
                model.Email = x.Email;
                list.Add(model);

            }
            return list;
        }

       
        private void BindCombo(NewsPaperModel model, IList<NewsPapersPrice> AvailableNewsPaper)
        {
            List<NewsPapersPrice> importname = new List<NewsPapersPrice>();
            model.NewsPapers = new MultiSelectList(AvailableNewsPaper, "Id", "NewsPaper","Price", importname.Select(x => x.Id));

        }
        [HttpGet]
        public ActionResult AvailableNewsPaper()
        {
             NewsPaperModel model = new NewsPaperModel();
             List<NewsPapersPrice> newspapers = new List<NewsPapersPrice>();
             newspapers = context.NewsPapersPrices.ToList();
             model.PriceList = new MultiSelectList(newspapers, "Id","Price",newspapers.Select(x => x.Id));
             model.NewsPapers = new MultiSelectList(newspapers, "Price", "NewsPaper", newspapers.Select(x => x.PriceTag));
             ViewBag.delievery = "Do you want it delivered to your address";
             return View(model);
        }

        [HttpPost]
        public ActionResult AvailableNewsPaper(NewsPaperModel model)
        {
            
            Delivery deliver = new Delivery();
            if(!string.IsNullOrEmpty(model.Address) && !string.IsNullOrEmpty(model.phoneNumber))
            {
                deliver.Address = model.Address;
                deliver.PhoneNumber = model.phoneNumber;
                deliver.DateOrdered = DateTime.Now;
                context.Deliveries.Add(deliver);
                context.SaveChanges();
                TempData["success"] = "Your NewsPaper will be delivered to the given address in three days you can now proceed to payment";
            }

            List<NewsPapersPrice> newspapers = new List<NewsPapersPrice>();
            newspapers = context.NewsPapersPrices.ToList();
            model.PriceList = new MultiSelectList(newspapers, "Id", "Price", newspapers.Select(x => x.Id));
            model.NewsPapers = new MultiSelectList(newspapers, "Price", "NewsPaper", newspapers.Select(x => x.PriceTag));
            ViewBag.delievery = "Do you want it delivered to your address";
            model.phoneNumber = string.Empty;
            model.Address = string.Empty;

            return View(model);
        }
        public ActionResult ChangeOfNamey()
        {
            return View();
        }
        private IEnumerable<SelectListItem> Nationalitems (NameChanger model)
        {
            List<Nationality> national = new List<Nationality>();
            national = context.Nationalities.Where(x => x.Adjective != null && x.Abbreviation!="-").ToList();
            var  list = from s in national
                        select new SelectListItem
                        {
                            Selected = s.NationalityId.ToString() == model.CountryId.ToString(),
                            Text = s.Adjective,
                            Value = s.NationalityId.ToString()
                        };
            model.Nationality = "Nigerian";
            return list;
        }
        private IEnumerable<SelectListItem> NewsPaper(NameChanger model)
        {
            List<NewsPapersPrice> dailies = new List<NewsPapersPrice>();
            dailies = context.NewsPapersPrices.ToList();
            var list = from s in dailies
                       select new SelectListItem
                       {
                           Selected = s.Id.ToString() == model.NewsId.ToString(),
                           Text = s.NewsPaper,
                           Value = s.Id.ToString()
                       };
           
            return list;
        }
        private IEnumerable<SelectListItem> countryItems(NameChanger model)
        {
            List<Nationality> national = new List<Nationality>();
            national = context.Nationalities.Where(x => x.Adjective != null && x.Adjective!="-").ToList();
            var list = from s in national
                       select new SelectListItem
                       {
                           Selected = s.NationalityId.ToString() == model.CountryId.ToString(),
                           Text = s.Country,
                           Value = s.NationalityId.ToString()
                       };
            model.country = "Nigeria";
            return list;
        }
        private IEnumerable<SelectListItem> HomeCountry(NameChanger model)
        {
            List<Nationality> national = new List<Nationality>();
            national = context.Nationalities.Where(x =>x.Country=="Nigeria").ToList();
            var list = from s in national
                       select new SelectListItem
                       {
                           Selected = s.NationalityId.ToString() == model.CountryId.ToString(),
                           Text = s.Country,
                           Value = s.NationalityId.ToString()
                       };
           
            return list;
        }
        private IEnumerable<SelectListItem> StatesList(NameChanger model)
        {
            List<State> national = new List<State>();
            national = context.States.ToList();
            var list = from s in national
                       select new SelectListItem
                       {
                           Selected = s.Id.ToString() == model.stateId.ToString(),
                           Text = s.StateName,
                           Value = s.Id.ToString()
                       };
           
            return list;
        }

        private IEnumerable<SelectListItem> NoticeList(NameChanger model)
        {
            List<State> national = new List<State>();
            national = context.States.ToList();
            var list = from s in national
                       select new SelectListItem
                       {
                           Selected = s.Id.ToString() == model.stateId.ToString(),
                           Text = s.StateName,
                           Value = s.Id.ToString()
                       };

            return list;
        }
        
        private List<SelectListItem> bindComboDate()
        {
            var listItems = new List<SelectListItem>
         {
          new SelectListItem { Text = "During the week", Value="1" },
          new SelectListItem { Text = "Weekends", Value="2" },
        
          };
            return listItems;
        }

        private List<SelectListItem> bindComboHear()
        {
            var listItems = new List<SelectListItem>
         {
          new SelectListItem { Text = "Radio", Value="1" },
          new SelectListItem { Text = "NewsPaper", Value="2" },
          new SelectListItem { Text = "Facebook", Value="3" },
          new SelectListItem { Text = "Blog", Value="4" },
          new SelectListItem { Text = "LinkedIn", Value="5" },
          new SelectListItem { Text = "Youtube", Value="5" },
          new SelectListItem { Text = "Others", Value="6" },
        
          };
            return listItems;
        }
       
        private List<SelectListItem> bindCombo()
        {
            var listItems = new List<SelectListItem>
         {
          new SelectListItem { Text = "No", Value="0" },
          new SelectListItem { Text = "Yes", Value="1" },
        
          };
            return listItems;
        }
      
        [HttpGet]
        public ActionResult ChangeOfNameForm()
        {
            NameChanger model = new NameChanger();
            List<Nationality> national = new List<Nationality>();
            national = context.Nationalities.Where(x=>x.Adjective!=null).ToList();
            model.NAtionalities = Nationalitems(model);

            model.countries = countryItems(model);
            model.Homecountries = HomeCountry(model);
            model.StateList = StatesList(model);
            model.NewsPapers = NewsPaper(model);

            model.AreyoumarriedList = bindCombo();
            model.LegalpuporseList = bindCombo();
            model.ConvictedList = bindCombo();
            model.DivoiceList = bindCombo();
            model.Hearabout = bindComboHear();
          

            List<NoticeLevel> notices = new List<NoticeLevel>();
            notices = context.NoticeLevels.ToList();
            model.NoticeLevel = new MultiSelectList(notices, "Id", "NoticeName", notices.Select(x => x.Id));

            List<Reason> Reasons = new List<Reason>();
            Reasons = context.Reasons.ToList();
            model.Reasons = new MultiSelectList(Reasons, "Id", "NameChangeReasons", Reasons.Select(x => x.Id));

            model.PreferedDate = bindComboDate();
            return View(model);
        }
    }
}