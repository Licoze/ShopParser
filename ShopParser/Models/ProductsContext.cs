using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
namespace ShopParser.Models
{
    //Можно было бы все это обернуть в репозиторий, а потом и в UoW но профит сомнителен, а пару сотен строк кода прибавится.
    public class ProductsContext:DbContext
    {
        ProductsContext():base("ParserBase") {

        }
        public DbSet<Product> Products { get; set; }
    }
}