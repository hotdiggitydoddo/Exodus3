using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exodus3.Domain;

namespace Exodus3.Services
{
    public class MockDataStore : IDataStore<Sermon>
    {
        List<Sermon> _sermons;

        public MockDataStore()
        {
            _sermons = new List<Sermon>
            {
                new Sermon { Id = 1, Name = "The Grass is Greener"},
                new Sermon { Id = 2, Name = "Pie in the Sky"},
                new Sermon { Id = 3, Name = "For God's Glory"}
            };

        }

        public Task<Sermon> Add(Sermon entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Sermon entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sermon>> Find(Expression<Func<Sermon, bool>> where, params Expression<Func<Sermon, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Sermon>> Get(params Expression<Func<Sermon, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<Sermon> GetById(int id, params Expression<Func<Sermon, object>>[] includes)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Sermon entity)
        {
            throw new NotImplementedException();
        }
    }
}
