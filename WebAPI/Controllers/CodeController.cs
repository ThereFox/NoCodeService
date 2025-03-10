using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NoCode.Application.Interfaces;
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
    private readonly ExecuteSavedScheme _executeSavedScheme;
    private readonly SaveScheme _saveScheme;
    
    public CodeController(TestScheme test, ExecuteSavedScheme executeSavedScheme, SaveScheme saveScheme)
    {
        _test = test;
        _executeSavedScheme = executeSavedScheme;
        _saveScheme = saveScheme;
    }
    
    
    [HttpPost]
    [Route("/{id}")]
    public async Task<IActionResult> SaveNew([FromRoute]Guid id, [FromBody] CodeScheme codeScheme)
    {
        var nodes = codeScheme.Nodes.Select(ex =>
            new NodeConfigInputObject(ex.Id, ex.TypeId, ex.ConnectedElements, ex.Configuration.ToString())).ToList();

        var handleResult = await _saveScheme.SaveWithValidation(id, nodes);

        if (handleResult.IsFailure)
        {
            return BadRequest(handleResult.Error);
        }
        
        return Ok("213");
    }
    
    [HttpPost]
    [Route("/test")]
    public async Task<IActionResult> Test([FromBody] CodeScheme codeScheme)
    {
        var nodes = codeScheme.Nodes.Select(ex =>
            new NodeConfigInputObject(ex.Id, ex.TypeId, ex.ConnectedElements, ex.Configuration.ToString())).ToList();

        var handleResult = await _test.TryRun(nodes);

        if (handleResult.IsFailure)
        {
            return BadRequest(handleResult.Error);
        }
        
        return Ok("213");
    }
    
    [HttpGet]
    [Route("/{id}")]
    public async Task<IActionResult> RunById([FromRoute] Guid id)
    {
        var runResult = await _executeSavedScheme.RunById(id);

        if (runResult.IsFailure)
        {
            return BadRequest(runResult.Error);
        }
        
        return Ok("213");
    }
}