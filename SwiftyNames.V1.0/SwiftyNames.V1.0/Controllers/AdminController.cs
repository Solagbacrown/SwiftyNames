using SwiftyNames.V1._0.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwiftyNames.V1._0.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        SwiftyNamesEntities Db = new SwiftyNamesEntities();
        //
        // GET: /Admin/
        [HttpGet]

        public ActionResult AdminPage()
        {
            ChangeName model = new ChangeName();
            var list = AdminReview();
            model.ChangeList = list;
            model.StartDate = DateTime.Today;
            model.EndDate = DateTime.Today;
            return View(model);

        }

        public List<ChangeName> AdminReview()
        {
            List<ChangeName> list = new List<ChangeName>();

            List<CustomerDetail> Namelist = Db.CustomerDetails.OrderByDescending(x => x.InfoId).ToList();
            foreach (var x in Namelist)
            {
                ChangeName model = new ChangeName();
                model.InfoId = x.InfoId;
                model.Date = x.Date;
                model.Phone = x.Phone;
                model.SessionKey = x.SessionKey;
                model.newsName = NewsPaperbyId(x.NewsPaperId);
                model.DayName = DaybyId(x.DayId);
                model.NewFullName = x.NewFullName;
                model.OldFullName = x.OldFullName;
                model.Address = x.Address;
                model.Email = x.Email;
                list.Add(model);

            }
            return list;
        }

        private string NewsPaperbyId (int Id)
        {
            var query = from category in Db.NewsPapersPrices
                        where category.Id == Id
                        select category;
            var content = query.ToList<NewsPapersPrice>();
            return content.SingleOrDefault().NewsPaper;
        }

        private string DaybyId(int Id)
        {
            var query = from category in Db.DayTypes
                        where category.DayTypeId == Id
                        select category;
            var content = query.ToList<DayType>();
            return content.SingleOrDefault().Day;
        }
        [HttpPost]
        public ActionResult AdminPage(ChangeName mode)
        {

            List<ChangeName> list = new List<ChangeName>();
            if (mode.StartDate != null && mode.EndDate != null) ;
            var query = Db.CustomerDetails.Where(emp => emp.Date >= mode.StartDate && emp.Date <= mode.EndDate);
           
                  
            foreach (var x in query)
            {
                ChangeName model = new ChangeName();

               
                model.InfoId = x.InfoId;
                model.Date = x.Date;
                model.Phone = x.Phone;
                model.SessionKey = x.SessionKey;
                model.newsName = NewsPaperbyId(x.NewsPaperId);
                model.DayName = DaybyId(x.DayId);
                model.DayId = x.DayId;
                model.Address = x.Address;
                model.NewFullName = x.NewFullName;
                model.OldFullName = x.OldFullName;
                model.Email = x.Email;
                list.Add(model);

            }
            mode.ChangeList = list;

            return View(mode);
        }
    }
}