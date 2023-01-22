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
    public class HomeController : Controller
    {
        private ModelDBContext db = new ModelDBContext();

        // GET: Home
        public ActionResult Index()
        {
           
                return View(db.Pietanza.ToList());
            
        }

        [HttpPost]
        public ActionResult AggiungiAlCarrello( int? id, int QuantitaAggiuntaAlCarrello)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pietanza pietanza = db.Pietanza.Find(id);

            DettaglioOrdine dettaglio = new DettaglioOrdine();
            dettaglio.Quantita = QuantitaAggiuntaAlCarrello;
            dettaglio.ID_Pietanza = pietanza.ID_Pietanza;
            dettaglio.Nome = pietanza.Nome;

            DettaglioOrdine.ListaDettagliOrdini.Add(dettaglio);
            
            //DettaglioOrdine.ListaDettagliOrdini.ForEach(item=>item.ID_Ordine = ID_Ordine)

            if (pietanza == null)
            {
                return HttpNotFound();
            }
            return RedirectToAction("Index");
        }

        // GET: Home/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pietanza pietanza = db.Pietanza.Find(id);
            if (pietanza == null)
            {
                return HttpNotFound();
            }
            return View(pietanza);
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Pietanza,Nome,Prezzo,MinutiConsegna,Ingredienti,Foto, FileFoto")] Pietanza pietanza)
        {
            if (ModelState.IsValid)
            {
                string path = Server.MapPath("../Content/img/" + pietanza.FileFoto.FileName);
                pietanza.FileFoto.SaveAs(path);
                pietanza.Foto = pietanza.FileFoto.FileName;
                db.Pietanza.Add(pietanza);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pietanza);
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pietanza pietanza = db.Pietanza.Find(id);
            if (pietanza == null)
            {
                return HttpNotFound();
            }
            return View(pietanza);
        }

        // POST: Home/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Pietanza,Nome,Prezzo,MinutiConsegna,Ingredienti,Foto")] Pietanza pietanza)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pietanza).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pietanza);
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pietanza pietanza = db.Pietanza.Find(id);
            if (pietanza == null)
            {
                return HttpNotFound();
            }
            return View(pietanza);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pietanza pietanza = db.Pietanza.Find(id);
            db.Pietanza.Remove(pietanza);
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
