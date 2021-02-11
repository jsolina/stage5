using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoAppAPI.Controllers.Base;
using Microsoft.AspNetCore.Authorization;

using TodoAppAPI.Application.Commands.Idempotency;
using System.ComponentModel.DataAnnotations;
using TodoAppAPI.Application.Queries.ItemList;
using TodoAppAPI.Application.Commands.ItemList;


namespace TodoAppAPI.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/itemlist")]
    [ApiController]

    public class ItemListController : BaseController
    {
        private readonly IItemListQueries _itemListQueries;
        public ItemListController(IItemListQueries itemListQueries) : base()
        {
            _itemListQueries = itemListQueries;
        }

        /// <summary>
        /// Get all ItemList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllItemList()
        {
            var accounts = await _itemListQueries.GetAllItemListAsync();

            return Ok(accounts);
        }

        /// <summary>
        /// Get specific Fee Scheme
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var feeSchemes = await _itemListQueries.GetItemListAsyncById(id);

            return Ok(feeSchemes);
        }

        /// <summary>   
        /// Create new TaskList
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateItem([FromBody] AddItemListCommand command)
        {
            //command.User = User.Identity.ConvertToAuthUser();
            var result = await Mediator.Send(command);

            return CreatedAtAction("ItemList", null);
        }

        /// <summary>   
        /// Create new TaskList
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("withkey")]
        [Authorize]
        public async Task<IActionResult> CreateItemWithKey([FromHeader(Name = "x-idempotency-key"), Required] string idempotencyKey, [FromBody] AddItemListDTO dto)
        {
            var command = new AddItemListCommand();
            command.IdTask = dto.IdTask;
            command.ItemName = dto.ItemName;
            command.ItemDetails = dto.ItemDetails;
            command.ItemStatus = dto.ItemStatus;

            var idempotenctCommand = new IdempotentCommand<AddItemListCommand, AddItemListResult>(command, idempotencyKey);

            //var result = await Mediator.Send(command);
            var result = await Mediator.Send(idempotenctCommand);

            return CreatedAtAction("ItemList", result);
            //return CreatedAtAction("TaskList", result);
        }


        /// <summary>
        /// Update a specific TaskList
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateItem([FromRoute] int id, [FromBody] UpdateItemListCommand command)
        {
            command.Id = id;

            var result = await Mediator.Send(command);

            return Ok();
        }

        /// <summary>
        /// Delete specific batch
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBatch([FromRoute] int id)
        {
            DeleteItemListCommand command = new DeleteItemListCommand
            {
                Id = id,
            };

            await Mediator.Send(command);

            return Ok();
        }

    }
}
