using Registration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registration.DataAccess.Repository.IRepository
{
    public interface ICandidateRepository : IRepository<Candidates>
    {
        void Update(Candidates obj);
        
    }

}
