using Registration.DataAccess.Data;
using Registration.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork 
    {

        private ApplicationDbContext _db;
        public ICourseRepository Course { get; private set; }
        public UnitOfWork(ApplicationDbContext db) 
        {
            _db = db;
            Course = new CourseRepository(db);
        }
        

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
