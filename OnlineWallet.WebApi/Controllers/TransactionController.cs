using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineWallet.Application.Common.DTOs.Transaction;
using OnlineWallet.Application.Transactions.Commands.AddTransaction;
using OnlineWallet.Application.Transactions.Queries.GetAllTransactions;
using OnlineWallet.Application.Transactions.Queries.GetTransaction;
using OnlineWallet.WebApi.Extensions;

namespace OnlineWallet.WebApi.Controllers
{
    public class TransactionController : BaseController
    {
        public TransactionController(ISender mediator) : base(mediator) { }

        [Authorize]
        [HttpPost("transfer-funds")]
        public async Task<IActionResult> TransferFunds([FromForm] TransferFundsTransactionModel model)
        {
            var userId = HttpContext.GetUserId();
            var command = new AddTransferFundsTransaction(userId, model.ReceiverUserId, model.SenderWalletCode, model.ReceiverWalletCode, model.Currency, model.Amount);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("deposit-funds")]
        public async Task<IActionResult> DepositFunds([FromForm] DepositFundsTransactionModel model)
        {
            var userId = HttpContext.GetUserId();
            var command = new AddDepositFundsTransaction(userId, model.WalletCode, model.Currency, model.Amount);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpPost("withdraw-funds")]
        public async Task<IActionResult> AddWithdrawFundsTransaction([FromForm] WithdrawFundsTransactionModel model)
        {
            var userId = HttpContext.GetUserId();
            var command = new AddWithdrawFundsTransaction(userId, model.WalletCode, model.Currency, model.Amount);
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-transaction-by-id")]
        public async Task<IActionResult> GetTransaction(Guid transactionId)
        {
            var userId = HttpContext.GetUserId();
            var query = new GetTransactionQuery(userId, transactionId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("get-all-transactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var userId = HttpContext.GetUserId();
            var query = new GetAllTransactionsQuery(userId);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
