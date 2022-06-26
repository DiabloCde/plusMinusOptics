using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.DAL.Interfaces
{
    public interface IProductRepository<T> where T: Product
    {
        IEnumerable<T> GetProducts(Expression<Func<T, bool>> filter);

        T? FirstOrDefault(Expression<Func<T, bool>> filter);

        T GetProductByID(int productId);

        void InsertProduct(T product);

        void DeleteProduct(int productId);

        void UpdateProduct(T product);
    }
}
