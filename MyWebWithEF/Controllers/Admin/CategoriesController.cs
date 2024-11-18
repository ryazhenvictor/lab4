using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MyWebWithEF.BLL.Components.CategoryComponent.Dtos;

namespace MyWebWithEF.Controllers.Admin.Base
{
    public class CategoriesController : AdminApiController
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

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory(CategoryEditDto model)
        {
            var category = _categoryService.AddCategory(model);

            await _context.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryEditDto model)
        {
            model.Id = id;

            var category = _categoryService.UpdateCategory(model);

            await _context.SaveChangesAsync();

            var categoryDto = _mapper.Map<CategoryDto>(category);
            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var deleted = _categoryService.DeleteCategory(id);
            if (!deleted)
            {
                return NotFound();
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}