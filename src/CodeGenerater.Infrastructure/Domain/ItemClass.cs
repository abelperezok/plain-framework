using Plain.Infrastructure.Domain;
using System.Collections.Generic;

namespace CodeGenerater.Infrastructure.Domain
{
    public class ItemClass : Entity
    {	
		public virtual string Name { get; set; }
		
		public virtual string Folders { get; set; }

        public virtual List<string> FolderList
        {
            get
            {
                var items = Folders.Split('.');
                return new List<string>(items);
            }
        }

        public virtual IList<Property> Properties { get; set; }

        public string Namespace { get; set; }

        public string Ext { get; set; }

        public virtual ItemClass Clone()
        {
            var result = new ItemClass { Name = this.Name, Folders = this.Folders };
            result.Properties = new List<Property>();
            foreach (var item in this.Properties)
            {
                result.Properties.Add(item.Clone());
            }
            return result;
        }

        public string GetFolderPath()
        {
            return "";
        }

        public string GetName()
        {
            return string.Concat(Name, ".", Ext);
        }
    }
}

