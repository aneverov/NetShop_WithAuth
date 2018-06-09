using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace NetShop_With_Auth.Models
{
    public enum SortState
    {
        NameAsc,
        NameDesc,
        PriceAsc,
        PriceDesc,
        CompAsc,
        CompDesc
    }

    public class Phone
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Поле должно быть заполнено")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Длина имени телефона от 1 до 50")]
        //[Remote(action: "CheckName", controller: "Phone", AdditionalFields = "CompanyId", ErrorMessage = "Такой телефон уже создан")]
        public string Name { get; set; }
        [Required]
        [Range(50, 2000)]
        public int Price { get; set; }

        public int Quantity { get; set; }

        public List<BasketToPhone> BasketToPhones { get; set; }

        [Remote(action: "CheckName", controller: "Phone", AdditionalFields = "Name", ErrorMessage = "Такой телефон уже создан")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
