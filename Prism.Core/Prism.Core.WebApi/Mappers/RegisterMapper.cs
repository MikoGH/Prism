using Prism.Core.WebApi.Dtos.Auth;
using Prism.Core.WebApi.Models;
using Riok.Mapperly.Abstractions;

namespace Prism.Core.WebApi.Mappers;

[Mapper]
public partial class RegisterMapper
{
    public partial RegisterUser ToRegisterUser(RegisterDto authDto);
}
