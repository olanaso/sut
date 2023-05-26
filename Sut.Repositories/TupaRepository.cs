using Sut.Entities;
using Sut.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sut.Repositories
{
    public class TupaRepository : BaseRepository<Tupa>, ITupaRepository
    {
        public TupaRepository(IContext context) : base(context) { }


    }
}
