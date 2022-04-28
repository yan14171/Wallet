using API.DTOs;
using AutoMapper;
using BusinessLogicLeve;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Entities;
using System.ComponentModel.DataAnnotations;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class IncomeItemsController
{
    private readonly IMapper _mapper;
    private readonly BankService _service;

    public IncomeItemsController(BankService service, IMapper mapper)
    {
        this._mapper = mapper;
        this._service = service;
    }

    [HttpGet(Name = "GetIncomeItemsByWallet")]
    public async Task<IActionResult> GetAll([FromQuery][Required] int Id)
    {
        var incomeItemQuery = _service.GetIncomesByWallet(Id);

        var incomeItems = await incomeItemQuery.ToListAsync();

        var incomeItemsDTOs = _mapper.Map<List<IncomeItem>, List<IncomeItemDTO>>(incomeItems);

        return new OkObjectResult(incomeItemsDTOs);
    }
}
