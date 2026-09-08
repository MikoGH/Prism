using Microsoft.AspNetCore.Mvc;
using Monq.Core.Paging.Models;
using Prism.Core.Domain.Models.Filters;
using Prism.Core.WebApi.Constants;
using Prism.Core.WebApi.Dtos.Theme;
using Prism.Core.WebApi.Mappers;
using Prism.Core.WebApi.Services.Contracts;

namespace Prism.Core.WebApi.Controllers;

[Controller]
[Route($"{AppConstants.RoutePrefix}/[controller]")]
public class ThemeController : ControllerBase
{
    private readonly ThemeMapper _mapper;
    private readonly IThemeService _themeService;
    private readonly IThemeFieldService _themeFieldService;

    public ThemeController(ThemeMapper mapper, IThemeService themeService, IThemeFieldService themeFieldService)
    {
        _mapper = mapper;
        _themeService = themeService;
        _themeFieldService = themeFieldService;
    }

    //public async Task<ActionResult<ThemeFullDto>> GetAsync([FromQuery]PagingModel paging, [FromBody]ThemeQueryDto themeQuery, CancellationToken token)
    //{
    //    var themeVersionFilter = _mapper.ToThemeVersionFilter(themeQuery.ThemeVersionFilter);
    //    var themeFieldVersionFilter= _mapper.ToThemeFieldVersionFilter(themeQuery.ThemeFieldVersionFilter);
    //    var themeVersionInclude = _mapper.ToThemeVersionInclude(themeQuery.ThemeVersionInclude);
    //    var themeFieldVersionInclude = _mapper.ToThemeFieldVersionInclude(themeQuery.ThemeFieldVersionInclude);

    //    var themeVersion = _themeService.FilterAsync(themeVersionFilter, paging, themeVersionInclude, token);
    //    var themeFieldPaging = new PagingModel
    //    {
    //        PerPage = PagingModel.ALL_ITEMS_PER_PAGE
    //    };
    //    var themeFieldVersion = _themeFieldService.FilterAsync(themeFieldVersionFilter, themeFieldPaging, themeFieldVersionInclude, token);
    //}

    [HttpPost("add")]
    public async Task<ActionResult> AddAsync([FromBody] AddThemeFullDto themeFullDto, CancellationToken token)
    {
        var themeVersion = _mapper.ToThemeVersion(themeFullDto.ThemeVersion, themeFullDto.UserId);

        var addedThemeVersion = await _themeService.AddAsync(themeVersion, token);
        if (addedThemeVersion is null)
            return NotFound();

        var themeFieldVersions = themeFullDto.ThemeFieldVersions
            .Select(themeFieldVersionDto => _mapper.ToThemeFieldVersion(themeFieldVersionDto, themeFullDto.UserId, addedThemeVersion.ThemeId))
            .ToList();

        await _themeFieldService.BatchAsync(themeFieldVersions, addedThemeVersion.ThemeId, token);

        return Ok();
    }
}
