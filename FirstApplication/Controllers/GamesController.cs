using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FirstApplication.Models;

namespace FirstApplication.Controllers
{
    public class GamesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Games
        public ActionResult Index()
        {
            return View(db.Games.ToList());
        }

        // GET: Games/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // GET: Games/Create
        public ActionResult Create()
        {
            Game model = new Game() { Name = "Game - " + DateTime.Now.Ticks };

            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x => x.GenreId).ToArray());

            //if (ViewBag.GenreId == null)
            //{
            //    ViewBag.GenreCount = 0;
            //}
            return View(model);
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,IsMultiplayer,GenreIds")] Game model, String[] GenreIds)
        {
            if (ModelState.IsValid)
            {

                Game checkmodel = db.Games.SingleOrDefault(x => x.Name == model.Name && x.IsMultiplayer == model.IsMultiplayer);

                if (checkmodel == null)
                {

                    //model.GameId = Guid.NewGuid().ToString();
                    //model.CreateDate = DateTime.Now;
                    //model.EditDate = model.CreateDate;
                    db.Games.Add(model);
                    db.SaveChanges();

                    if (GenreIds != null)
                    {
                        foreach (string genreid in GenreIds)
                        {
                            GameGenre gamegenre = new GameGenre();

                            //gamegenre.GameGenreId = Guid.NewGuid().ToString();
                            //gamegenre.CreateDate = DateTime.Now;
                            //gamegenre.EditDate = gamegenre.CreateDate;

                            gamegenre.GameId = model.GameId;
                            gamegenre.GenreId = genreid;

                            
                            model.Genres.Add(gamegenre);
                        }
                        db.Entry(model).State = EntityState.Modified;
                        db.SaveChanges();
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Duplicate Game Detected");
                }
            }

            //List<Genre> gameGenres = db.Genres.ToList();
            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);

            //SelectList genres = new SelectList(db.Genres, "GenreId", "Name", GenreIds);
            //for (int i = 0; i < genres.Count(); i++)
            //{
            //    SelectListItem genre = genres.ElementAt(i);
            //    if (GenreIds.Contains(genre.Value))
            //    {
            //        genres.ElementAt(i).Selected = true;
            //    }
            //}


            //foreach (var genre in genres)
            //{
            //    if (GenreId.Contains(genre.Value))
            //    {
            //        genre.Selected = true;
            //    }
            //}

            //ViewBag.GenreId = new MultiSelectList(genres, "GenreId", "Name", GenreId);
            //ViewBag.GenreId = genres;
            return View(model);
        }

        // GET: Games/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game model = db.Games.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }

            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", model.Genres.Select(x => x.GenreId).ToArray());

            return View(model);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "GameId,Name,IsMultiplayer,GenreIds")] Game model, string[] GenreIds)
        {
            if (ModelState.IsValid)
            {
                Game tmpmodel = db.Games.Find(model.GameId);
                if (tmpmodel != null)
                {
                    Game checkmodel = db.Games.SingleOrDefault(x => x.Name == model.Name && x.IsMultiplayer == model.IsMultiplayer && x.GameId != model.GameId);

                    if (checkmodel == null)
                    {

                        tmpmodel.Name = model.Name;
                        tmpmodel.EditDate = DateTime.Now;
                        tmpmodel.IsMultiplayer = model.IsMultiplayer;
                        // tmpgame.GenreId = game.GenreId;

                        db.Entry(tmpmodel).State = EntityState.Modified;

                        //Items to remove
                        var removeItems = tmpmodel.Genres.Where(x => !GenreIds.Contains(x.GenreId)).ToList();

                        foreach (var removeItem in removeItems)
                        {
                            db.Entry(removeItem).State = EntityState.Deleted;
                        }

                        if (GenreIds != null)
                        {
                            var addedItems = GenreIds.Where(x => !tmpmodel.Genres.Select(y => y.GenreId).Contains(x));

                            //Items to add
                            foreach (string addedItem in addedItems)
                            {
                                GameGenre gamegenre = new GameGenre();
                                gamegenre.GameGenreId = Guid.NewGuid().ToString();
                                gamegenre.GameId = tmpmodel.GameId;
                                gamegenre.GenreId = addedItem;

                                gamegenre.CreateDate = DateTime.Now;
                                gamegenre.EditDate = gamegenre.CreateDate;
                                db.GameGenres.Add(gamegenre);
                            }
                        }
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Duplicate Game Detected");
                    }
                }

            }

            ViewBag.Genres = new MultiSelectList(db.Genres.ToList(), "GenreId", "Name", GenreIds);
            return View(model);
        }

        // GET: Games/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Game game = db.Games.Find(id);
            if (game == null)
            {
                return HttpNotFound();
            }
            return View(game);
        }

        // POST: Games/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Game game = db.Games.Find(id);
            foreach (var gameGenre in game.Genres.ToList())
            {
                db.GameGenres.Remove(gameGenre);
            }
            db.Games.Remove(game);
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
