using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace MyWebWithEF.Controllers.User.Base
{
    public class ProductsController : UserApiController
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
    }
}
