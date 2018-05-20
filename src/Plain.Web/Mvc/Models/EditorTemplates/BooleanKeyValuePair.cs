using System;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class BooleanKeyValuePair<TK, TV> 
    {
        public BooleanKeyValuePair()
        {
        }

        public BooleanKeyValuePair(TK key, TV value, bool selected = false)
        {
            Key = key;
            Value = value;
            Selected = selected;
        }

        public TK Key { get; set; }

        public TV Value { get; set; }

        public bool Selected { get; set; }
    }
}