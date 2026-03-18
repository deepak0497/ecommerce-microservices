

namespace Shared.Contracts.Events
{
    public class OrderCreatedEvent
    {

        public Guid OrderId { get; set; }
        public string UserId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
