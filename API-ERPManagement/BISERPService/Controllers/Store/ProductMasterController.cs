using BISERPBusinessLayer.Entities.Masters;
using BISERPBusinessLayer.Repositories.Master.Interfaces;
using BISERPCommon;
using BISERPCommon.Extensions;
using BISERPService.Caching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BISERPService.Controllers.Store
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiController
    {
        IProductMasterRepository _productMaster;
        private static readonly ILogger _loggger = Logger.Register(typeof(ProductController));

        public ProductController(IProductMasterRepository productMaster)
        {
            _productMaster = productMaster;
        }

        private List<ProductMasterEntities> AllProduct()
        {
            List<ProductMasterEntities> product = new List<ProductMasterEntities>();
            TryCatch.Run(() =>
            {
                if (!MemoryCaching.CacheKeyExist(CachingKeys.Product.ToString()))
                {
                    var list = _productMaster.GetAllProduct();
                    if (list != null && list.Count() > 0)
                        product = list.ToList();

                    MemoryCaching.AddCacheValue(CachingKeys.Product.ToString(), product);
                }
                else
                {
                    product = (List<ProductMasterEntities>)(MemoryCaching.GetCacheValue(CachingKeys.Product.ToString()));
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in AllProduct method of ProductController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            return product;
        }

        [Route("getallproduct")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult GetAllProduct()
        {
            List<ProductMasterEntities> product = new List<ProductMasterEntities>();
            TryCatch.Run(() =>
            {
                var list = AllProduct();
                if (list != null && list.Count() > 0)
                    product = list.ToList();
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in GetAllProduct method of ProductController :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (product.Any())
                return Ok(product);
            else
                return Ok(product);
        }

        [Route("createProduct")]
        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult CreateProduct(ProductMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _productMaster.CheckDuplicateItem(entity.ProductName);
                if (isDuplicate == false)
                {
                    var newID = _productMaster.CreateProduct(entity);
                    entity.ProductID = newID;
                    isSucecss = true;
                    MemoryCaching.RemoveCacheValue(CachingKeys.Product.ToString());
                }

            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in CreateUnit method of  ProductController : parameter :" + Environment.NewLine + ex.StackTrace);
                return InternalServerError();
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Created<ProductMasterEntities>(Request.RequestUri + entity.ProductID.ToString(), entity);
            else
                return BadRequest();
        }

        [Route("updateProduct")]
        [AcceptVerbs("POST")]
        public IHttpActionResult UpdateProduct(ProductMasterEntities entity)
        {
            bool isSucecss = false, isDuplicate = false;
            TryCatch.Run(() =>
            {
                isDuplicate = _productMaster.CheckDuplicateupdate(entity.ProductName, entity.ProductID);
                if (isDuplicate == false)
                {
                    isSucecss = _productMaster.UpdateProduct(entity);
                    MemoryCaching.RemoveCacheValue(CachingKeys.Product.ToString());
                }
            }).IfNotNull(ex =>
            {
                _loggger.LogError("Error in UpdateProduct method of ProductController : parameter :" + Environment.NewLine + ex.StackTrace);
                return new HttpResponseException(HttpStatusCode.InternalServerError);
            });
            if (isDuplicate)
                return BadRequest("Code Already Exist");
            else if (isSucecss)
                return Ok(Created<ProductMasterEntities>(Request.RequestUri + entity.ProductID.ToString(), entity));
            else
                return BadRequest();
        }

    }
}
