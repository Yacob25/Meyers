using System;
using System.Collections.Generic;
using InterviewTest.Orders;
using InterviewTest.Returns;

namespace InterviewTest.Customers
{
    public abstract class CustomerBase : ICustomer
    {
        private readonly OrderRepository _orderRepository;
        private readonly ReturnRepository _returnRepository;

        protected CustomerBase(OrderRepository orderRepo, ReturnRepository returnRepo)
        {
            _orderRepository = orderRepo;
            _returnRepository = returnRepo;
        }

        public abstract string GetName();
        
        public void CreateOrder(IOrder order)
        {
            _orderRepository.Add(order);
        }

        public List<IOrder> GetOrders()
        {
            return _orderRepository.Get();
        }

        public void CreateReturn(IReturn rga)
        {
            _returnRepository.Add(rga);
        }

        public List<IReturn> GetReturns()
        {
            return _returnRepository.Get();
        }
            
            
        // Note: using double would be more appropriate  since its a  financial transaction or no?
         public float GetTotalSales()
        {
            float totalSales = 0;

            // Getting all of the orders from the repository 
            List <IOrder> orders = _orderRepository.Get();

            // create outer loop to iterate over each order 
            foreach (IOrder order in orders)
            {
                //checking order for each SPECIFIC customer to make sure it matches 
                if (order.Customer == this)
                {

                    //Creating inner loop to loop thru each product corresponding with the order 
                    foreach (OrderedProduct product in order.Products)
                    {
                    //adding the product 
                    totalSales += product.Product.GetSellingPrice();

                    }
                }

            }

            return totalSales;

        }

        public float GetTotalReturns()
        {
            float totalReturns = 0;

            // Getting all of the returns from the repository 
            List <IReturn> returns = _returnRepository.Get();

            // Nested loop. create outer loop to iterate over each order 
            foreach (IReturn returning in returns)
            {
                //checking return for each SPECIFIC customer to make sure it matches 
                if (returning.OriginalOrder.Customer == this)
                {
                    //looping  each product in the return 
                    foreach (ReturnedProduct returnedProduct in returning.ReturnedProducts)
                    {
                    //total returns Calculation
                    totalReturns += returnedProduct.OrderProduct.Product.GetSellingPrice();
                    }
                }
            }

           return totalReturns;

        }

        public float GetTotalProfit()
        {
            return GetTotalSales() - GetTotalReturns();
        }
    }
}
