using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Plain.Web.Mvc.Models
{
    public class ListViewModel
    {
        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }

        public int PageSize { get; set; }

        public FilterViewModel Filter { get; set; }
    }

    public class ListViewModel<T> : ListViewModel
    {
        public IList<T> Items { get; set; }
    }
}