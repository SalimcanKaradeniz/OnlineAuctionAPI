using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using OnlineAuction.Data.DbEntity;
using OnlineAuction.Data.Models;
using OnlineAuction.Services.Log;
using OnlineAuction.Services.Pages;
using OnlineAuction.Services.Products;
using OnlineAuction.Services.Sliders;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly AppSettings _appSettings;
        private readonly ISliderService _sliderService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IAppContext _appContext;
        private readonly IProductCategoryGroupService _productCategoryGroupService;
        private readonly IProductCategoryService _productCategoryService;
        private readonly IProductService _productService;
        public ProductsController(IOptions<AppSettings> appSettings,
            ISliderService sliderService,
            IServiceProvider serviceProvider,
            IAppContext appContext,
            IProductCategoryGroupService productCategoryGroupService,
            IProductCategoryService productCategoryService,
        IProductService productService)
        {
            _appSettings = appSettings.Value;
            _sliderService = sliderService;
            _serviceProvider = serviceProvider;
            _appContext = appContext;
            _productService = productService;
            _productCategoryService = productCategoryService;
            _productCategoryGroupService = productCategoryGroupService;
        }

        #region Product Category Group

        [HttpGet]
        [Route("/productcategorygroups")]
        public IActionResult GetProductCategoryGroups()
        {
            return Ok(_productCategoryGroupService.GetProductCategoryGroups());
        }

        [HttpGet]
        [Route("/productcategorygroup/{id}")]
        public IActionResult GetProductCategoryGroupsById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_productCategoryGroupService.GetProductCategoryGroupsById(id));
        }

        [HttpPost]
        [Route("/productcategorygroup/add")]
        public IActionResult ProductCategoryGroupAdd([FromBody] ProductCategoryGroup model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryGroupService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/productcategorygroup/update")]
        public IActionResult ProductCategoryGroupUpdate([FromBody] ProductCategoryGroup model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryGroupService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }


        [HttpPost]
        [Route("/productcategorygroup/delete/{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kategori Grubu bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryGroupService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Product Category

        [HttpGet]
        [Route("/productcategories")]
        public IActionResult GetProductCategories()
        {
            return Ok(_productCategoryService.GetProductCategories());
        }

        [HttpGet]
        [Route("/productcategory/{id}")]
        public IActionResult GetProductCategoryById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün Kategori Grubu bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_productCategoryService.GetProductCategoryById(id));
        }

        [HttpPost]
        [Route("/productcategory/add")]
        public IActionResult ProductCategoryAdd([FromBody] ProductCategoryRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/productcategory/update")]
        public IActionResult ProductCategoryUpdate([FromBody] ProductCategoryRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }


        [HttpPost]
        [Route("/productcategory/delete/{id}")]
        public IActionResult ProductCategoryDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Kategori Grubu bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _productCategoryService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion

        #region Product

        [HttpGet]
        [Route("/products")]
        public IActionResult GetProducts()
        {
            return Ok(_productService.GetProducts());
        }

        [HttpGet]
        [Route("/product/{id}")]
        public IActionResult GetProductById([FromRoute]int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün bulunamadı";
                return BadRequest(returnModel);
            }

            return Ok(_productService.GetProductById(id));
        }

        [HttpPost]
        [Route("/products/add")]
        public IActionResult ProductAdd([FromBody] ProductRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productService.Add(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        [HttpPost]
        [Route("/products/update")]
        public IActionResult ProductUpdate([FromBody] ProductRequestModel model)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (!ModelState.IsValid)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Lütfen zorunlu alanları doldurunuz";

                return BadRequest(returnModel);
            }

            returnModel = _productService.Update(model);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }


        [HttpPost]
        [Route("/product/delete/{id}")]
        public IActionResult ProductDelete([FromRoute] int id)
        {
            ReturnModel<object> returnModel = new ReturnModel<object>();

            if (id <= 0)
            {
                returnModel.IsSuccess = false;
                returnModel.Message = "Ürün bulunamadı";

                return BadRequest(returnModel);
            }

            returnModel = _productService.Delete(id);

            if (returnModel.IsSuccess)
                return Ok(returnModel);
            else
                return Conflict(returnModel);
        }

        #endregion
    }
}