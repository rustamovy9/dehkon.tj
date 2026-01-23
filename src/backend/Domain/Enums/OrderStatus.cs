namespace Domain.Enums;

public enum OrderStatus
{
    Created = 1,
    AwaitingPayment = 2,
    Paid = 3,
    AssignedCourier = 4,
    OnTheWay = 5,
    InDelivery =6,
    Delivered = 7,
    Canceled = 8
}