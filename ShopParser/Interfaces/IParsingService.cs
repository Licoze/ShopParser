using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopParser.Models;

namespace ShopParser.Interfaces
{
    public interface IParsingService
    {
        Task<int> ParseNewAsync(string link);
        Task<int> RefreshAsync();
        Product GetById(int id);
        IEnumerable<Product> GetAll();
    }
}
