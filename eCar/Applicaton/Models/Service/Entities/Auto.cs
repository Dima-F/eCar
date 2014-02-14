using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using System.Web.Mvc;
using Iesi.Collections.Generic;

namespace eCar.Applicaton.Models.Service.Entities
{
    public class Auto
    {
        [ScaffoldColumn(false)]
        public int AutoID { get; set; }

        [DisplayName("Auto name")]
        [Required(ErrorMessage = "The name of car required")]
        [StringLength(50)]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:F2}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Price is required")]
        [Range(10.00, 1000000.00, ErrorMessage = "Price must be between 100.00 and 1000000.00")] 
        public decimal Price { get; set; }

        [StringLength(256)]
        [Required(ErrorMessage = "Please, define path to small image")]
        [DisplayName("Small image")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Please, define path to main image")]
        [DisplayName("Image")]
        [StringLength(256)]
        public string Image { get; set; }

        public bool PromoFront { get; set; }
        
        public bool PromoDept { get; set; }

        //для связи one-to-many с категориями
        [Required]
        [DisplayName("Category")]
        public int CategoryID { get; set; }

        public virtual Category Category { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}