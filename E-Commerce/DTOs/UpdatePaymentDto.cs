using E_Commerce.Enum;

namespace E_Commerce.DTOs
{
    public class UpdatePaymentDto
    {

        public int PaymentId { get; set; }
        public  DelevieryMethod PaymentMethod {  get; set; }  

        
    }
}
