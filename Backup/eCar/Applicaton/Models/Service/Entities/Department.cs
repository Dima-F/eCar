using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Iesi.Collections.Generic;

namespace eCar.Applicaton.Models.Service.Entities
{
    //точно также как и в случае категорий, отделы редактироватся не будут. Поэтому сущность - без атрибутов.
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //для связи one-to-many с категориями
        public virtual ICollection<Category> Categories { get; set; }
        public Department()
        {
            Categories=new HashedSet<Category>();
        }
        public override string ToString()
        {
            return Name;
        }
    }
}