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
    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IMapper _mapper;

        public OrderDetailsController(IOrderDetailRepository orderDetailRepository, IMapper mapper)
        {
            _orderDetailRepository = orderDetailRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OrderDetailDto>> GetAll()
        {
            var orderDetails = _orderDetailRepository.GetAll();
            var orderDetailDtos = _mapper.Map<IEnumerable<OrderDetailDto>>(orderDetails);
            return Ok(orderDetailDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<OrderDetailDto> GetById(int id)
        {
            var orderDetail = _orderDetailRepository.GetById(id);
            if (orderDetail == null)
                return NotFound();

            var orderDetailDto = _mapper.Map<OrderDetailDto>(orderDetail);
            return Ok(orderDetailDto);
        }

        [HttpPost]
        public ActionResult<OrderDetailDto> Create(OrderDetailDto orderDetailDto)
        {
            var orderDetail = _mapper.Map<OrderDetail>(orderDetailDto);
            var createdOrderDetail = _orderDetailRepository.Create(orderDetail);
            var createdOrderDetailDto = _mapper.Map<OrderDetailDto>(createdOrderDetail);

            return CreatedAtAction(nameof(GetById), new { id = createdOrderDetailDto.Id }, createdOrderDetailDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, OrderDetailDto orderDetailDto)
        {
            var existingOrderDetail = _orderDetailRepository.GetById(id);
            if (existingOrderDetail == null)
                return NotFound();

            _mapper.Map(orderDetailDto, existingOrderDetail);
            _orderDetailRepository.Update(existingOrderDetail);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingOrderDetail = _orderDetailRepository.GetById(id);
            if (existingOrderDetail == null)
                return NotFound();

            _orderDetailRepository.Delete(id);
            return NoContent();
        }
    }
}
