using System;

namespace Plain.Web.Mvc.Models.EditorTemplates
{
    public class TextValueItem
    {
        public long LanguageId { get; set; }

        public string LanguageName { get; set; }

        public string Value { get; set; }

        [Obsolete("Esta propiedad está obsoleta, en su lugar se debería utilizar un objeto de la clase TextMultiValueItem, que ya se prevee para el tratamiento de plantillas")]
        public string FieldName { get; set; }
    }
}