using System.Collections.Generic;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class DropDownListStringItem
    {
        public DropDownListStringItem()
        {
            ItemList = new List<KeyValuePair<string, string>>();
        }

        public string ItemId { get; set; }

        public List<KeyValuePair<string, string>> ItemList { get; set; }
    }
}