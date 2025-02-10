using Ambev.Api.Controllers.Base;
using Ambev.Application.Cart.Commands;
using Ambev.Application.Cart.Queries;
using Ambev.Application.Enums;
using Ambev.Application.Shared;
using Ambev.Domain.Resourcers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.Api.Controllers;


public class CartsController : BaseController
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
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
    [HttpGet("GetCartsByEspecialFilersAsync")]
    public async Task<IActionResult> GetCartsByEspecialFilersAsync([FromQuery] Dictionary<string, string> filters)
    {
        var query = new GetCartsByEspecialFiltersQuery(filters);

        var carts = await _mediator.Send(query);

        return Ok(carts);
    }

   
    [HttpGet("GetCartsByFilterPagedAsync")]
    public async Task<IActionResult> GetCartsByFilterPagedAsync(
        [FromQuery] ECartFilterType filterType,
        [FromQuery] string filterValue,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string orderBy = null)
    {    
            object convertedFilterValue = ConvertFilterValue(filterType, filterValue);

            var query = new GetCartsByFilterQuery(convertedFilterValue, filterType, pageNumber, pageSize, orderBy);

            var result = await _mediator.Send(query);

            return Ok(result);    
    }

    [HttpGet("GetCartByIdAsync/{id}")]
    public async Task<IActionResult> GetCartByIdAsync([FromRoute] int id)
    {
        var query = new GetCartByIdQuery { Id = id };

        var result = await _mediator.Send(query);

        return Ok(result);
    }


    [HttpPost("AddCartAsync")]
    public async Task<IActionResult> AddCartAsync([FromBody] CreateCartCommand command)
    {
        var result = await _mediator.Send(command);

        //return CreatedAtAction(nameof(GetCartByIdAsync), new { id = result.Id }, result);
        return Created($"v1/api/carts/{result.Id}", result);
    }

    [HttpPut("UpdateCartAsync/{id}")]
    public async Task<IActionResult> UpdateCartAsync([FromRoute] int id, [FromBody] UpdateCartCommand command)
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

    [HttpDelete("DeleteCartAsync/{id}")]
    public async Task<IActionResult> DeleteCartAsync([FromRoute] int id)
    {
        var reult = await _mediator.Send(new DeleteCartCommand { Id = id });

        return Ok(reult);
    }


    private object ConvertFilterValue(ECartFilterType filterType, string filterValue)
    {
        return filterType switch
        {
            ECartFilterType.Cancel => bool.Parse(filterValue),
            ECartFilterType.Date => DateTime.Parse(filterValue),
            ECartFilterType.UserId => int.Parse(filterValue),
            _ => throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER)
        };
    }
}
