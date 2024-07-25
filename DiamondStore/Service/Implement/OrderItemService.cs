using BussinessObject.Models;
using Repository.Interface;
using Service.Interface;
using System;
using System.Linq.Expressions;

namespace Service.Implement
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IBaseCRUD<OrderItem> _itemRepo;
        private readonly IBaseCRUD<Order> _orderRepo;
        private readonly IBaseCRUD<Product> _productRepo;

        public OrderItemService(IBaseCRUD<OrderItem> itemRepo, IBaseCRUD<Order> orderRepo, IBaseCRUD<Product> productRepo)
        {
            _itemRepo = itemRepo ?? throw new ArgumentNullException(nameof(itemRepo));
            _orderRepo = orderRepo;
            _productRepo = productRepo;
        }

        public async Task<OrderItem> GetByIdAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var orderItem = await _itemRepo.GetByIdAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"Order item with ID {id} not found.");
            }

            return orderItem;
        }

        public async Task<List<OrderItem>> GetAllAsync()
        {
            var orderItems = await _itemRepo.GetAllAsync();
            return orderItems.ToList();
        }

        public async Task AddAsync(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order item entity cannot be null.");
            }

            await _itemRepo.AddAsync(entity);
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Order item entity cannot be null.");
            }

            var existingOrderItems = await _itemRepo.GetByIdAsync(entity.OrderItemId.ToString());
            if (existingOrderItems == null)
            {
                throw new KeyNotFoundException($"Order item with ID {entity.OrderItemId} not found.");
            }

            await _itemRepo.UpdateAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentException("ID cannot be null or empty.", nameof(id));
            }

            var existingOrderItem = await _itemRepo.GetByIdAsync(id);
            if (existingOrderItem == null)
            {
                throw new KeyNotFoundException($"Order item with ID {id} not found.");
            }

            await _itemRepo.DeleteAsync(id);
        }

        public async Task<IEnumerable<OrderItem>> FindAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentException("Predicate is null");
            }
            return await _itemRepo.FindAsync(predicate);
        }

        public async Task AddProductToOrder(string userId, string productId)
        {
            if (userId == null)
            {
                throw new ArgumentException("userId is null");
            }
            if (productId == null)
            {
                throw new ArgumentException("Predicate is null");
            }
            var existOrderList = (await _orderRepo.FindAsync(x => x.UserId.ToString() == userId)).ToList();
            if (existOrderList.Count == 0)
            {
                Order order = new Order();
                order.UserId = Guid.Parse(userId);
                order.OrderDate = DateOnly.FromDateTime(DateTime.Now);
                order.Status = "Pending";
                await _orderRepo.AddAsync(order);
            }
            existOrderList = (await _orderRepo.FindAsync(x => x.UserId.ToString() == userId)).ToList();
            if (existOrderList.Count != 0)
            {
                Order existOrder = existOrderList[existOrderList.Count - 1];
                OrderItem orderItem = new OrderItem();
                orderItem.OrderId = existOrder.OrderId;
                orderItem.ProductId = Guid.Parse(productId);
                var existProduct = await _productRepo.Find(x => x.ProductId == Guid.Parse(productId));
                existOrder.TotalAmount = existOrder.TotalAmount + existProduct.Price;
                await _itemRepo.AddAsync(orderItem);
                await _orderRepo.UpdateAsync(existOrder);
            }
        }
    }
}
