using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetShop_With_Auth.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Заполните текст комментария")]
        public string Text { get; set; }

        public DateTime CommentDate { get; set; }

        public int PhoneId { get; set; }
        public Phone Phone { get; set; }
    }
}
