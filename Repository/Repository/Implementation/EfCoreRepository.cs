using Domain.Models.Base;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository.Implementation
{
    public class EfCoreRepository<T> : IRepository<T> where T : class, IEntity
    {

        private readonly LibraryDBContext Db;

        public EfCoreRepository(LibraryDBContext db)
        {
            Db = db;
        }
        public async Task<IList<T>> GetAllAsync()
        {
            return await Db.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await Db.Set<T>().FindAsync(id);
        }


        public async Task<bool> AddAsync(T item)
        {
            try
            {
                await Db.Set<T>().AddAsync(item);
                await Db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> AddAsync(List<T> item)
        {
            await Db.Set<T>().AddRangeAsync(item);
            await Db.SaveChangesAsync();
            return true;
        }

        public Task<bool> AddAsync(params T[] items)
        {
            throw new NotImplementedException();
        }


        public bool Update(T item)
        {
            try
            {
                Db.Update(item);
                Db.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(List<T> item)
        {
            throw new NotImplementedException();
        }

        public bool Update(params T[] items)
        {
            throw new NotImplementedException();
        }


        public Task<bool> DeleteAsync(T item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(List<T> item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(params T[] items)
        {
            throw new NotImplementedException();
        }

        

       
    }
}
