namespace Basket.Api.Repositories
{
    using System;
    using Basket.Api.Entities;
    using Newtonsoft.Json;
    using Microsoft.Extensions.Caching.Distributed;

    public class BasketRepository: IBasketRepository
    {
        private readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            this._redisCache = redisCache ?? throw new ArgumentNullException(nameof(redisCache));
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            string shoppingCart = await this._redisCache.GetStringAsync(userName);
            if (String.IsNullOrEmpty(shoppingCart))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(shoppingCart);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart shoppingCart)
        {
            var cart = JsonConvert.SerializeObject(shoppingCart);
            await this._redisCache.SetStringAsync(shoppingCart.UserName, cart);
            return await this.GetBasket(shoppingCart.UserName);
        }

        public async Task DeleteBasket(string userName)
        {
            await this._redisCache.RemoveAsync(userName);
        }
    }
}