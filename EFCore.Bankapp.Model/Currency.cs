using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCore.Bankapp.Model
{
    public class Currency
    {
        [Key]
        public string Name { get; set; }
        public float ConversionRate { get; set; }
    }
}
