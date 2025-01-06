using OrderAPI.DTOs;

namespace OrderAPI.Interfaces
{
    public interface ICouponService
    {
        Task<CouponReadDto?> GetCouponByNameAsync(string couponName);
    }
}