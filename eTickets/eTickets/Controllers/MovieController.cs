using eTickets.Data;
using eTickets.Data.Services;
using eTickets.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class MovieController : Controller
    {
       // private readonly AppDbContext _context;

        private readonly IMovieService _service;

        //public MovieController(AppDbContext context)
        //{
        //    _context = context;  
        //}
        public MovieController(IMovieService service)
        {
            _service = service;
        }


        public async Task<IActionResult> Index()
        {
            // var allMovies = await _context.Movie.Include(n => n.Cinema).OrderBy(n => n.Name).ToListAsync();
            var allMovies = await _service.GetAllAsync(n => n.Cinema);
            return View(allMovies);
        }

        public async Task<IActionResult> Filter(string searchString)
        {
            var allMovies = await _service.GetAllAsync(n => n.Cinema);

            if (!string.IsNullOrEmpty(searchString))
            {
               // var ser = searchString.ToUpper();
                var filteredResult = allMovies.Where(n => n.Name.Contains(searchString) || 
                  n.Description.Contains(searchString)).ToList();
                return View("Index",filteredResult);
            }
            return View("Index",allMovies);
        }

        //Get: Movie/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            return View(movieDetails);
        }

        //Get: Movie/Create
        public async Task<IActionResult> Create()
        {
            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinema = new SelectList(movieDropdownsData.Cinema, "Id", "Name");
            ViewBag.Producer = new SelectList(movieDropdownsData.Producer, "Id", "FullName");
            ViewBag.Actor = new SelectList(movieDropdownsData.Actor, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewMovieVM movie)
        {
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinema = new SelectList(movieDropdownsData.Cinema, "Id", "Name");
                ViewBag.Producer = new SelectList(movieDropdownsData.Producer, "Id", "FullName");
                ViewBag.Actor = new SelectList(movieDropdownsData.Actor, "Id", "FullName");

                return View(movie);
            }
            await _service.AddNewMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

        //Get: Movie/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var movieDetails = await _service.GetMovieByIdAsync(id);
            if(movieDetails == null) { return View("Not Found"); }

            var response = new NewMovieVM()
            {
                Id = movieDetails.Id,
                Name = movieDetails.Name,
                Description = movieDetails.Description,
                Price = movieDetails.Price,
                StartDate = movieDetails.StartDate,
                EndDate = movieDetails.EndDate,
                ImageURL = movieDetails.ImageURL,
                MovieCategory = movieDetails.MovieCategory,
                CinemaId = movieDetails.CinemaId,
                ProducerId = movieDetails.ProducerId,
                ActorIds = movieDetails.Actor_Movie.Select(n => n.ActorId).ToList(),
            };

            var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
            ViewBag.Cinema = new SelectList(movieDropdownsData.Cinema, "Id", "Name");
            ViewBag.Producer = new SelectList(movieDropdownsData.Producer, "Id", "FullName");
            ViewBag.Actor = new SelectList(movieDropdownsData.Actor, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id,NewMovieVM movie)
        {
            if (id != movie.Id) return View("Not Found");
            if (!ModelState.IsValid)
            {
                var movieDropdownsData = await _service.GetNewMovieDropdownsValues();
                ViewBag.Cinema = new SelectList(movieDropdownsData.Cinema, "Id", "Name");
                ViewBag.Producer = new SelectList(movieDropdownsData.Producer, "Id", "FullName");
                ViewBag.Actor = new SelectList(movieDropdownsData.Actor, "Id", "FullName");

                return View(movie);
            }
            await _service.UpdateMovieAsync(movie);
            return RedirectToAction(nameof(Index));
        }

    }
}
