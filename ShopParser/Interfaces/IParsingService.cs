using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopParser.Interfaces
{
    public interface IParsingService
    {
        IEnumerable<string> GetProductLinks(string catalogLink);
        string GetImageLink(string productLink);
        string GetDescription(string productLink);
        string GetName(string productLink);
        string GetPrice(string productLink);
    }
}
