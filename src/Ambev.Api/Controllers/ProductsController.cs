using Ambev.Api.Controllers.Base;
using Ambev.Application.Enums;
using Ambev.Application.Product.Commands;
using Ambev.Application.Product.Queries;
using Ambev.Application.Shared;
using Ambev.Domain.Resourcers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers;


public class ProductsController : BaseController
{
    private readonly IMediator _mediator;

    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtém a lista de produtos aplicando filtros dinâmicos.
    /// </summary>
    /// <param name="filters">Dicionário de filtros passados na query string.</param>
    /// <returns>Uma lista de produtos filtrados.</returns>
    /// <response code="200">Retorna a lista de produtos filtrados.</response>
    /// <response code="400">Requisição inválida (exemplo: formato incorreto de um filtro).</response>
    [HttpGet ("GetProductsEspecialFilterAsync")]
    public async Task<IActionResult> GetProductsEspecialFilterAsync([FromQuery] Dictionary<string, string> filters)
    {
        var query = new GetProductsByEspcialFilterQuery(filters);

        var products = await _mediator.Send(query);

        return Ok(products);
    }


    [HttpGet("GetProductsByFilterPagedAsync")]
    public async Task<IActionResult > GetCartsByFilterPagedAsync(
        [FromQuery] EProductFilterType filterType,
        [FromQuery] string filterValue,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string orderBy = null)
    {
        object convertedFilterValue = ConvertFilterValue(filterType, filterValue);

        var query = new GetProductsByFilterQuery(convertedFilterValue, filterType, pageNumber, pageSize, orderBy);

        var products = await _mediator.Send(query);

        return Ok(products);
    }


    [HttpGet("GetPrudctByIdAsync/{id}")]
    public async Task<IActionResult> GetPrductByIdAsync([FromRoute] int id)
    {
        var query = new GetProductByIdQuery { Id = id };

        var result = await _mediator.Send(query);

        return Ok(result);
    }


    [HttpPost("AddProductAsync")]
    public async Task<IActionResult> AddProductAsync([FromBody] CreateProductCommand command)
    {
        var result = await _mediator.Send(command);
        //return CreatedAtAction(nameof(GetPrductByIdAsync), new { id = result.Id }, result);
        return Created($"v1/api/products/{result.Id}", result);
    }

    [HttpPut("UpdateProductAsync/{id}")]
    public async Task<IActionResult> UpdateProductAsync([FromRoute] int id, [FromBody] UpdateProductCommand command)
    {
        if (id != command.Id)
            return BadRequest(new ApiErrorResponse(
                ResourceMessagesException.ERROR_TYPE_BAD_REQUEST,
                ResourceMessagesException.ERROR_ID,
               $"{ResourceMessagesException.ERROR_DETAIL_ID} {id}"
            ));

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete("DeletePoductAsync/{id}")]
    public async Task<IActionResult> DeleteProductAsync([FromRoute] int id)
    {
        var reult = await _mediator.Send(new DeleteProductCommand { Id = id });

        return Ok(reult);
    }

    private object ConvertFilterValue(EProductFilterType filterType, string filterValue)
    {
        return filterType switch
        {
            EProductFilterType.Description => filterValue.ToString(),
            EProductFilterType.Title => filterValue.ToString(),
            EProductFilterType.Category => filterValue.ToString(),
            EProductFilterType.Price => decimal.Parse(filterValue),
            _ => throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER)
        };
    }
}
