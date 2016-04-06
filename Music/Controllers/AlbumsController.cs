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
    public class AlbumsController : Controller
    {
        private MusicContext db = new MusicContext();

        // GET: Albums
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.TitleSortParm = String.IsNullOrEmpty(sortOrder) ? "TitleDesc" : "";
            ViewBag.ArtistSortParm = sortOrder == "Artist" ? "ArtistDesc" : "Artist";
            ViewBag.GenreSortParm = sortOrder == "Genre" ? "GenreDesc" : "Genre";
            ViewBag.PriceSortParm = sortOrder == "Price" ? "PriceDesc" : "Price";
            ViewBag.LikesSortParm = sortOrder == "Likes" ? "LikesDesc" : "Likes";

            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
            if (!String.IsNullOrEmpty(searchString))
            {
                albums = albums.Where(s => s.Title.Contains(searchString) || s.Artist.Name.Contains(searchString)
                || s.Genre.Name.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "TitleDesc":
                    albums = albums.OrderByDescending(s => s.Title);
                    break;
                case "Title":
                    albums = albums.OrderBy(s => s.Title);
                    break;
                case "Artist":
                    albums = albums.OrderBy(s => s.Artist.Name);
                    break;
                case "ArtistDesc":
                    albums = albums.OrderByDescending(s => s.Artist.Name);
                    break;
                case "Genre":
                    albums = albums.OrderBy(s => s.Genre.Name);
                    break;
                case "GenreDesc":
                    albums = albums.OrderByDescending(s => s.Genre.Name);
                    break;
                case "Price":
                    albums = albums.OrderBy(s => s.Price);
                    break;
                case "PriceDesc":
                    albums = albums.OrderByDescending(s => s.Price);
                    break;
                case "Likes":
                    albums = albums.OrderBy(s => s.Likes);
                    break;
                case "LikesDesc":
                    albums = albums.OrderByDescending(s => s.Likes);
                    break;
                default:
                    albums = albums.OrderBy(s => s.Title);
                    break;
            }
            return View(albums.ToList());
        }

        public ActionResult IndexLiked(int? id)
        {
            if (ModelState.IsValid)
            {
                Album album = db.Albums.Include(a => a.Artist).Include(a => a.Genre).Where(a => a.AlbumID == id).Single();
                db.Entry(album).State = EntityState.Modified;
                album.Likes++;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var albums = db.Albums.Include(a => a.Artist).Include(a => a.Genre);
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
            ViewBag.Reccomendations = db.Albums.Where(a => a.Artist.ArtistID == album.ArtistID
            || a.Genre.GenreID == album.GenreID).ToList();
            return View(album);
        }

        // GET: Albums/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name");
            return View();
        }

        // POST: Albums/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AlbumID,Title,GenreID,Price,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Albums.Add(album);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AlbumID,Title,GenreID,Price,Likes,ArtistID")] Album album)
        {
            if (ModelState.IsValid)
            {
                db.Entry(album).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.Artists, "ArtistID", "Name", album.ArtistID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "Name", album.GenreID);
            return View(album);
        }

        // GET: Albums/Edit/5
        public ActionResult Add2Playlist(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaylistID = new SelectList(db.Playlists, "PlaylistID", "Name", album.Playlist);
            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumID", "Title", album.AlbumID);
            NewModel viewModel = new NewModel() {
                AlbumID = album.AlbumID,
                Playlist = ViewBag.PlayListID,
                Title = album.Title
            };
            return View(viewModel);
        }

        // POST: Albums/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add2Playlist(NewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Album album = db.Albums.Find(viewModel.AlbumID);
                Playlist pList = db.Playlists.Find(viewModel.PlayListId);
                if (pList.Albums == null)
                {
                    pList.Albums = new List<Album>();
                }
                pList.Albums.Add(album);
                db.Entry(pList).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaylistID = new SelectList(db.Playlists, "PlaylistID", "Name", viewModel.Playlist);
            ViewBag.AlbumID = new SelectList(db.Albums, "AlbumID", "Title", viewModel.AlbumID);
            return View(viewModel);
        }

        // GET: Albums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Album album = db.Albums.Find(id);
            if (album == null)
            {
                return HttpNotFound();
            }
            return View(album);
        }

        // POST: Albums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Album album = db.Albums.Find(id);
            db.Albums.Remove(album);
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
