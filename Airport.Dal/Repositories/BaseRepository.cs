using Airport.Dal.Api;
using Airport.Utilities;
using Airport.Utilities.Api;
using Airport.Utilities.Implementations;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Airport.Dal
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly AirportContext context;
        private readonly IErrorLogger logger;
        private DbSet<T> dbSet;
        private static object locker = new object();

        public BaseRepository(AirportContext context, IErrorLogger logger)
        {
            this.context = context;
            this.logger = logger;
            this.dbSet = context.Set<T>();
            

        }

        public bool Add(T entity)
        {

            lock (locker)
            {
                try
                {

                    dbSet.Add(entity);
                    context.SaveChanges();
                    return true;

                }
                catch (Exception ex)
                {
                    logger.Log($"Error occured while adding {typeof(T)} to Db.\n\n{ex}");

                    return false;
                }
            }
        }

        public IEnumerable<T> GetAll() => dbSet;

        public T GetById(int id) => dbSet.Find(id);
    }
}
