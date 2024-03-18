﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OutHouse.Server.Service.Mappers;
using OutHouse.Server.Service.Services;
using OutHouse.Server.Infrastructure;

namespace OutHouse.Server.Presentation.Controllers
{
    [Authorize]
    [Route("api/outhouses")]
    public class OuthouseController(
        ILogger<OuthouseController> logger,
        ApplicationDbContext context)
            : ApplicationBaseController
    {
        private readonly ILogger<OuthouseController> _logger = logger;

        private OuthouseService Service => new(context, UserContext);

        [HttpPost("")]
        public async Task<ActionResult<OuthouseDto>> CreateNew(CreateNewOuthouseRequest request)
        {
            return await ExecuteWithExceptionHandling(
                Service.CreateNewOuthouseAsync(request),
                new CreatedAtActionResultFactory<OuthouseDto>(nameof(Get), this));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OuthouseDto>> Get(Guid id)
        {
            return await ExecuteWithExceptionHandling(
                Service.GetOuthouseByIdAsync(id),
                new OkResultFactory<OuthouseDto>());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<OuthouseDto>> Delete(Guid id)
        {
            return await ExecuteWithExceptionHandling(
                Service.RemoveOuthouseAsync(id), 
                new OkResultFactory<OuthouseDto>());
        }
    }
}