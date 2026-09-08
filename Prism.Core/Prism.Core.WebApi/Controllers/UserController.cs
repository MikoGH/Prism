using Flequery.Extensions;
using Flequery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prism.Core.Domain.Contracts;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.User;
using Prism.Core.WebApi.Mappers;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
[Authorize(Roles = "Admin")]
public class UserController : ControllerBase
{
    private readonly UserMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserController(UserMapper mapper, IUserRepository userRepository)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task<ActionResult<IEnumerable<UserDto>>> FilterUsersAsync(QueryRequest request, [FromBody] FilterRequest filter, CancellationToken token)
    {
        request.AddFilters(filter.Filters);

        var usersPagedResponse = await _userRepository.FilterAsync(request, token);

        var userDtos = usersPagedResponse.Records.Select(x => _mapper.ToUserDto(x)).ToList();

        HttpContext.SetPagingHeaders(usersPagedResponse.Headers);

        return Ok(userDtos);
    }
}
