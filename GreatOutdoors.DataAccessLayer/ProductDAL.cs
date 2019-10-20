using System;
using System.Collections.Generic;
using Capgemini.GreatOutdoors.Contracts.DALContracts;
using Capgemini.GreatOutdoors.Entities;
using Capgemini.GreatOutdoors.Exceptions;
using Capgemini.GreatOutdoors.Helpers;

namespace Capgemini.GreatOutdoors.DataAccessLayer
{
    /// <summary>
    /// Contains data access layer methods for inserting, updating, deleting products from Products collection.
    /// </summary>
    public class ProductDAL : ProductDALBase, IDisposable
    {
        /// <summary>
        /// Adds new product to Products collection.
        /// </summary>
        /// <param name="newProduct">Contains the product details to be added.</param>
        /// <returns>Determinates whether the new product is added.</returns>
        public override bool AddProductDAL(Product newProduct)
        {
            bool productAdded = false;
            try
            {
                newProduct.ProductID = Guid.NewGuid();
                productList.Add(newProduct);
                productAdded = true;
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return productAdded;
        }

        /// <summary>
        /// Gets all products from the collection.
        /// </summary>
        /// <returns>Returns list of all products.</returns>
        public override List<Product> GetAllProductsDAL()
        {
            return productList;
        }

        /// <summary>
        /// Gets product based on ProductID.
        /// </summary>
        /// <param name="searchProductID">Represents ProductID to search.</param>
        /// <returns>Returns Product object.</returns>
        public override Product GetProductByProductIDDAL(Guid searchProductID)
        {
            Product matchingProduct = null;
            try
            {
                //Find Product based on searchProductID
                matchingProduct = productList.Find(
                    (item) => { return item.ProductID == searchProductID; }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingProduct;
        }

        /// <summary>
        /// Gets product based on ProductName.
        /// </summary>
        /// <param name="productName">Represents ProductName to search.</param>
        /// <returns>Returns Product object.</returns>
        public override List<Product> GetProductsByNameDAL(string productName)
        {
            List<Product> matchingProducts = new List<Product>();
            try
            {
                //Find All Products based on productName
                matchingProducts = productList.FindAll(
                    (item) => { return item.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase); }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingProducts;
        }

        /// <summary>
        /// Gets product based on email.
        /// </summary>
        /// <param name="CategoryName">Represents Product's Category Name.</param>
        /// <returns>Returns Product object.</returns>
        public override List<Product> GetProductsByCategoryDAL(Category categoryName)
        {
            List<Product> matchingProduct = null;
            try
            {
                //Find Product based on Email and Password
                matchingProduct = productList.FindAll(
                    (item) => { return item.CategoryName.Equals(categoryName); }
                );
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return matchingProduct;
        }



        /// <summary>
        /// Updates product based on ProductID.
        /// </summary>
        /// <param name="updateProduct">Represents Product details including ProductID, ProductName etc.</param>
        /// <returns>Determinates whether the existing product is updated.</returns>
        public override bool UpdateProductDAL(Product updateProduct)
        {
            bool productUpdated = false;
            try
            {
                //Find Product based on ProductID
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);

                if (matchingProduct != null)
                {
                    //Update product details
                    ReflectionHelpers.CopyProperties(updateProduct, matchingProduct, new List<string>() { "ProductName", "CategoryName", "ProductSize", "ProductColour",
                    "ProductTechSpecs"});
                    productUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return productUpdated;
        }

        /// <summary>
        /// Update product stock.
        /// </summary>
        /// <param name="updateProduct">Represents Product details including ProductID, ProductName etc.</param>
        /// <returns>Determinates whether the existing product is updated.</returns>

        public override bool UpdateProductStockDAL(Product updateProduct)
        {
            bool stockUpdated = false;
            try
            {
                //Find Product based on ProductID
                Product matchingProduct = GetProductByProductIDDAL(updateProduct.ProductID);

                if (matchingProduct != null)
                {
                    //Update product details
                    ReflectionHelpers.CopyProperties(updateProduct, matchingProduct, new List<string>() { "ProductStock" });
                    stockUpdated = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return stockUpdated;
        }


        /// <summary>
        /// Deletes product based on ProductID.
        /// </summary>
        /// <param name="deleteProductID">Represents ProductID to delete.</param>
        /// <returns>Determinates whether the existing product is updated.</returns>
        public override bool DeleteProductDAL(Guid deleteProductID)
        {
            bool productDeleted = false;
            try
            {
                //Find Product based on searchProductID
                Product matchingProduct = productList.Find(
                    (item) => { return item.ProductID == deleteProductID; }
                );

                if (matchingProduct != null)
                {
                    //Delete Product from the collection
                    productList.Remove(matchingProduct);
                    productDeleted = true;
                }
            }
            catch (GreatOutdoorsException)
            {
                throw;
            }
            return productDeleted;
        }



        /// <summary>
        /// Clears unmanaged resources such as db connections or file streams.
        /// </summary>
        public void Dispose()
        {
            //No unmanaged resources currently
        }
    }
}



