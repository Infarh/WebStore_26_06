﻿using System;

namespace WebStore.Domain.ViewModels.Product
{
    public class PageViewModel
    {
        public int TotalItems { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }

        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }
}
