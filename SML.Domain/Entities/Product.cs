using Microsoft.AspNetCore.Http;
using SML.Domain.Common;
using SML.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace SML.Domain
{
    public class Product : BaseEntity
    {
        [Required(ErrorMessage = "Пожалуйста введите название")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Пожалуйста введите описание")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Пожалуйста введите цену")]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }

        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
