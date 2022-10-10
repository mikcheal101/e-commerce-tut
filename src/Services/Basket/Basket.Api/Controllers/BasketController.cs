using System.Net;
namespace Basket.Api.Controllers
{
    using System;
    using Basket.Api.Repositories;
    using Basket.Api.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController: ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{userName}", Name = "GetShoppingCart")]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> GetShoppingCart(string userName)
        {
            var shoppingCart = await this._repository.GetBasket(userName);
            return Ok(shoppingCart ?? new ShoppingCart(userName));
        }

        [HttpPost]
        [ProducesResponseType(typeof(ShoppingCart), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCart>> UpdateShoppingCart([FromBody] ShoppingCart shoppingCart)
        {
            var cartUpdated = await this._repository.UpdateBasket(shoppingCart);
            return Ok(cartUpdated);
        }

        [HttpDelete("{userName}", Name = "DeleteShoppingCart")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteShoppingCart(string userName)
        {
            await this._repository.DeleteBasket(userName);
            return Ok();
        }

    }
}