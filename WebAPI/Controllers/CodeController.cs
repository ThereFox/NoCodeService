using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NoCode.Application.UseCases;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.DTOs;
using WebAPI.Requests;

namespace WebAPI.Controllers;

[Controller]
[Route("/")]
public class CodeController : Controller
{

    private readonly TestScheme _test;
    
    public CodeController(TestScheme test)
    {
        _test = test;
    }
    
    
    [HttpPost]
    [Route("")]
    public async Task<IActionResult> SaveNew([FromBody] CodeScheme codeScheme)
    {
        var nodes = codeScheme.Nodes.Select(ex =>
            new NodeConfigInputObject(ex.Id, ex.TypeId, ex.ConnectedElements, ex.Configuration.ToString())).ToList();

        var handleResult = await _test.HandleNode(nodes);

        if (handleResult.IsFailure)
        {
            return BadRequest(handleResult.Error);
        }
        
        return Ok("213");
    }
}