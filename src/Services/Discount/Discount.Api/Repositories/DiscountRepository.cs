namespace Discount.Api.Repositories
{
    using System;
    using Npgsql;
    using System.Threading.Tasks;
    using Discount.Api.Entities;
    using Dapper;
    
    public class DiscountRepository: IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration config)
        {
            this._configuration = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<Coupon> GetDiscount(string productId)
        {
            var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            using var connection = new NpgsqlConnection(connectionString);

            var coupon = await connection
                .QueryFirstOrDefaultAsync<Coupon>(
                    "SELECT * FROM coupon WHERE productName=@productName", 
                    new { ProductName = productId });

            if (coupon == null)
            {
                return new Coupon {ProductName = "No Discount", Amount = 0, Desription = "No Discount Description"};
            }

            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            var connectionString = _configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            using var connection = new NpgsqlConnection(connectionString);

            var affected = await connection
                .ExecuteAsync(
                    "INSERT INTO coupon (productName, description, amount) VALUES (@productName, @description, @amount)",
                    new { ProductName = coupon.ProductName, Amount = coupon.Amount, Description = coupon.Desription, ProductId = coupon.ProductId }
                );


            return affected > 0;
        }

        public async Task<bool> DeleteDiscount(string productId)
        {}
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {}
    }
}