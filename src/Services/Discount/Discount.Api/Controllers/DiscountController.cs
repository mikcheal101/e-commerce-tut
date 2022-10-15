using System.Net;
namespace Discount.Api.Controllers
{
    using System;
    using Discount.Api.Repositories;
    using Discount.Api.Entities;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController: ControllerBase
    {
        private readonly IDiscountRepository _repository;

        public DiscountController(IDiscountRepository repository)
        {
            this._repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet("{productId}", Name = "GetDiscount")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productId)
        {
            var discount = await this._repository.GetDiscount(productId);

            if (discount == null)
            {
                return NotFound();
            }
            return Ok(discount);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateDiscount([FromBody] Coupon coupon)
        {
            await this._repository.CreateDiscount(coupon);
            return CreatedAtRoute("GetDiscount", new { productId = coupon.ProductId }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> UpdateDiscount([FromBody] Coupon coupon)
        {
            var discount = await this._repository.UpdateDiscount(coupon);
            return Ok(discount);
        }

        [HttpDelete("{productId}", Name = "DeleteDiscount")]
        public async Task<IActionResult> DeleteDiscount(string productId)
        {
            var deleted = await this._repository.DeleteDiscount(productId);
            return Ok(deleted);
        }
    }
}