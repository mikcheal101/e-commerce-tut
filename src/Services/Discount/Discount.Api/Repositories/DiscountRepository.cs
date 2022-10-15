namespace Discount.Api.Repositories
{
    using System;
    using Npgsql;
    using System.Threading.Tasks;
    using Discount.Api.Entities;
    using Dapper;
    
    public class DiscountRepository: IDiscountRepository
    {

        public DiscountRepository(IConfiguration config)
        {
            this._configuration = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var coupon = await this.Connection
                .QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM coupon WHERE productId=@productId", 
                    new { ProductId = productId });

            if (coupon == null)
            {
                return new Coupon { ProductId = "", ProductName = "No Discount", Amount = 0, Desription = "No Discount Description" };
            }

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var affected = await this.Connection
                .ExecuteAsync(
                    "INSERT INTO coupon (productId, productName, description, amount) VALUES (@productId, @productName, @description, @amount)",
                    new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Desription, ProductId = coupon.ProductId }
                );


            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {
            var deleted = await this.Connection
                .ExecuteAsync(
                    "DELETE FROM coupon WHERE productId=@productId",
                    new { productId = productId }
                );

            return deleted > 0;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var updated = await this.Connection
                .ExecuteAsync(
                    "UPDATE coupon SET productName=@productName, description=@description, amount=@amount WHERE id=@id",
                    new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Desription, ProductId = coupon.ProductId, Id = coupon.Id }
                );

            return updated > 0;
        }


        private readonly IConfiguration _configuration;
        public NpgsqlConnection Connection { 
            get
            {
                var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
                return new NpgsqlConnection(connectionString);
            } 
        }
    }
}