using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Iesi.Collections.Generic;

namespace eCar.Applicaton.Models.Service.Entities
{
    //категорию пользователи редактировать не будут. Поэтому никаких атрибутов валидации пока не будем добавлять.
    public class Category
    {
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DepartmentID { get; set; }
        public virtual Department Department { get; set; }
        public virtual ICollection<Auto> Autos { get; set; } 
        public Category()
        {
            Autos=new HashedSet<Auto>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}