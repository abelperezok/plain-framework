using System.Collections.Generic;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class TextMultiValueItem : List<TextValueItem>
    {
        public TextMultiValueItem()
        {
            
        }

        public TextMultiValueItem(IEnumerable<TextValueItem> textValueItems) : base(textValueItems)
        {
        }
    }
}