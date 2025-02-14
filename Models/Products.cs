using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;
using System.Threading.Tasks;

namespace HydroSpark.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? Category { get; set; } 
        public string? Description { get; set; } 
        public string? ProductImgUrl { get; set; }
        
        public decimal? Price { get; set; }  

    }
}