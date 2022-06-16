using Microsoft.Extensions.Logging;
using PlusMinus.BLL.Interfaces;
using PlusMinus.Core.Models;
using PlusMinus.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PlusMinus.BLL.Services
{
    public class ProductService<T> : IProductService<T> where T : Product
    {
        private readonly IProductRepository<T> _productRepository;

        private readonly ILogger<ProductService<T>> _logger;

        public ProductService(IProductRepository<T> productRepository, ILogger<ProductService<T>> logger)
        {
            this._productRepository = productRepository;
            this._logger = logger;
        }

        public void AddProduct(T product)
        {
            if (product is null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                this._productRepository.InsertProduct(product);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Product was not added.");
                throw;
            }
        }

        public void DeleteProduct(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductID cannot be less than or equal to zero.");
            }

            try
            {
                this._productRepository.DeleteProduct(productId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Product was not deleted.");
                throw;
            }
        }

        public T GetProductByID(int productId)
        {
            if (productId <= 0)
            {
                throw new ArgumentException("ProductID cannot be less than or equal to zero.");
            }

            try
            {
                return this._productRepository.GetProductByID(productId);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Product was not found.");
                throw;
            }
        }

        public List<T> GetProducts(Expression<Func<T, bool>> filter)
        {
            try
            {
                return this._productRepository.GetProducts(filter);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Products with such filter were not found.");
                throw;
            }
        }

        public void UpdateProduct(T product)
        {
            if (product is null)
            {
                throw new ArgumentException("Product cannot be null.");
            }

            try
            {
                this._productRepository.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, "Product was not updated.");
                throw;
            }
        }
    }
}
