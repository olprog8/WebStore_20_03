﻿using System;
using System.Collections.Generic;
using System.Linq;


namespace WebStore1p.ViewModels
{
    public class CartViewModel
    {
        public Dictionary<ProductViewModel, int> Items { get; set; } = new Dictionary<ProductViewModel, int>();

        public int ItemsCount => Items?.Sum(item => item.Value) ?? 0;
    }
}
