using Newtonsoft.Json;

using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.Req;

    public class ProductsSvc : GenericSvc<ProductsRep, Products>
    {
        #region -- Overrides --

        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public override SingleRsp Update(Products m)
        {
            var res = new SingleRsp();

            var m1 = m.CategoryId > 0 ? _rep.Read(m.ProductId) : _rep.Read(m.ProductName);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;
            }

            return res;
        }
        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public ProductsSvc() { }

        //Tìm kiếm sản phẩm
        public object SearchProduct(int Page, int Size, int Id, string Type, string Keyword)
        {
            var products = All.Where(x => x.ProductName.Contains(Keyword));
            var offset = (Page - 1) * Size;
            int total = products.Count();
            int totalPages = (total % Size) == 0 ? (int)(total / Size) : (int)((total / Size) + 1);
            var data = products.OrderBy(x => x.ProductName).Skip(offset).Take(Size).ToList();

            var res = new
            {
                Data = data,
                TotalRecord = total,
                TotalPages = totalPages,
                PageNumber = Page,
                SizeNumber = Size
            };

            return res;
        }

        //Thêm sản phẩm
        public SingleRsp CreateProduct(ProductReq productsReq)
        {
            var res = new SingleRsp();
            Products products = new Products();
            products.ProductId = productsReq.ProductId;
            products.ProductName = productsReq.ProductName;
            products.CategoryId = productsReq.CategoryId;
            products.QuantityPerUnit = productsReq.QuantityPerUnit;
            products.UnitPrice = productsReq.UnitPrice;
            products.UnitsInStock = productsReq.UnitsInStock;
            products.UnitsOnOrder = productsReq.UnitsOnOrder;
            products.ReorderLevel = productsReq.ReorderLevel;
            products.Discontinued = productsReq.Discontinued;
            res = _rep.CreateProduct(products);

            return res;
        }

        //Cập nhật sản phẩm
        public SingleRsp UpdateProduct(ProductReq productsReq)
        {
            var res = new SingleRsp();
            Products products = new Products();
            products.ProductId = productsReq.ProductId;
            products.ProductName = productsReq.ProductName;
            products.CategoryId = productsReq.CategoryId;
            products.QuantityPerUnit = productsReq.QuantityPerUnit;
            products.UnitPrice = productsReq.UnitPrice;
            products.UnitsInStock = productsReq.UnitsInStock;
            products.UnitsOnOrder = productsReq.UnitsOnOrder;
            products.ReorderLevel = productsReq.ReorderLevel;
            products.Discontinued = productsReq.Discontinued;
            res = _rep.UpdateProduct(products);

            return res;
        }

        //Xóa sản phẩm
        public SingleRsp DeleteProduct(int id)
        {
            var res = new SingleRsp();
            Products products = _rep.Read(id);
            res = _rep.DeleteProduct(products);

            return res;
        }
        #endregion
    }
}
