using System;

namespace Bakery.DB.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; set; }

        int CakeId { get; set; }

        int CustomerId { get; set; }

        float OrderWeight { get; set; }

        OrderType OrderType { get; set; }

        DateTime OrderDate { get; set; }
    }
}
