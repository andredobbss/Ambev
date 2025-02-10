using Ambev.Application.Enums;
using Ambev.Application.Interfaces;
using Ambev.Domain.Entities;
using Ambev.Domain.IUnitOfWork;
using Ambev.Domain.Resourcers;
using System.Linq.Expressions;

namespace Ambev.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<IEnumerable<ProductDomain>> EspecialFiltersToProductsAsync(Dictionary<string, string> filters)
    {
        var products = await _unitOfWork.productRepository.EspecialFiltersAsync(filters);

        return products;
    }

    public async Task<(IEnumerable<ProductDomain> Data, int TotalItems, int TotalPages)> GetProductsByFilterToPagedListAsync(object filterValue, EProductFilterType filterType, int pageNumber, int pageSize, string orderBy = null)
    {
        Expression<Func<ProductDomain, bool>> filterExpression = u => true;

        switch (filterType)
        {
            case EProductFilterType.Category when filterValue is string categoryValue:
                filterExpression = p => p.Category == categoryValue;
                break;

            case EProductFilterType.Description when filterValue is string descriptionValue:
                filterExpression = p => p.Description == descriptionValue;
                break;

            case EProductFilterType.Price when filterValue is decimal priceValue:
                filterExpression = p => p.Price == priceValue;
                break;

            case EProductFilterType.Title when filterValue is string titledValue:
                filterExpression = p => p.Title == titledValue;
                break;

            default:
                throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER);
        }

        var products = await _unitOfWork.productRepository.GetToPagedListAsync(filterExpression, pageNumber, pageSize, orderBy);

        return (products);
    }

    public async Task<ProductDomain> GetProductByIdAsync(int id)
    {
        var product = await _unitOfWork.productRepository.GetSingleAsync(p => p.Id == id);

        return product;
    }


    public async Task<ProductDomain> AddProductAsync(ProductDomain productDomain)
    {
       await  _unitOfWork.productRepository.Add(productDomain);

        await _unitOfWork.Commit();

        return productDomain;
    }

    public async Task<ProductDomain> UpdateProductAsync(ProductDomain productDomain)
    {
        await _unitOfWork.productRepository.Update(productDomain);

        await _unitOfWork.Commit();

        return productDomain;
    }

    public async Task<ProductDomain> DeleteProductAsync(int id)
    {
        var productResult = await _unitOfWork.productRepository.GetSingleAsync(p => p.Id == id);

        if (productResult is null)
            return null;

        await _unitOfWork.productRepository.Delete(productResult);

        await _unitOfWork.Commit();

        return productResult;
    }

}
