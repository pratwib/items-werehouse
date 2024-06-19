using ItemsWarehouse.Models;
using ItemsWarehouse.Services;

namespace ItemsWarehouse.Controllers;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

[Route("api/[controller]")]
[ApiController]
public class BarangController : ControllerBase
{
    private readonly BarangService _barangService;

    public BarangController(BarangService barangService)
    {
        _barangService = barangService;
    }

    [HttpGet]
    public ActionResult<List<Barang>> GetAll()
    {
        return _barangService.GetAllBarang();
    }

    [HttpPost]
    public IActionResult Create([FromBody] Barang barang)
    {
        _barangService.AddBarang(barang.NamaBarang, barang.HargaBarang, barang.JumlahBarang, barang.ExpiredBarang,
            barang.KodeGudang);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] Barang barang)
    {
        _barangService.UpdateBarang(id, barang.NamaBarang, barang.HargaBarang, barang.JumlahBarang,
            barang.ExpiredBarang, barang.KodeGudang);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _barangService.DeleteBarang(id);
        return Ok();
    }

    [HttpPost("filter")]
    public IActionResult FilterBarangWithPaging([FromBody] FilterBarangRequest request)
    {
        var result =
            _barangService.FilterBarangWithPaging(request.Page, request.PageSize, request.GudangName,
                request.ExpiredDate);
        return Ok(result);
    }
}

public class FilterBarangRequest
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public string GudangName { get; set; }
    public DateTime ExpiredDate { get; set; }
}