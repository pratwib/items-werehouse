using ItemsWarehouse.Models;
using ItemsWarehouse.Services;

namespace ItemsWarehouse.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class GudangController : ControllerBase
{
    private readonly GudangService _gudangService;

    public GudangController(GudangService gudangService)
    {
        _gudangService = gudangService;
    }

    [HttpGet]
    public ActionResult<List<Gudang>> GetAll()
    {
        return _gudangService.GetAllGudang();
    }

    [HttpPost]
    public IActionResult Create(string gudangName)
    {
        _gudangService.AddGudang(gudangName);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, string gudangName)
    {
        _gudangService.UpdateGudang(id, gudangName);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _gudangService.DeleteGudang(id);
        return Ok();
    }
}