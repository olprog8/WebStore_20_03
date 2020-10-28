﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

using WebStore1p.Domain.Identity;

using WebStore1p.Domain.Entities;

using WebStore1p.Infrastructure.Interfaces;

namespace WebStore1p.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrator)]
    public class ProductsController : Controller
    {
        private readonly IProductData _ProductData; 
        public ProductsController(IProductData ProductData) => _ProductData = ProductData;
        public IActionResult Index(/*[FromServices] IProductData Products*/) => View(_ProductData.GetProducts());

        public IActionResult Edit(int? id) {

            var product = id is null ? new Product() : _ProductData.GetProductById((int)id);

            if (product is null)
                return NotFound();


            return View(product);
         
        }

        public IActionResult Delete(int id)
        {
            var product = _ProductData.GetProductById((int)id);

            if (product is null)
                return NotFound();

            return View(product);
        };

        [HttpPost, ValidateAntiForgeryToken, ActionName(nameof(Delete))]
        public IActionResult DeleteConfirm(int id) => RedirectToAction(nameof(Index));
    }
}
