using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MyWebWithEF.Controllers.Admin.Base
{
    public class ProductsController : AdminApiController
    {
        private readonly ProductService _productService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ProductService productService, ApplicationDbContext context, IMapper mapper)
        {
            _productService = productService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return Ok(productDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDto>> CreateProduct(ProductEditDto model)
        {
            var product = await _productService.AddProduct(model);

            await _context.SaveChangesAsync();

            var productDto = _mapper.Map<ProductDto>(product);
            return Ok(productDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductEditDto model)
        {
            model.Id = id;

            var product = _productService.UpdateProduct(model);

            await _context.SaveChangesAsync();

            var categoryDto = _mapper.Map<ProductDto>(product);
            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var deleted = _productService.DeleteProduct(id);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}