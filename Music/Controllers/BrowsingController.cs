using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Music.Models;

namespace Music.Controllers
{
    public class BrowsingController : Controller
    {
        private MusicContext db = new MusicContext();

        // GET: Albums
        public ActionResult genre()
        {
            var albums = db.Genres;
            return View(albums.ToList());
        }

        public ActionResult artist()
        {
            var albums = db.Artists;
            return View(albums.ToList());
        }

        // GET: Albums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).Single();
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            //ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "GenreID,Name")] Genre genre)
        {
            if (ModelState.IsValid)
            {
                db.Genres.Add(genre);
                db.SaveChanges();
                return RedirectToAction("genre");
            }

            //ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", genre.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", genre.GenreID);
            return View(genre);
        }

        // GET: Albums/Create
        public ActionResult Create2()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "ArtistID,Name")] Artist artist)
        {
            if (ModelState.IsValid)
            {
                db.Artists.Add(artist);
                db.SaveChanges();
                return RedirectToAction("artist");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", artist.ArtistID);
            return View(artist);
        }

        public ActionResult ShowSomeAlbums(int id)
        {
            var albums = db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.GenreID == id);
            return View(albums.ToList());
        }

        public ActionResult ShowSomeAlbums2(int id)
        {
            var albums = db.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .Where(a => a.ArtistID == id);
            return View(albums.ToList());
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
