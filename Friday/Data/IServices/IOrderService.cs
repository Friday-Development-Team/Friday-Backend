using Friday.DTOs;
using Friday.Models;
using Friday.Models.Out;

namespace Friday.Data.IServices {
    public interface IOrderService {
        OrderHistory GetHistory(string username);
        bool SetAccepted(int id, bool value);
        bool PlaceOrder(OrderDTO orderdto);

    }
}
