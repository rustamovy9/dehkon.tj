using Application.Contracts.IServices;
using Application.DTO_s;
using Application.Filters;
using Domain.Constants;
using MainApp.HelpersApi.Extensions.ResultPattern;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MainApp.Controllers;

[ApiController]
[Route("api/products")]
public sealed class ProductController(IProductService service) : BaseController
{
    private int UserId =>
        int.Parse(User.FindFirst(CustomClaimTypes.Id)?.Value
                  ?? throw new UnauthorizedAccessException("UserId not found"));

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ProductFilter filter)
        => (await service.GetAllAsync(filter)).ToActionResult();

    [HttpGet("{id;int}")]
    public async Task<IActionResult> GetById(int id)
        => (await service.GetByIdAsync(id)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin + "," + DefaultRoles.Seller)]
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] ProductCreateInfo createInfo)
        => (await service.CreateAsync(UserId,createInfo)).ToActionResult();
    
    [Authorize(Roles = DefaultRoles.Admin + "," + DefaultRoles.Seller)]
    [HttpPut]
    public async Task<IActionResult> Update(int id,[FromForm] ProductUpdateInfo updateInfo)
        => (await service.UpdateAsync(id,UserId,updateInfo)).ToActionResult();

    [Authorize(Roles = DefaultRoles.Admin + "," + DefaultRoles.Seller)]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => (await service.DeleteAsync(id,UserId)).ToActionResult();
}