using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PizzeriaInForno.Models;

namespace PizzeriaInForno.Controllers
{
    
    public class UtenteController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        [Authorize]
        // GET: Utente
        public ActionResult Index()
        {
            return View(db.Utente.ToList());
        }
        [Authorize]
        // GET: Utente/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Utente u)
        {
            if (u == null)
            {
                return HttpNotFound();
            }
            Utente LoginCorretto = db.Utente.FirstOrDefault( (db) => db.Username == u.Username && db.Psw == u.Psw);
            

            if (LoginCorretto != null)
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                //HttpCookie httpCookie = new HttpCookie("ID_Utente", u.ID_Utente.ToString());
                //httpCookie.Value = u.ID_Utente.ToString();
                //Response.Cookies.Add(httpCookie);

            }
            return RedirectToAction("Index","Home");
        }
        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Response.Cookies["ID_Utente"].Expires = DateTime.Now.AddDays(-1);
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        // GET: Utente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Utente/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Utente,Username,Psw,isAdmin,Nome,Cognome,Indirizzo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Utente.Add(utente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utente);
        }

        [Authorize]
        // GET: Utente/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utente/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Utente,Username,Psw,isAdmin,Nome,Cognome,Indirizzo")] Utente utente)
        {
            if (ModelState.IsValid)
            {
                db.Entry(utente).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(utente);
        }

        // GET: Utente/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Utente utente = db.Utente.Find(id);
            if (utente == null)
            {
                return HttpNotFound();
            }
            return View(utente);
        }

        // POST: Utente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(int id)
        {
            Utente utente = db.Utente.Find(id);
            db.Utente.Remove(utente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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
