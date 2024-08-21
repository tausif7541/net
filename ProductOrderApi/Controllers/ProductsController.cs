using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ProductOrderApi.Entities;
using ProductOrderApi.Models;
using ProductOrderApi.Repositories;

namespace ProductOrderApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        public ProductsController(IProductRepository productRepository, IMapper mapper, IMemoryCache memoryCache)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll()
        {
            var cacheKey = "Products";
            if (_memoryCache.TryGetValue(cacheKey, out IEnumerable<Product> product))
            {
                return Ok(product);
            }

            var products = _productRepository.GetAll();
            _memoryCache.Set(cacheKey, products, TimeSpan.FromMinutes(5)); // Adjust expiration time as needed
            var productDtos = _mapper.Map<IEnumerable<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public ActionResult<ProductDto> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
                return NotFound();

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public ActionResult<ProductDto> Create(ProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var createdProduct = _productRepository.Create(product);
            var createdProductDto = _mapper.Map<ProductDto>(createdProduct);

            return CreatedAtAction(nameof(GetById), new { id = createdProductDto.Id }, createdProductDto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductDto productDto)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
                return NotFound();

            _mapper.Map(productDto, existingProduct);
            _productRepository.Update(existingProduct);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
                return NotFound();

            _productRepository.Delete(id);
            return NoContent();
        }
    }
}
