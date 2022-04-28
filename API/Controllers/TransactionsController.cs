namespace API.Controllers;
using API.DTOs;
using AutoMapper;
using BusinessLogicLeve;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projects.Entities;
using System.ComponentModel.DataAnnotations;


[ApiController]
[Route("[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly BankService _service;

    public TransactionsController(BankService service, IMapper mapper)
    {
        this._mapper = mapper;
        this._service = service;
    }

    [HttpGet(Name = "GetTransactionsByIncomeItem")]
    public async Task<IActionResult> Get([FromQuery][Required]int itemId)
    {
        var transactionQuery = _service.GetTransactionsByIncomeItem(itemId);

        var transactions = await transactionQuery.Include(n => n.IncomeItem).ToListAsync();

        var transactionsDTO = _mapper.Map<List<Transaction>, List<TransactionDTO>>(transactions);

        return Ok(transactionsDTO);
    }

    [HttpPost(Name = "Make a transaction")]
    public IActionResult MakeTransaction([FromBody] TransactionDTO transaction)
    {
        _service.IncomeChange(transaction.AccountId, transaction.IncomeItemId, (int)transaction.Sum);
        _service.Save();
        return Created(Request.Path, transaction);
    }
}
