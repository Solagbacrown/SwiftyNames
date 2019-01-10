using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SwiftyNames.V1._0.Models
{
    public class NameChanger
    {


        public NameChanger()
        {

            Nationality = "Nigerian";
            country = "Nigeria";
         }
        public IEnumerable<SelectListItem> NAtionalities { get; set; }
        public IEnumerable<SelectListItem> countries { get; set; }
        public List<SelectListItem> AreyoumarriedList { get; set; }
        public List<SelectListItem> DivoiceList { get; set; }
        public List<SelectListItem> PreferedDate { get; set; }
        public List<SelectListItem> LegalpuporseList { get; set; }
        public List<SelectListItem> ConvictedList { get; set; }
        public IEnumerable<SelectListItem> Homecountries { get; set; }
        public IEnumerable<SelectListItem> StateList { get; set; }
        public IEnumerable<SelectListItem> NewsPapers { get; set; }

        public List<SelectListItem> Hearabout { get; set; }

        public MultiSelectList NoticeLevel { get; set; }

        public MultiSelectList Reasons { get; set; }
        public int CountryId { get; set; }
        public int stateId { get; set; }
        public int NewsId { get; set; }

        //process1
        [Required]
        [Display(Name = "Nationality")]
        public string Nationality { get; set; }
        [Display(Name = "Are you presently in the country?")]
        public string YourPresentCountry { get; set; }

        

        //Process2
        [Required]
        [Display(Name = "Are you Married and seeking to change your maiden name?")]
        public bool AreYouMarried { get; set; }
        [Required]
        [Display(Name = "Are you divorce and seeking to change your name back to your maiden name or former married name?")]
        public bool Divorcepuporse { get; set; }
        [Required]
        [Display(Name = "Is the purpose of this name change to avoid any court order, legal action,debt od finacial obligation?")]
        public bool Legalpuporse { get; set; }
        [Required]
        [Display(Name = "Have you ever been convicted of felony, or are there any criminal proceedings pending against you?")]
        public bool haveyoubeenconvicted { get; set; }

        //Process3
        [Required]
        [Display(Name = "Your current name")]
        public string yourcurrentName { get; set; }

        [Required]
        [Display(Name = "Home Address")]
        public string street { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string country { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Personal Phone Number")]
        public string PersonalPhoneNumber { get; set; }

        //Process4
        [Required]
        [Display(Name = "New Name")]
        public string ProposedName { get; set; }

        [Required]
        [Display(Name = " Confirm New Name")]
        public string ConfirmProposedName { get; set; }

        [Required]
        [Display(Name = "Why are you changing your name")]
        public string WhyAreyouChangingYourName { get; set; }

        [Required]
        [Display(Name = "You are Changing Name for the Notice of")]
        public string NoticeOf{ get; set; }

        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Upload Marriage Certificate,Affidavit or legal report backing your reason of change of Name,your birth certificate and ID card if any")]
        public string UploadCertificates { get; set; }

        
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please select your preffered leading National Daily(NewsPapers")]
        public string Prefferednews {get; set;}

        [Required]

        [Display(Name = "Please select the day of the week you want your advert to be out")]
        public string dateRealease { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please select your preffered leading National Daily(NewsPapers")]
        public string PrefferedDatenewsofPublication { get; set; }
        //Process5
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please do you want the newspaper deliver to a specific address?")]
        public bool DoYouWhatItDelivered { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Please do you want the newspaper deliver to an Embassy")]
        public bool DoYouWhatItDeliveredAtAnEmbassy { get; set; }
        [Required]

        [Display(Name = "Please do you want the newspaper deliver to be delivered outside the Nigeria")]
        //public bool DoYouWhatItDeliveredAtAnEmbassy { get; set; }
        public string DeliveryAddress { get; set; }


        public string HowGettoKnowUs { get; set; }

        [Required]
        [Display(Name = "Help us to get better, please drop a comment on our services")]
        public string AcommentOnOurService { get; set; }
       
    }
}