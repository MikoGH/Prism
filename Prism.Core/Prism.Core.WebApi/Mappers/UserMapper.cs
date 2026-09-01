using Prism.Core.WebApi.Dtos.User;
using Prism.Core.WebApi.Models;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class UserMapper
{
    public partial UserDto ToUserDto(User user);
}
