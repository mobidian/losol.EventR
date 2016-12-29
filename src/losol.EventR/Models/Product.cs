using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace losol.EventR.Models
{
    public class Product
    {
        public int ProductId { get; set; }


        [Required(ErrorMessage = "Produktet må ha et navn")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Produktet må en kort beskrivelse")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public int MaxParticipants { get; set; } = 0;
        public decimal Price { get; set; } = 0;
        public int VatPercent { get; set; } = 25;


    }
}
