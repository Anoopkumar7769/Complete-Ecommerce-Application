using eTickets.Data.Base;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class ActorService : EntityBaseRepository<Actor>, IActorService
    {
        private readonly AppDbContext _context;

        public ActorService(AppDbContext context) : base(context) { }
        //{
        //    _context = context;
        //}

        //public async Task AddAsync(Actor actor)
        //{
        //    await _context.Actor.AddAsync(actor);
        //    await _context.SaveChangesAsync();
        //}

      

        //public async Task<IEnumerable<Actor>> GetAllAsync()
        //{
        //    var result = await _context.Actor.ToListAsync();
        //    return result;
        //}

        //public async Task<Actor> GetByIdAsync(int id)
        //{
        //   var result = await _context.Actor.FirstOrDefaultAsync(n => n.Id == id);
        //   return result;
        //}

        //public async Task<Actor> UpdateAsync(int id, Actor newActor)
        //{
        //    _context.Update(newActor);
        //    await _context.SaveChangesAsync();
        //    return newActor;
        //}

        //public async Task DeleteAsync(int id)
        //{
        //    var result = await _context.Actor.FirstOrDefaultAsync(n => n.Id == id);
        //    _context.Actor.Remove(result);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task<Actor> UpdateAsync(int id, Actor newActor)
        //{
        //    newActor.ActorId = id;
        //   _context.Update(newActor);
        //    await _context.SaveChangesAsync();
        //    return newActor;
        //}
    }
}
