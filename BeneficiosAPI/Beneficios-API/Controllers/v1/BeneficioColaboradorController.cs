using Beneficios.Application.Handlers;
using Beneficios.Domain.Entities.Commands;
using Beneficios.Domain.Entities.ViewModel;
using Beneficios.Domain.Shareds.Notifications;
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

    /// <summary>
    /// Endpoint para incluir benefícios a um colaborador.
    /// </summary>
    /// <param name="command">O comando contendo os dados do colaborador e os benefícios a serem incluídos.</param>
    /// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
    /// <returns>Uma resposta contendo o modelo de visualização do colaborador com os benefícios incluídos.</returns>
    [HttpPost("incluir")]
    [ProducesResponseType(typeof(Response<ColaboradorBeneficioViewModel>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<ColaboradorBeneficioViewModel>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> IncluirBeneficioColaborador(
        [FromBody] IncluirBeneficioColaboradorCommand command,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);

        if (response.IsSuccess)
        {
            return Ok(response);
        }

        return BadRequest(response);
    }
}
