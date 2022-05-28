using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CodingChallenge5Entities
{
    public class SoldItemDetailEntity
    {
        [Required]
        public string ItemName { get; set; }

        [Required]
        public double Quantity { get; set; }

        [Required]
        public double Amount { get; set; }

    }
}
