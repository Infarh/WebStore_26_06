﻿using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Domain.ViewModels.BreadCrumbs
{
    public enum BreadCrumbsType
    {
        None,
        Section, 
        Brand,
        Item
    }

    public class BreadCrumbsViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BreadCrumbsType BreadCrumbsType { get; set; }
    }
}
