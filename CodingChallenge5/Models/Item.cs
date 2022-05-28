using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodingChallenge5.Models
{
    public class Item
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public int ItemQuantity { get; set; }

    }
}
