using Entities.Model;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Repository.Data;
using Repository.Extensions.Utility;
using System.Linq.Dynamic.Core;

namespace Repository.Extensions;

public static class RepositoryProductExtensions
{
    public static IQueryable<Product> Filter(this IQueryable<Product> products, decimal MinPrice, decimal MaxPrice) =>
        products.Where(p => (p.Price >= MinPrice && p.Price <= MaxPrice));

    public static IQueryable<Product> Search(this IQueryable<Product> products, string? query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return products;
        var lowerCaseQuery = query.Trim().ToLower();
        return products.Where(p => p.Name.ToLower().Contains(lowerCaseQuery));
    }

    public static IQueryable<Product> Sort(this IQueryable<Product> products, string? orderByQueryString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString)) return products;
        if (orderByQueryString == "Name")
            return products.OrderBy(p => p.Name);
       
        var orderQuery = OrderQueryBuilder.CreateOrderQuery<Product>(orderByQueryString);

        if (string.IsNullOrWhiteSpace(orderQuery))
            return products.OrderBy(p => p.Name);

        return products.OrderBy(orderQuery);
    }
}
