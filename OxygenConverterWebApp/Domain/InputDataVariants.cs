using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OxygenConverterWebApp.Domain
{
    public class InputDataVariants
    {
        [Key]
        public int ID_InputDataVariant { get; set; }

        /// <summary>
        /// ID варианта расчета
        /// </summary>
        public int ID_Variant { get; set; }

        public Variants Variants { get; set; }

                
        [Display(Name = "Q")]
        public double Q { get; set; }

               
        [Display(Name = "q1")]
        public double q { get; set; }

        [Display(Name = "T")]
        public double T { get; set; }

        [Display(Name = "P")]
        public double P { get; set; }

        public UserProfile Owner { get; set; }
    }
}