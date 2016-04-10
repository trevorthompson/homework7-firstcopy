//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: controller for the events

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Thompson_Trevor_HW7.Models;


namespace Thompson_Trevor_HW7.Controllers
{
    public class EventsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Events/
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: /Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: /Events/Create
        public ActionResult Create()
        {
            //create query to find all committees
            var query = from c in db.Committees
                        orderby c.Name
                        select c;
            //execute query and store in list
            List<Committee> allCommittees = query.ToList();

            //convert list to select list format needed for HTML
            SelectList allCommitteeslist = new SelectList(allCommittees, "CommitteeID", "Name");

            ViewBag.AllCommittees = allCommitteeslist;
            
            return View();
        }

        // POST: /Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="EventID,Title,EventDate,Location,MembersOnly")] Event @event, Int32 CommitteeID)
        {
            //find selected committee
            Committee SelectedCommittee = db.Committees.Find(CommitteeID);

            //associate committee with event
            @event.SponsoringCommittee = SelectedCommittee;

            
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: /Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            //populate list of committees
            var query = from c in db.Committees
                        orderby c.Name
                        select c;

            //create lsit and execute query
            List<Committee> allCommittees = query.ToList();

            //convert to select list
            SelectList list = new SelectList(allCommittees, "CommitteeID", "Name", @event.SponsoringCommittee.CommitteeID);

            //add to viewbag
            ViewBag.AllCommittees = list;
            
            //find the list of members
            var query2 = from m in db.Members
                         orderby m.Email
                         select m;
            //convert to list and execute query
            List<Member> allMembers = query2.ToList();

            //create list of selected members
            List<Int32> SelectedMembers = new List<Int32>();

            //Loop through list of members and add MemberID
            foreach(Member m in @event.Members)
            {
                SelectedMembers.Add(m.MemberID);
            }

            //convert to multiselect
            MultiSelectList allMembersList = new MultiSelectList(allMembers, "MemberID", "Email", SelectedMembers);

            //ad to viewbag
            ViewBag.AllMembers = allMembersList;


            return View(@event);
        }

        // POST: /Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="EventID,Title,EventDate,Location,MembersOnly")] Event @event, Int32 CommitteeID, int[] SelectedMembers)
        {

            if (ModelState.IsValid)
            {
                //find associated event
                Event eventToChange = db.Events.Find(@event.EventID);

                //change committee if necessary
                if (eventToChange.SponsoringCommittee.CommitteeID != CommitteeID)
                {
                    //find commitee
                    Committee SelectedCommittee = db.Committees.Find(CommitteeID);

                    //update the committee
                    eventToChange.SponsoringCommittee = SelectedCommittee;


                }

                //change members
                //remove any existing members
                eventToChange.Members.Clear();

                //if there are members to add, add them
                if (SelectedMembers != null)
                {
                    foreach (int memberID in SelectedMembers)
                    {
                        Member memberToAdd = db.Members.Find(memberID);
                        eventToChange.Members.Add(memberToAdd);
                    }
                }

                //update the rest of the fields
                eventToChange.Title = @event.Title;
                eventToChange.EventDate = @event.EventDate;
                eventToChange.Location = @event.Location;
                eventToChange.MembersOnly = @event.MembersOnly;


                db.Entry(eventToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: /Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: /Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
