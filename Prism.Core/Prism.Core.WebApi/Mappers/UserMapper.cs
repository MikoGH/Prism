using Prism.Core.Domain.Models;
using Prism.Core.WebApi.Dtos.User;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserDto ToUserDto(User user);
}
