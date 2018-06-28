﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using TestService.Interfaces;
using TestService.BindingModels;
using TestService;
using Microsoft.AspNet.Identity.Owin;
using TestService.Implementations;

namespace TestRestApi.Controllers
{
    [Authorize(Roles = ApplicationRoles.SuperAdmin + "," + ApplicationRoles.Admin)]
    public class CategoryController : ApiController
    {
        #region global
        private ApplicationDbContext _context;

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? HttpContext.Current.GetOwinContext().Get<ApplicationDbContext>();
            }
            private set
            {
                _context = value;
            }
        }

        private ICategory _service;

        public ICategory Service
        {
            get
            {
                return _service ?? CategoryService.Create(Context);
            }
            private set
            {
                _service = value;
            }
        }
        #endregion

        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = Service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = Service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }

        [HttpPost]
        public void AddElement(CategoryBindingModel model)
        {
            Service.AddElement(model);
        }

        [HttpPost]
        public void UpdElement(CategoryBindingModel model)
        {
            Service.UpdElement(model);
        }

        [HttpPost]
        public void DelElement(CategoryBindingModel model)
        {
            Service.DelElement(model.Id);
        }
    }
}