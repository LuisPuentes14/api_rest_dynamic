using api_rest_dynamic.CustomExceptions;
using api_rest_dynamic.Repository;
using api_rest_dynamic.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NpgsqlTypes;
using System.Collections.Generic;
using System.Data;
using System.Reflection.Metadata;
using System.Text.Json;

namespace api_rest_dynamic.Controllers
{
    
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class RequestsController(IServiceRequests serviceRequests) : ControllerBase
    {
        private readonly IServiceRequests _serviceRequests = serviceRequests;

        [HttpPost]
        [Route("{nameProcedure}")]
        public async Task<IActionResult> Request(string nameProcedure, Dictionary<string, object> parameters)
        {
            try
            {
                return Ok(await _serviceRequests.GetResponse(nameProcedure, parameters));       
            }
            catch (NotFoundException ex)
            {
                return NotFound($"{{\"error\":\"{ex.Message}\"}}");

            }

        }
    }
}
