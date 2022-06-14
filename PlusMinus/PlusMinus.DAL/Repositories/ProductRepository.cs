using Microsoft.EntityFrameworkCore;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Repositories
{
    public class ProductRepository<T> : IProductRepository<T> where T : Product
    {
        private readonly ApplicationContext _context;

        public ProductRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public void DeleteProduct(int productId)
        {
            try
            {
                T product = this._context.Set<T>().Find(productId);
                this._context.Set<T>().Remove(product);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public T GetProductByID(int productId)
        {
            try
            {
                return (T)this._context.Set<T>().Find(productId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> GetProducts(Expression<Func<T, bool>> filter)
        {
            try
            {
                return this._context.Set<T>().Where(filter).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void InsertProduct(T product)
        {
            try
            {
                this._context.Set<T>().Add(product);
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProduct(T product)
        {
            try
            {
                this._context.Entry(product).State = EntityState.Modified;
                this._context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
