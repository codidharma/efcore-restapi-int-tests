using System.Net.NetworkInformation;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.MappingExtensions;

public static class ExpenseCategoriesExtensions
{
    public static ExpenseCategory ToEntity(this CreateExpenseCategoryRequestDto createExpenseCategoryRequestDto)
    {
        var entity = new ExpenseCategory
        {
            Name = createExpenseCategoryRequestDto.Name,
            Description = createExpenseCategoryRequestDto.Description
        };

        return entity;
    }

    public static GetExpenseCategoryByIdResponseDto ToGetExpenseCategoryByIdResponseDto(this ExpenseCategory expenseCategory)
    {
        var dto = new GetExpenseCategoryByIdResponseDto(expenseCategory.ID,
                                                expenseCategory.Name,
                                                expenseCategory.Description);
        return dto;
    }

    public static GetAllExpenseCategoriesResponseDto ToGetAllExpenseCategoriesResponseDto(this ExpenseCategory expenseCategory)
    {
        var dto = new GetAllExpenseCategoriesResponseDto(expenseCategory.ID,
                                                 expenseCategory.Name,
                                                 expenseCategory.Description);

        return dto;
    }

    public static CreateExpenseCategoryResponseDto ToCreateExpenseCategoryResponseDto(this ExpenseCategory expenseCategory)
    {
        var dto = new CreateExpenseCategoryResponseDto(expenseCategory.ID,
                                                       expenseCategory.Name,
                                                       expenseCategory.Description);

        return dto;
    }

}
