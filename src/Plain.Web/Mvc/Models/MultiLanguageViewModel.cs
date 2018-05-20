using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Plain.Web.Mvc.Models
{
    public class MultiLanguageViewModel
    {
        public string LanguageName { get; set; }

        public int LanguageId { get; set; }

        //[DataType(DataType.MultilineText)]
        public string Value { get; set; }
    }

    public class ListMultiLanguageViewModel : List<MultiLanguageViewModel>
    { 
    
    }
}