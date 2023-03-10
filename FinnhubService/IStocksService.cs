using ServiceContracts.DTO;
using System;

namespace ServiceContracts
{
    public interface IStocksService
    {
        BuyOrderResponse CreateBuyOrder(BuyOrderRequest? request);

        SellOrderResponse CreateSellOrder(SellOrderRequest? request);

        List<BuyOrderResponse> GetBuyOrders();

        List<SellOrderResponse> GetSellOrders();
    }
}
