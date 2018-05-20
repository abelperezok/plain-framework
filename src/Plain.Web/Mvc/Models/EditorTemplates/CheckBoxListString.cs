using System.Collections.Generic;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class CheckBoxListString : List<BooleanKeyValuePair<string, string>>
    {
        public CheckBoxListString()
        {
            
        }

        public CheckBoxListString(IEnumerable<BooleanKeyValuePair<string, string>> booleanKeyValuePairs)
            : base(booleanKeyValuePairs)
        {
            
        }
    }
}