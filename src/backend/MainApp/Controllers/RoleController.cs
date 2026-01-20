using Application.Contracts.IServices;
using Application.DTO_s;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/roles")]
[Authorize(Roles = DefaultRoles.Admin)]
public sealed class RoleController(IRoleService service) : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RoleCreateInfo createInfo)
        => (await service.CreateAsync(createInfo)).ToActionResult();

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] RoleUpdateInfo updateInfo)
        => (await service.UpdateAsync(id, updateInfo)).ToActionResult();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id)).ToActionResult();

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
        => (await service.GetByIdAsync(id)).ToActionResult();

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => (await service.GetAllAsync()).ToActionResult();
}