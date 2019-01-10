using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwiftyNames.V1._0.Models
{
    public class ChangeName
    {
       
        public IEnumerable<SelectListItem> Day { get; set; }
        public IEnumerable<SelectListItem> NewsPapers { get; set; }

     
        public int InfoId { get; set; }
        [Display(Name="Newspaper")]
        public string newsName { get; set; }
        public string newsImage { get; set; }
        [Display(Name = "Day")]
       
        public string DayName { get; set; }
        [Display(Name = "Former Full Name")]
        public string OldFullName { get; set; }
        [Display(Name = "New Full Name")]
        public string NewFullName { get; set; }

       

        [Display(Name = "Address")]
        [Required(ErrorMessage = "This field is required.")]
        public string Address { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [RegularExpression("[0-9]{11}")]
        public string Phone { get; set; }

        public string SessionKey { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public int NewsPaperId { get; set; }
        [EmailAddress(ErrorMessage = " A valid email address is required.")]
        [Required]
        [Display(Name = "E-Mail Address")]
        public string Email { get; set; }

        public int DayId { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public List<ChangeName> ChangeList { get; set; }
        public List<ChangeName> CatalogList { get; set; }
       
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<ChangeName> NewsPaperlist { get; set; }
        public List<ChangeName> customerlist { get; set; }
        public List<ChangeName> newslist { get; set; }
        [Display(Name = "SEARCH")]
        public string Search { get; set; }
        public int Id { get; set; }
        public string NewsPaper { get; set; }
        public string PaperImage { get; set; }
     
        public string PriceTag { get; set; }
        public string NewsPaperName { get; set; }
        public string PaperImageName { get; set; }
        public decimal Price { get; set; }
        public string PriceTagName { get; set; }
        public string PaperSlogan { get; set; }
        public string NoticePrice { get; set; }
        public int NoticeId { get; set; }
        public string NoticeName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "This field is required.")]
        [RegularExpression("[0-9]{11}")]
        public string NoticePhone { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "E-Mail Address")]
        [EmailAddress]
        public string NoticeEmail { get; set; }

        [MaxLength (200, ErrorMessage="Not more than 200 words") ]
        [Display(Name = "Address")]
        [Required(ErrorMessage = "This field is required.")]
        public string NoticeWord { get; set; }

        public int NoticeDayId { get; set; }
        public int NoticePaperId { get; set; }
        public DateTime  NoticeDate{get; set;}
    }
}