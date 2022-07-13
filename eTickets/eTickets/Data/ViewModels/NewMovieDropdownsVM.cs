using eTickets.Models;
using System.Collections.Generic;

namespace eTickets.Data.ViewModels
{
    public class NewMovieDropdownsVM
    {
        public NewMovieDropdownsVM()
        {
            Producer = new List<Producer>();
            Cinema = new List<Cinema>();
            Actor = new List<Actor>();
        }
        public List<Producer> Producer { get; set; }
        public List<Cinema> Cinema { get; set; }
        public List<Actor> Actor { get; set; }

    }
}
