﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Exodus3.Domain;

namespace Exodus3.Core
{
    public interface IDataStore<T> where T : E3Entity
    {
        Task<IEnumerable<T>> Get(params Expression<Func<T, object>>[] includes);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task<T> GetById(int id, params Expression<Func<T, object>>[] includes);
        Task<T> Add(T entity);
        Task<bool> Update(T entity);
        Task Delete(T entity);
    }
}