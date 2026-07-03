using AutoMapper;
using EcommerceBackend.Application.Common;
using EcommerceBackend.Application.Common.Query;
using EcommerceBackend.Application.Dto.Order;
using EcommerceBackend.Application.Exceptions;
using EcommerceBackend.Application.Interfaces;
using EcommerceBackend.Application.Interfaces.Services;
using EcommerceBackend.Domain.Enums;
using EcommerceBackend.Domain.Models;

namespace EcommerceBackend.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;
        private readonly ICartRepository _cartRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public OrderService(IOrderRepository orderRepo, ICartRepository cartRepo, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUser)
        {
            _orderRepo = orderRepo;
            _cartRepo = cartRepo;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        private void ValidateUser(Order order)
        {
            if (order.UserId != _currentUser.UserId)
                throw new ForbiddenException("Access denied");
        }

        public async Task<PagedResult<OrderDto>> GetAllOrdersAsync(OrderQueryParameters query)
        {
            var result = await _orderRepo.GetAllOrdersAsync(query);
            return new PagedResult<OrderDto>
                (
                    _mapper.Map<List<OrderDto>>(result.Items),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
        }

        public async Task<PagedResult<OrderDto>> GetMyOrdersAsync(OrderQueryParameters query)
        {
            var userId = _currentUser.UserId;
            var result = await _orderRepo.GetByUserAsync(userId, query);
            return new PagedResult<OrderDto>
                (
                    _mapper.Map<List<OrderDto>>(result.Items),
                    result.TotalCount,
                    result.PageNumber,
                    result.PageSize
                );
        }

        public async Task<OrderDetailDto> GetOrderByIdAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException("Order not found");
            ValidateUser(order);
            return _mapper.Map<OrderDetailDto>(order);
        }

        public async Task<OrderDetailDto> GetOrderByIdForAdminAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdWithItemsAsync(orderId,true);
            if (order == null)
                throw new NotFoundException("Order not found");
            return _mapper.Map<OrderDetailDto>(order);
        }

        public async Task<OrderDetailDto> CreateOrderAsync()
        {
            var userId = _currentUser.UserId;
            var cart = await _cartRepo.GetByUserWithItemsAsync(userId);
            if (cart == null)
                throw new NotFoundException("Cart not found");
            if (!cart.CartItems.Any())
                throw new BadRequestException("Cart is empty");
            foreach(var item in cart.CartItems)
            {
                if (item.Quantity > item.Product.Stock)
                    throw new BadRequestException($"{item.Product.Name} doesn't have enough stock");
            }
            var order = new Order()
            {
                CreatedDate = DateTime.UtcNow,
                Status = OrderStatus.PendingPayment,
                UserId = userId,
                OrderItems = new List<OrderItem>()
            };
            foreach(var item in cart.CartItems)
            {
                order.OrderItems.Add(new OrderItem()
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                });
                order.TotalAmount += item.Quantity * item.UnitPrice;
                item.Product.Stock -= item.Quantity;
            }
            cart.CartItems.Clear();
            _orderRepo.Add(order);
            await _unitOfWork.SaveChangesAsync();
            var createdOrder = await _orderRepo.GetByIdWithItemsAsync(order.Id);
            return _mapper.Map<OrderDetailDto>(createdOrder);
        }

        public async Task UpdateStatusAsync(int orderId, OrderStatus status)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new NotFoundException("Order not found");
            if (order.IsDeleted)
                throw new BadRequestException("Order already deleted");
            if (order.Status == OrderStatus.Cancelled)
                throw new BadRequestException("Can't update, order already cancelled");
            if (order.Status > status)
                throw new BadRequestException("Order status can't be changed back to previous state");
            order.Status = status;
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdWithItemsAsync(orderId);
            if (order == null)
                throw new NotFoundException("Order not found");
            ValidateUser(order);
            if (order.Status == OrderStatus.Cancelled)
                throw new BadRequestException("Order already cancelled");
            if (order.Status >= OrderStatus.Shipping)
                throw new BadRequestException($"Can not cancelled order with status {order.Status}");
            order.Status = OrderStatus.Cancelled;
            foreach(var item in order.OrderItems)
            {
                item.Product.Stock += item.Quantity;
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var order = await _orderRepo.GetByIdAsync(orderId) ?? throw new NotFoundException("Order not found");
            if (order.IsDeleted)
                throw new BadRequestException("Order already deleted");
            if (order.Status < OrderStatus.Completed)
                throw new BadRequestException("Can't delete incomplete order");
            order.IsDeleted = true;
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
