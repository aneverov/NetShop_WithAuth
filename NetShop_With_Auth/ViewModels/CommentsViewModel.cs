using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetShop_With_Auth.Models;

namespace NetShop.ViewModels
{
    public class CommentsViewModel
    {
        public int PhoneId { get; set; }
        public List<Comment> Comments { get; set; }
        public Comment NewComment { get; set; }
    }
}
