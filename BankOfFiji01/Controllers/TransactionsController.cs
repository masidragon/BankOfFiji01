using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankOfFiji01.Models;

namespace BankOfFiji01.Controllers
{
    public class TransactionsController : Controller
    {
        private BankOfFijiEntities db = new BankOfFijiEntities();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.Bank_Account).Include(t => t.Transaction_Type);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID");
            ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Trans_ID,Trans_Description,Trans_Date,Trans_Amount,Trans_Adjustment,Acc_ID,Transac_Type_ID")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transactions);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID", transactions.Acc_ID);
            ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript", transactions.Transac_Type_ID);
            return View(transactions);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID", transactions.Acc_ID);
            ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript", transactions.Transac_Type_ID);
            return View(transactions);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Trans_ID,Trans_Description,Trans_Date,Trans_Amount,Trans_Adjustment,Acc_ID,Transac_Type_ID")] Transactions transactions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transactions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Acc_ID = new SelectList(db.Bank_Account, "Acc_ID", "Acc_ID", transactions.Acc_ID);
            ViewBag.Transac_Type_ID = new SelectList(db.Transaction_Type, "Transac_Type_ID", "Transac_Type_Descript", transactions.Transac_Type_ID);
            return View(transactions);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transactions transactions = db.Transactions.Find(id);
            if (transactions == null)
            {
                return HttpNotFound();
            }
            return View(transactions);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Transactions transactions = db.Transactions.Find(id);
            db.Transactions.Remove(transactions);
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
