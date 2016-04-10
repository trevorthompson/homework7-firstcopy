//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: class for the members
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace Thompson_Trevor_HW7.Models
{
    public class Member
    {

        [Display(Name = "Customer ID")]
        public Int32 MemberID { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address.")]


        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public String Email { get; set; }


        [Required(ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Please enter a valid number")]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:(###)###-####}", ApplyFormatInEditMode = true)]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please enter whether it is okay to text you or not")]
        [Display(Name = "Is it okay to text you?")]
        public Boolean OKToText { get; set; }


        public enum MajorList { Accounting, BusinessHonors, Finance, InternationalBusiness, Management, MIS, Marketing, SupplyChainManagement, STM }
        [Display(Name = "Major")]
        [Required(ErrorMessage = "Please select your major.")]
        [EnumDataType(typeof(MajorList))]
        public MajorList major { get; set; }


        //navigation properties
        [Display(Name = "Attended Events")]
        public virtual List<Event> AttendedEvents { get; set; }


     
    }
}