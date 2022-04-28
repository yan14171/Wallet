namespace API.Controllers;
using API.DTOs;
using AutoMapper;
using BusinessLogicLeve;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Entities;


[ApiController]
[Route("[controller]")]
public class WalletsController
{
    private readonly IMapper _mapper;
    private readonly BankService _service;

    public WalletsController(BankService service, IMapper mapper)
    {
        this._mapper = mapper;
        this._service = service;
    }
    [HttpGet(Name = "GetWallets")]
    public async Task<IActionResult> GetAll()
    {
        var walletQuery = _service.GetAllWallets();
        var wallets = await walletQuery.ToListAsync();

        var walletDTOs = _mapper.Map<List<Account>, List<AccountDTO>>(wallets);

        return new OkObjectResult(walletDTOs);
    }

    [HttpPatch(Name = "Make a Payment")]
    public async Task<IActionResult> Payment(
        [FromQuery] int fromId, 
        [FromQuery] int toId, 
        [FromBody] int sum)
    {
        _service.Transaction(fromId, toId, sum);
        _service.Save();

        return new OkObjectResult(new {from = fromId, to = toId, sum = sum});
    }
} 
