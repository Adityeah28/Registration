using Registration.DataAccess.Data;
using Registration.DataAccess.Repository.IRepository;
using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataAccess.Repository
{
  public class CandidateRepository : Repository<Candidates>, ICandidateRepository
    {
        private ApplicationDbContext _db;
        public CandidateRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Candidates obj)
        {
            _db.Candidates.Update(obj);
        }
    }
}
