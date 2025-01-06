
using OrderAPI.DTOs;

namespace OrderAPI.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResponseDTO> GetBkashUrlAsync(PaymentDTO paymentData);
    }
}