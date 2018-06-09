using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetShop_With_Auth.Models
{
    public class BasketToPhoneModel
    {
        public int Id { get; set; }

        public int BasketId { get; set; }
        public Basket Basket { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}
