using Microsoft.AspNetCore.Http;
using SML.Domain.Common;
using SML.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SML.Domain
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
