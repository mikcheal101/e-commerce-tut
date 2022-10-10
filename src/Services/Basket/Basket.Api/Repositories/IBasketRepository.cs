namespace Basket.Api.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Basket.Api.Entities;

    public interface IBasketRepository
    {
        Task<ShoppingCart> GetBasket(string userName);
        Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart);
        Task DeleteBasket(string userName);
    }
}