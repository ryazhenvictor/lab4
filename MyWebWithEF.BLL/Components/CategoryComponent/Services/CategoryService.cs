using Microsoft.EntityFrameworkCore;
using MyWebWithEF.BLL.Components.CategoryComponent.Dtos;

public class CategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all categories
    public async Task<List<Category>> GetAllCategoriesAsync()
    {
        return await _context.Categories.Include(c => c.Products).ToListAsync();
    }

    // Get category by Id
    public async Task<Category?> GetCategoryByIdAsync(int id)
    {
        return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
    }

    // Create a new category (without saving changes)
    public Category AddCategory(CategoryEditDto model)
    {
        var category = new Category
        {
            Name = model.Name
        };
        _context.Categories.Add(category);
        return category;
    }

    // Update a category (without saving changes)
    public Category UpdateCategory(CategoryEditDto category)
    {
        var existingCategory = _context.Categories.Find(category.Id);
        if (existingCategory == null)
        {
            throw new Exception("Category not found");
        }

        existingCategory.Name = category.Name;
        return existingCategory;
    }

    // Delete a category (without saving changes)
    public bool DeleteCategory(int id)
    {
        var category = _context.Categories.Find(id);
        if (category == null)
        {
            throw new Exception("Category not found");
        }

        _context.Categories.Remove(category);
        return true;
    }
}
