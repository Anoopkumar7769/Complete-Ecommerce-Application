using eTickets.Data.Base;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class MovieService : EntityBaseRepository<Movie>, IMovieService
    {
        private readonly AppDbContext _context;
        public MovieService(AppDbContext context) : base(context)
        {
            _context= context;
        }
        public async Task AddNewMovieAsync(NewMovieVM data)
        {
            var newMovie = new Movie()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                CinemaId = data.CinemaId,
                StartDate = data.StartDate,
                EndDate = data.EndDate,
                MovieCategory = data.MovieCategory,
                ProducerId = data.ProducerId,

            };
            await _context.Movie.AddAsync(newMovie);
            await _context.SaveChangesAsync();

            //Add Movie Actors
            foreach(var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = newMovie.Id,
                    ActorId = actorId
                };
                await _context.Actor_Movie.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Movie> GetMovieByIdAsync(int id)
        {
            var movieDetails = await _context.Movie
               .Include(c => c.Cinema)
               .Include(p => p.Producer)
               .Include(am => am.Actor_Movie).ThenInclude(a => a.Actor) //Actor_Movie is joining entity between actors and movies
               .FirstOrDefaultAsync(n => n.Id == id);

            return movieDetails;
        }

        public async Task<NewMovieDropdownsVM> GetNewMovieDropdownsValues()
        {
            var response = new NewMovieDropdownsVM()
            {
                Actor = await _context.Actor.OrderBy(n => n.FullName).ToListAsync(),
                Cinema = await _context.Cinema.OrderBy(n => n.Name).ToListAsync(),
                Producer = await _context.Producer.OrderBy(n => n.FullName).ToListAsync()
            };
            return response;
        }

        public async Task UpdateMovieAsync(NewMovieVM data)
        {
            var dbMovie = await _context.Movie.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbMovie != null)
            {
                    dbMovie.Name = data.Name;
                    dbMovie.Description = data.Description;
                    dbMovie.Price = data.Price;
                    dbMovie.ImageURL = data.ImageURL;
                    dbMovie.CinemaId = data.CinemaId;
                    dbMovie.StartDate = data.StartDate;
                    dbMovie.EndDate = data.EndDate;
                    dbMovie.MovieCategory = data.MovieCategory;
                    dbMovie.ProducerId = data.ProducerId;
                    await _context.SaveChangesAsync();
            }

            //Remove existing actors
            var existingActorDb = _context.Actor_Movie.Where(n=>n.MovieId == data.Id).ToList();
            _context.Actor_Movie.RemoveRange(existingActorDb);
            await _context.SaveChangesAsync();  

            //Add Movie Actors
            foreach (var actorId in data.ActorIds)
            {
                var newActorMovie = new Actor_Movie()
                {
                    MovieId = data.Id,
                    ActorId = actorId
                };
                await _context.Actor_Movie.AddAsync(newActorMovie);
            }
            await _context.SaveChangesAsync();
        }
    }
}
