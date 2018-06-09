using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetShop_With_Auth.Models
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyComparer : IComparer<Company>
    {
        public int Compare(Company x, Company y)
        {
            return x.Name.CompareTo(y.Name);
        }
    }
}
