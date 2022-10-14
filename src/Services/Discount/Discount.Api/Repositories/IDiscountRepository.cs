namespace Discount.Api.Repositories
{
    using System;
    using Discount.Api.Entities;

    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productId);

        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productId);
        Task<bool> UpdateDiscount(Coupon coupon);
    }    
}