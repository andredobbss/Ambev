using Ambev.Api.Controllers.Base;
using Ambev.Application.DTOs;
using Ambev.Application.Enums;
using Ambev.Application.Shared;
using Ambev.Application.User.Commands;
using Ambev.Application.User.Queries;
using Ambev.Domain.Resourcers;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Ambev.Api.Controllers;


public class UsersController : BaseController
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetUserByFilterPagedAsync")]
    public async Task<IActionResult> GetUserByFilterPagedAsync(
        [FromQuery] EUserFilterType filterType,
        [FromQuery] string filterValue,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string orderBy = null)
    {
        object convertedFilterValue = ConvertFilterValue(filterType, filterValue);

        var query = new GetUserByFilterQuery(convertedFilterValue, filterType, pageNumber, pageSize, orderBy);

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpGet("GetUserByIdAsync/{id}")]
    public async Task<IActionResult> GetUserByIdAsync([FromRoute] int id)
    {
        var query = new GetUserByIdQuery { Id = id };

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPost("RegisterUserAsync")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] CreateUserCommand command)
    {
        var result = await _mediator.Send(command);
        //return CreatedAtAction(nameof(GetUserByIdAsync), new { id = result.Id }, result);
        return Created($"v1/api/users/{result.Id}", result);
    }

    [HttpPost("LoginAsync")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
            return BadRequest(ResourceMessagesException.INVALID_LOGIN);

        var authenticationUser = await _mediator.Send(new AuthenticationUserCommand
        {
            Username = loginRequest.Username,
            Password = loginRequest.Password
        });

        return Ok(authenticationUser);
    }

    [HttpPut("UpdateUserAsync/{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] int id, [FromBody] UpdateUserCommand command)
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

    [HttpDelete("DeleteUserAsync/{id}")]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
    {
        var result = await _mediator.Send(new DeleteUserCommand { Id = id });

        return Ok(result);
    }

    private object ConvertFilterValue(EUserFilterType filterType, string filterValue)
    {
        return filterType switch
        {
            EUserFilterType.UserName => filterValue.ToString(),
            EUserFilterType.Phone => filterValue.ToString(),
            EUserFilterType.Email => filterValue.ToString(),
            _ => throw new KeyNotFoundException(ResourceMessagesException.INVALID_FILTER)
        };
    }

}
