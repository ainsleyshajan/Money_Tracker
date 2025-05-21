using Microsoft.AspNetCore.Mvc;
using Moneytracker.Contracts;
using Moneytracker.Models;
using System;
using System.Threading.Tasks;

namespace Moneytracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DebtController : ControllerBase
    {
        private readonly IDebtService _debtService;

        public DebtController(IDebtService debtService)
        {
            _debtService = debtService;
        }

        [HttpPost("getAll")]
        public async Task<IActionResult> GetAllDebts([FromQuery] string name)
        {
            var debts = await _debtService.GetAllDebtsAsync(name);
            return Ok(debts);
        }

        [HttpPost("getByDate")]
        public async Task<IActionResult> GetDebtsByDate(
            [FromQuery] DateTime fromDate,
            [FromQuery] DateTime toDate)
        {
            var debts = await _debtService.GetDebtsByDateAsync(fromDate, toDate);
            return Ok(debts);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateDebt(
            [FromQuery] string name,
            [FromQuery] DateTime date,
            [FromQuery] decimal amount,
            [FromQuery] string category,
            [FromQuery] DateTime deadline,
            [FromQuery] string description)
        {
            var debt = new DebtModel
            {
                Name = name,
                Date = date,
                Amount = amount,
                Category = category,
                Deadline = deadline,
                Description = description
            };

            await _debtService.CreateDebtAsync(debt);
            return Ok(new { message = "Debt created successfully." });
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateDebt(
            [FromQuery] int debtId,
            [FromQuery] string name,
            [FromQuery] DateTime date,
            [FromQuery] decimal amount,
            [FromQuery] string category,
            [FromQuery] DateTime deadline,
            [FromQuery] string description)
        {
            var debt = new DebtModel
            {
                DebtId = debtId,
                Name = name,
                Date = date,
                Amount = amount,
                Category = category,
                Deadline = deadline,
                Description = description
            };

            await _debtService.UpdateDebtAsync(debt);
            return Ok(new { message = "Debt updated successfully." });
        }

        [HttpPost("delete")]
        public async Task<IActionResult> DeleteDebt([FromQuery] DateTime date, [FromQuery] string name)
        {
            await _debtService.DeleteDebtAsync(date, name);
            return Ok(new { message = "Debt deleted successfully." });
        }
    }
}