using Beneficios.Domain.Entities.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Beneficios_API.Controllers.v1;

/// <summary>
/// Controlador responsável por gerenciar as operações relacionadas aos benefícios dos colaboradores.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class BeneficioColaboradorController : ControllerBase
{
    private readonly IMediator _mediator;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="BeneficioColaboradorController"/>.
    /// </summary>
    /// <param name="mediator">O mediador para enviar comandos e consultas.</param>
    public BeneficioColaboradorController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> IncluirBeneficioColaborador([FromBody] IncluirBeneficioColaboradorCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}
