using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzeriaInForno.Models;

namespace PizzeriaInForno.Controllers
{
    [Authorize]
    public class DettaglioOrdineController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: DettaglioOrdines
        public ActionResult Index()
        {
            return View(DettaglioOrdine.ListaDettagliOrdini);
        }

        // GET: DettaglioOrdines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettaglioOrdine dettaglioOrdine = db.DettaglioOrdine.Find(id);
            if (dettaglioOrdine == null)
            {
                return HttpNotFound();
            }
            return View(dettaglioOrdine);
        }

        [HttpPost]
        public ActionResult ConfermaOrdine()
        {
            Ordine o = new Ordine();
            

            Utente utente = db.Utente.Where(x => x.Username == User.Identity.Name).First();
            o.ID_Utente = utente.ID_Utente;

            o.ID_Pietanza = 0;
            o.Evaso = "false";
            o.Confermato = DateTime.Now;

            db.Ordine.Add(o);
            db.SaveChanges();
            foreach(var item in DettaglioOrdine.ListaDettagliOrdini)
            {
                DettaglioOrdine dettOrd = new DettaglioOrdine();
                dettOrd.ID_Pietanza = item.ID_Pietanza;
                dettOrd.Quantita = item.Quantita;
                dettOrd.ID_Ordine = o.ID_Ordine;
                db.DettaglioOrdine.Add(dettOrd);
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }

        // GET: DettaglioOrdines/Create
        public ActionResult Create()
        {
            ViewBag.ID_Ordine = new SelectList(db.Ordine, "ID_Ordine", "Evaso");
            ViewBag.ID_Pietanza = new SelectList(db.Pietanza, "ID_Pietanza", "Nome");
            return View();
        }

        // POST: DettaglioOrdines/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_DettaglioOrdine,ID_Pietanza,Quantita,ID_Ordine")] DettaglioOrdine dettaglioOrdine)
        {
            if (ModelState.IsValid)
            {
                db.DettaglioOrdine.Add(dettaglioOrdine);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Ordine = new SelectList(db.Ordine, "ID_Ordine", "Evaso", dettaglioOrdine.ID_Ordine);
            ViewBag.ID_Pietanza = new SelectList(db.Pietanza, "ID_Pietanza", "Nome", dettaglioOrdine.ID_Pietanza);
            return View(dettaglioOrdine);
        }

        // GET: DettaglioOrdines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DettaglioOrdine dettaglioOrdine = db.DettaglioOrdine.Find(id);
            if (dettaglioOrdine == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Ordine = new SelectList(db.Ordine, "ID_Ordine", "Evaso", dettaglioOrdine.ID_Ordine);
            ViewBag.ID_Pietanza = new SelectList(db.Pietanza, "ID_Pietanza", "Nome", dettaglioOrdine.ID_Pietanza);
            return View(dettaglioOrdine);
        }

        // POST: DettaglioOrdines/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_DettaglioOrdine,ID_Pietanza,Quantita,ID_Ordine")] DettaglioOrdine dettaglioOrdine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dettaglioOrdine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Ordine = new SelectList(db.Ordine, "ID_Ordine", "Evaso", dettaglioOrdine.ID_Ordine);
            ViewBag.ID_Pietanza = new SelectList(db.Pietanza, "ID_Pietanza", "Nome", dettaglioOrdine.ID_Pietanza);
            return View(dettaglioOrdine);
        }

        // GET: DettaglioOrdines/Delete/5
        public ActionResult Delete(int id)
        {
            
            foreach(var item in DettaglioOrdine.ListaDettagliOrdini)
            {
                if (item.ID_Pietanza == id)
                {
                    DettaglioOrdine.ListaDettagliOrdini.Remove(item);
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        // POST: DettaglioOrdines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DettaglioOrdine dettaglioOrdine = db.DettaglioOrdine.Find(id);
            db.DettaglioOrdine.Remove(dettaglioOrdine);
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
