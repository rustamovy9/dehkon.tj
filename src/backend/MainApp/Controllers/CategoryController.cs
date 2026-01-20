using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/categories")]
public sealed class CategoryController(ICategoryService service) : BaseController
{
    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryCreateInfo createInfo)
        => (await service.CreateAsync(createInfo)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] CategoryUpdateInfo updateInfo)
        => (await service.UpdateAsync(id,updateInfo)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id)).ToActionResult();

    [AllowAnonymous]
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => (await service.GetByIdAsync(id)).ToActionResult();

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery]CategoryFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();
}