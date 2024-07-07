using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.MappingExtensions;

namespace WebApi.Apis;
public static class ExpenseTypeApiEndpoints
{
    public static void RegisterExpenseCategoryEndpoints(this IEndpointRouteBuilder routeBuilder)
    {
        var routeGroup = routeBuilder.MapGroup("/expense-categories");

        routeGroup.MapGet("/", GetAllExpenseCategories);
        routeGroup.MapGet("/{id}", GetExpenseCategoryById);
        routeGroup.MapPost("/", CreateExpenseCategory);
        routeGroup.MapPut("/{id}", UpdateExpenseCategory);
        routeGroup.MapDelete("/{id}", DeleteExpenseCategory);

        routeGroup
            .WithOpenApi()
            .WithDisplayName("Expense Category")
            .WithTags("Expense Category");
    }


    public static async Task<IResult> GetAllExpenseCategories(ExpenseManagerContext dbContext)
    {
        var expenseCategories = await dbContext.ExpenseCategories.ToListAsync();

        var dto = expenseCategories.Select(x => x.ToGetAllExpenseCategoriesResponseDto());

        return TypedResults.Ok(dto);
    }

    public static async Task<IResult> CreateExpenseCategory(CreateExpenseCategoryRequestDto requestDto, ExpenseManagerContext dbContext)
    {
        var entity = requestDto.ToEntity();
        dbContext.ExpenseCategories.Add(entity);
        await dbContext.SaveChangesAsync();

        var responseDto = entity.ToCreateExpenseCategoryResponseDto();

        return TypedResults.Created($"{responseDto.Id}", responseDto);
    }

    public static async Task<IResult> GetExpenseCategoryById(int id, ExpenseManagerContext dbContext)
    {
        var entity = await dbContext.ExpenseCategories.FindAsync(id);

        return entity is null ? TypedResults.NotFound() : TypedResults.Ok(entity.ToGetExpenseCategoryByIdResponseDto());
    }

    public static async Task<IResult> UpdateExpenseCategory(int id, UpdateExpenseCategoryRequestDto requestDto, ExpenseManagerContext dbContext)
    {
        var existingEntity = await dbContext.ExpenseCategories.FindAsync(id);

        if (existingEntity is null)
        {
            return TypedResults.NotFound();
        }

        existingEntity.Description = requestDto.Description;
        existingEntity.Name = requestDto.Name;

        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

    public static async Task<IResult> DeleteExpenseCategory(int id, ExpenseManagerContext dbContext)
    {
        var entity = await dbContext.ExpenseCategories.FindAsync(id);

        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        dbContext.ExpenseCategories.Remove(entity);
        await dbContext.SaveChangesAsync();

        return TypedResults.NoContent();
    }

}
