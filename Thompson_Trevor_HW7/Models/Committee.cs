//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: class for the committee
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Thompson_Trevor_HW7.Models
{
    public class Committee
    {

        [Display(Name = "CommitteeID")]
        public Int32 CommitteeID { get; set; }

        [Display(Name = "Committee Name")]
        public String Name { get; set; }


         //navigation properties
        [Display(Name = "Events")]
        public virtual List<Event> Events { get; set; }
    }
}