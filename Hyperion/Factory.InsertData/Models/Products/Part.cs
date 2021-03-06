﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Factory.InsertData.Models.Products
{
    public class Part
    {
        public Part()
        {
            this.Spaceships = new HashSet<Spaceship>();
        }
        
        [Key]
        public int Id { get; set; }
        
        [StringLength(50)]
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        [ForeignKey("PartType")]
        public int PartTypeId { get; set; }
        
        public virtual PartType PartType { get; set; }
        
        [ForeignKey("Supplier")]
        public int SupplierId { get; set; }
        
        public virtual Supplier Supplier { get; set; }
        
        public virtual ICollection<Spaceship> Spaceships { get; set; }
    }
}
