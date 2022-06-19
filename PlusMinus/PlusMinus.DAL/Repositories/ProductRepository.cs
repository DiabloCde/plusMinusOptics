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
            T product = this._context.Set<T>().Find(productId);
            this._context.Set<T>().Remove(product);
            this._context.SaveChanges();
        }

        public T? FirstOrDefault(Expression<Func<T, bool>> filter)
        {
            T? product = this._context.Set<T>().FirstOrDefault(filter);

            return product;
        }

        public T GetProductByID(int productId)
        {
            return (T)this._context.Set<T>().Find(productId);
        }

        public List<T> GetProducts(Expression<Func<T, bool>> filter)
        {
            return this._context.Set<T>().Where(filter).ToList();
        }

        public void InsertProduct(T product)
        {
            this._context.Set<T>().Add(product);
            this._context.SaveChanges();
        }

        public void UpdateProduct(T product)
        {
            this._context.Entry(product).State = EntityState.Modified;
            this._context.SaveChanges();
        }
    }
}
