using Plain.Infrastructure.Domain;

namespace CodeGenerater.Infrastructure.Domain
{
    public class Property : Entity
    {	
		public virtual string Name { get; set; }
		
		public virtual string VarType { get; set; }
		
		public virtual ColumnType ColumnType { get; set; }
		
		public virtual bool AllowNull { get; set; }

        public virtual ItemClass ItemClass { get; set; }

        public int ItemClassId { get; set; }

        public virtual string GetCode()
        {
            string value = string.Concat("public", " ", "virtual", " ", VarType, " ", Name, " ", "{ get; set; }");
            return value;
        }
        
        public virtual Property Clone()
        {
           return new Property { Name = this.Name, VarType = this.VarType };
        }
    }
}
