using ServiceContracts;
using ServiceContracts.DTO;
using Services;
using Xunit.Abstractions;

namespace UnitTest
{
    public class BuyAndSellTest
    {
        private readonly IStocksService _stocksService;
        private readonly ITestOutputHelper _testOutputHelper;

        public BuyAndSellTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _stocksService = new StocksService();
        }

        #region CreateBuyOrder


        [Fact]
        public void BuyOrderRequest_AsNull()
        {
            //Arrange
            BuyOrderRequest? order_request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateBuyOrder(order_request);
            });
        }


        [Theory] // we use [Theory] instead of [Fact], so that we can pass parameters to the test method 
        [InlineData(0)] // passing parameters to the test method
        public void BuyOrderRequest_AsZero(uint orderQuantity)
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = orderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("1999-05-05")
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(order_request);
            });
        }

        [Theory] // we use [Theory] instead of [Fact], so that we can pass parameters to the test method 
        [InlineData(100001)] // passing parameters to the test method
        public void BuyOrderRequest_AsMax(uint orderQuantity)
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = orderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("1999-05-05")
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(order_request);
            });
        }


        [Theory]
        [InlineData(0)]
        public void BuyOrderRequest_PriceLess(double orderPrice)
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = orderPrice,
                Quantity = 3
            };
            if (order_request.Price < 1)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _stocksService.CreateBuyOrder(order_request);
                });
            }
        }

        [Theory]
        [InlineData(10001)]
        public void BuyOrderRequest_PriceMore(double orderPrice)
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = orderPrice,
                Quantity = 3
            };
            if (order_request.Price > 10000)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _stocksService.CreateBuyOrder(order_request);
                });
            }
        }


        [Fact]
        public void BuyOrderRequest_EmptyStockSymbol()
        {
            BuyOrderRequest order_request = new BuyOrderRequest();
            if(string.IsNullOrEmpty(order_request.StockSymbol))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _stocksService.CreateBuyOrder(order_request);
                });
            }
        }

        [Fact]
        public void BuyOrderRequest_OldDateTime()
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                Quantity = 2,
                Price = 100

            };
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateBuyOrder(order_request);

            });
        }   

        [Fact]
        public void BuyOrderRequest_ProperValues()
        {
            BuyOrderRequest order_request = new BuyOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2003-01-01"),
                Quantity= 2,
                Price= 100
            };

            BuyOrderResponse order_from_create = _stocksService.CreateBuyOrder(order_request);

            List<BuyOrderResponse> order_list = _stocksService.GetBuyOrders();

            //we check newly created Guid if it is created
            Assert.True(order_from_create.BuyOrderID != Guid.Empty);
            Assert.Contains(order_from_create, order_list);
        }
        #endregion







        #region CreateSellOrder
        [Fact]
        public void SellOrderRequest_AsNull()
        {
            //Arrange
            SellOrderRequest? order_request = null;

            //Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                _stocksService.CreateSellOrder(order_request);
            });
        }


        [Theory] // we use [Theory] instead of [Fact], so that we can pass parameters to the test method 
        [InlineData(0)] // passing parameters to the test method
        public void SellOrderRequest_QuantityLessThanMinimum(uint orderQuantity)
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = orderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("1999-05-05")
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(order_request);
            });
        }

        [Theory] // we use [Theory] instead of [Fact], so that we can pass parameters to the test method 
        [InlineData(100001)] // passing parameters to the test method
        public void SellOrderRequest_QuantityMoreThanMax(uint orderQuantity)
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = 1,
                Quantity = orderQuantity,
                DateAndTimeOfOrder = DateTime.Parse("1999-05-05")
            };

            // Act and Assert
            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(order_request);
            });
        }


        [Theory]
        [InlineData(0)]
        public void SellOrderRequest_PriceLessThanMin(double orderPrice)
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = orderPrice,
                Quantity = 3
            };
            if (order_request.Price < 1)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _stocksService.CreateSellOrder(order_request);
                });
            }
        }

        [Theory]
        [InlineData(10001)]
        public void SellOrderRequest_PriceMoreThanMax(double orderPrice)
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                Price = orderPrice,
                Quantity = 3
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(order_request);
            });
        }


        [Fact]
        public void SellOrderRequest_EmptyStockSymbol()
        {
            SellOrderRequest order_request = new SellOrderRequest();
            if (string.IsNullOrEmpty(order_request.StockSymbol))
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    _stocksService.CreateSellOrder(order_request);
                });
            }
        }

        [Fact]
        public void SellOrderRequest_OldDateTime()
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("1999-01-01"),
                Quantity = 2,
                Price = 100
            };

            Assert.Throws<ArgumentException>(() =>
            {
                _stocksService.CreateSellOrder(order_request);

            });
        }

        [Fact]
        public void SellOrderRequest_ProperValues()
        {
            SellOrderRequest order_request = new SellOrderRequest()
            {
                StockSymbol = "MSFT",
                StockName = "Microsoft",
                DateAndTimeOfOrder = DateTime.Parse("2003-01-01"),
                Quantity = 2,
                Price = 100
            };

            SellOrderResponse order_from_create = _stocksService.CreateSellOrder(order_request);

            List<SellOrderResponse> order_list = _stocksService.GetSellOrders();

            //we check newly created Guid if it is created
            Assert.True(order_from_create.SellOrderID != Guid.Empty);
            Assert.Contains(order_from_create, order_list);
        }

        #endregion
    }
}