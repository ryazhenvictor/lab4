using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MyWebWithEF.BLL.Components.CategoryComponent.Dtos;

namespace MyWebWithEF.Controllers.User.Base
{
    public class CategoriesController : UserApiController
    {
        private readonly CategoryService _categoryService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoriesController(CategoryService categoryService, ApplicationDbContext context, IMapper mapper)
        {
            _categoryService = categoryService;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }
    }
}