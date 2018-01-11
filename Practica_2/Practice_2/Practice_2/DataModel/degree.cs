using System;
using System.Collections.Generic;
using System.Linq;

using LinqToDB;
using LinqToDB.Mapping;

namespace DataModel
{
    [Table(Schema = "public", Name = "degrees")]
    public partial class degree
    {
        public degree() { this.id = 0; } //empty degree

        public override string ToString()
        {
            if (this.isEmpty())
            {
                return "no degree";
            }
               
            return this.code + "-" + this.name;
        }

        public Boolean isEmpty()
        {
            return this.id == 0;
        }
        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }

            degree deg = (degree)obj;
            if (deg.isEmpty())
            {
                return this.isEmpty();
            }

            if (this.isEmpty())
            {
                return false;
            }
            
            return deg.name.Equals(this.name) && deg.code.Equals(this.code);
        }
        
        public override int GetHashCode()
        {
            if (this.isEmpty())
            {
                return this.id.GetHashCode();
            }
            return this.code.GetHashCode() ^ this.name.GetHashCode() ^ this.id.GetHashCode();
        }               
    }
}
