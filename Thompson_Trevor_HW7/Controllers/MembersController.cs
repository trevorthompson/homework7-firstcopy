//Name:Trevor Thompson
//Date: 3/22/16
//Assignment: homework 5 member tracker
//Description: controller for the members
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
    public class MembersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Members/
        public ActionResult Index()
        {
            return View(db.Members.ToList());
        }

        // GET: /Members/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: /Members/Create
        public ActionResult Create()
        {
  

            

            
            return View();
        }

        // POST: /Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="MemberID,FirstName,LastName,Email,PhoneNumber,OKToText,major")] Member member)
        {




            if (ModelState.IsValid)
            {
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(member);
        }

        // GET: /Members/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }

            //populate list of events
            var query3 = from e in db.Events
                         orderby e.Title
                         select e;

            //create list and execute query
            List<Event> allEvents = query3.ToList();

            //create list of selected events
            List<Int32> SelectedEvents = new List<Int32>();

            //Loop through list of events and add EventID
            foreach(Event e in member.AttendedEvents)
            {
                SelectedEvents.Add(e.EventID);
            }

            //convert to multiselect
            MultiSelectList allEventsList = new MultiSelectList(allEvents, "EventID", "Title", SelectedEvents);

            //add to viewbag
            ViewBag.AllEvents = allEventsList;


            return View(member);
        }

        // POST: /Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="MemberID,FirstName,LastName,Email,PhoneNumber,OKToText,major")] Member member, int[] SelectedEvents)
        {


            if (ModelState.IsValid)
            {
                //find associated member
                Member memberToChange = db.Members.Find(member.MemberID);

                //remove any existing events
                memberToChange.AttendedEvents.Clear();

                //if there are events to add add them
                if (SelectedEvents != null)
                {
                    foreach (int eventID in SelectedEvents)
                    {
                        Event eventToAdd = db.Events.Find(eventID);
                        memberToChange.AttendedEvents.Add(eventToAdd);

                    }

                }
                //update rest of the fields
                memberToChange.FirstName = member.FirstName;
                memberToChange.LastName = member.LastName;
                memberToChange.Email = member.Email;
                memberToChange.PhoneNumber = member.PhoneNumber;
                memberToChange.major = member.major;
                memberToChange.OKToText = member.OKToText;


                db.Entry(memberToChange).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: /Members/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: /Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Member member = db.Members.Find(id);
            db.Members.Remove(member);
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
