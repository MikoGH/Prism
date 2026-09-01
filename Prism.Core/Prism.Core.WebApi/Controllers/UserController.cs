using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.User;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Models.Filters;
using Prism.Core.WebApi.Repositories.Abstractions;

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

    public async Task<ActionResult<IEnumerable<UserDto>>> FilterUsersAsync(
        [FromBody] PagingModel paging,
        [FromBody] UserFilter filter,
        CancellationToken token)
    {
        var users = await _userRepository.FilterAsync(paging, filter, token);

        var userDtos = users.Select(x => _mapper.ToUserDto(x)).ToList();

        return Ok(userDtos);
    }
}
