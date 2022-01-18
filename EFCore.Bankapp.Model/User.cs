using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using static EFCore.Bankapp.Enums.UserEnum;

namespace EFCore.Bankapp.Model
{
    public class User
    {
        [MaxLength(100)]
        public string ID { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public UserGender Gender { get; set; }

    }
}
