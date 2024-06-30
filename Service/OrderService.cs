using Sample_AP.Model;
using Sample_AP.Repository;

namespace Sample_AP.Service;

public class OrderService 
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public Order GetOrderByID(int orderID)//調用
    {
        return _orderRepository.GetOrderByID(orderID);
    }
}
