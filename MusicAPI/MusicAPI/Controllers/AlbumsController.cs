using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MusicAPI.Models;

namespace MusicAPI.Controllers
{
    public class AlbumsController : ApiController
    {
        private MusicAPIContext db = new MusicAPIContext();

        // GET: api/Albums
        public IQueryable<AlbumDTO> GetAlbums()
        {
            var albums = from a in db.Albums
                         select new AlbumDTO()
                         {
                             Id = a.Id,
                             Album = a.Title,
                             ArtistName = a.Artist.Name
                         };

            return albums;
        }

        // GET: api/Albums/5
        [ResponseType(typeof(AlbumDetailsDTO))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var album = await db.Albums.Include(b => b.Artist).Select(b =>
                new AlbumDetailsDTO()
                {
                    Id = b.Id,
                    Title = b.Title,
                    Year = b.Year,
                    Price = b.Price,
                    ArtistName = b.Artist.Name,
                    Genre = b.Genre
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }
        // PUT: api/Albums/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAlbum(int id, Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != album.Id)
            {
                return BadRequest();
            }

            db.Entry(album).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Albums
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> PostAlbum(Album album)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Albums.Add(album);
            await db.SaveChangesAsync();

            db.Entry(album).Reference(x => x.Artist).Load();

            var dto = new AlbumDTO()
            {
                Id = album.Id,
                Album = album.Title,
                ArtistName = album.Artist.Name
            };

            return CreatedAtRoute("DefaultApi", new { id = album.Id }, dto);
        }

        // DELETE: api/Albums/5
        [ResponseType(typeof(Album))]
        public async Task<IHttpActionResult> DeleteAlbum(int id)
        {
            Album album = await db.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }

            db.Albums.Remove(album);
            await db.SaveChangesAsync();

            return Ok(album);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlbumExists(int id)
        {
            return db.Albums.Count(e => e.Id == id) > 0;
        }
    }
}