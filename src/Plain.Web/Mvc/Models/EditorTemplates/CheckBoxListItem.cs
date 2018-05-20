using System.Collections.Generic;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class CheckBoxListItem : List<BooleanKeyValuePair<int,string>>
    {
        public CheckBoxListItem()
        {
            
        }

        public CheckBoxListItem(IEnumerable<BooleanKeyValuePair<int, string>> booleanKeyValuePairs)
            : base(booleanKeyValuePairs)
        {
        }
    }
}