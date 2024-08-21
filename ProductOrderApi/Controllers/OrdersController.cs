using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductOrderApi.Entities;
using ProductOrderApi.Models;
using ProductOrderApi.Repositories;
using System.Collections.Generic;

namespace ProductOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDto>> GetAll()
        {
            var orders = _orderRepository.GetAll();
            var orderDtos = _mapper.Map<IEnumerable<OrderDto>>(orders);
            return Ok(orderDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDto> GetById(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
                return NotFound();

            var orderDto = _mapper.Map<OrderDto>(order);
            return Ok(orderDto);
        }

        [HttpPost]
        public ActionResult<OrderDto> Create(OrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            // Set the OrderId for each OrderDetail
            foreach (var detail in order.OrderDetails)
            {
                detail.OrderId = order.Id;
                detail.UnitPrice = detail.Product?.Price ?? 0;
            }

            order.TotalAmount = order.OrderDetails.Sum(d => d.Quantity * d.UnitPrice);
            var createdOrder = _orderRepository.Create(order);
            var createdOrderDto = _mapper.Map<OrderDto>(createdOrder);

            return CreatedAtAction(nameof(GetById), new { id = createdOrderDto.Id }, createdOrderDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDto orderDto)
        {
            var existingOrder = _orderRepository.GetById(id);
            if (existingOrder == null)
                return NotFound();

            _mapper.Map(orderDto, existingOrder);
            existingOrder.TotalAmount = existingOrder.OrderDetails.Sum(d => d.Quantity * d.UnitPrice);
            _orderRepository.Update(existingOrder);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingOrder = _orderRepository.GetById(id);
            if (existingOrder == null)
                return NotFound();

            _orderRepository.Delete(id);
            return NoContent();
        }
    }
}
