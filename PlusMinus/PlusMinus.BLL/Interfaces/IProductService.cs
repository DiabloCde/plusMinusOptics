using PlusMinus.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.BLL.Interfaces
{
    public interface IProductService<T> where T: Product
    {
        IEnumerable<T> GetProducts(Expression<Func<T, bool>> filter);

        T? FirstOrDefault(Expression<Func<T, bool>> filter);

        T GetProductByID(int productId);

        void AddProduct(T product);

        void DeleteProduct(int productId);

        void UpdateProduct(T product);
    }
}
