using System.Collections.Generic;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class DropDownListItem
    {
        public DropDownListItem()
        {
            ItemList = new List<KeyValuePair<long, string>>();
        }

        public int ItemId { get; set; }

        public List<KeyValuePair<long, string>> ItemList { get; set; }
    }
}