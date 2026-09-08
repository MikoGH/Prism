using Flequery.Extensions;
using Flequery.Models;
using Microsoft.AspNetCore.Mvc;
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

    [HttpPost("filter")]
    public async Task<ActionResult<ThemeFullDto>> FilterAsync(QueryRequest request, [FromBody] FilterRequest filters, CancellationToken token)
    {
        request.AddFilters(filters.Filters);

        //var themeVersionPagedResponse = _themeService.FilterAsync(request, token);
        //var themeFieldRequest = request;  // TODO: clone

        //themeFieldRequest.Paging.PerPage = -1;
        //themeFieldRequest.Paging.Page = 1;
        //themeFieldRequest.Paging.Skip = 0;
        //var themeFieldVersion = _themeFieldService.FilterAsync(themeFieldRequest, token);

        return Ok(new ThemeFullDto());
    }

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
