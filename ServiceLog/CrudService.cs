using InterseptorSample.data;
using InterseptorSample.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InterseptorSample.ServiceLog
{
    public class CrudService
    {
        private readonly Mydbcontext _context;

        public CrudService(Mydbcontext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Cars Create(Cars entity)
        {
            _context.Cars.Add(entity);
            _context.SaveChanges(); 
            return entity;
        }
        public bool Update(Cars entity)
        {
            _context.Cars.Update(entity);
            var res = _context.SaveChanges();
            return res > 0;
        }

        public bool Delete(Cars entity)
        {
            _context.Cars.Remove(entity);
            var res = _context.SaveChanges();
            return res > 0;
        }
    }
}
