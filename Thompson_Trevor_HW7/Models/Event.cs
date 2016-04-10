//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: class for the events
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Thompson_Trevor_HW7.Models
{
    public class Event
    {

        
        public Int32 EventID { get; set; }

        [Required(ErrorMessage = "Event Title is required")]
        public String Title { get; set; }

        [Display(Name = "Event Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EventDate { get; set; }

        [Display(Name = "Event Location")]
        public String Location { get; set; }

        [Display(Name = "Members Only?")]
        public Boolean MembersOnly { get; set; }




        //navigation properties
        [Display(Name = "Sponsoring Committee")]
        public virtual Committee SponsoringCommittee { get; set; }


        [Display(Name = "Members")]
        public virtual List<Member> Members { get; set; }

       
    }
}