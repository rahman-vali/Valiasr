using System;
using System.Collections.Generic;
using System.Linq;

namespace Valiasr.DataAccess.Repositories
{
    using System.Data.Entity;
    using System.Linq.Expressions;

    using Valiasr.Domain.Model;
    using Valiasr.Domain.Repositories;

    public abstract class Repository<T> : IRepository<T> where T : class, IAggregateRoot
    {
        public Repository()
        {
            this.ActiveContext = new ValiasrContext("Valiasr");
        }

        //[Dependency]
        public ValiasrContext ActiveContext { get; private set; }

        private DbSet<T> set;

        public DbSet<T> Set
        {
            get
            {
                if (set == null)
                {
                    set = this.ActiveContext.Set<T>();
                }
                return set;
            }
        }

        public void Add(T item)
        {
            this.Set.Add(item);
            this.ActiveContext.SaveChanges();
        }

        public void Remove(T item)
        {            
            this.Set.Remove(item);
            this.ActiveContext.SaveChanges();
        }

        public void Update(T item)
        {
            this.ActiveContext.SaveChanges();
        }

        public IQueryable<T> Include(Expression<Func<T, object>> subSelector)
        {
            return this.Set.Include(subSelector);
        }

        #region Implementation of IEnumerable

        public IEnumerator<T> GetEnumerator()
        {
            return this.Set.AsQueryable().AsEnumerable().GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of IQueryable

        public Type ElementType
        {
            get
            {
                return this.Set.AsQueryable().ElementType;
            }
        }

        public Expression Expression
        {
            get
            {
                return this.Set.AsQueryable().Expression;
            }
        }

        public IQueryProvider Provider
        {
            get
            {
                return this.Set.AsQueryable().Provider;
            }
        }

        #endregion
    }
}

