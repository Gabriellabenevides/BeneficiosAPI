using Beneficios.Domain.Entities;
using Beneficios.Domain.Entities.Commands;
using Beneficios.Domain.Entities.ViewModel;
using Beneficios.Domain.Repositories;
using Beneficios.Domain.Shareds.Notifications;
using MediatR;

namespace Beneficios.Application.Handlers;

/// <summary>
/// Handler responsável por processar o comando de inclusão de benefícios a um colaborador.
/// </summary>
public class IncluirBeneficioColaboradorHandler : IRequestHandler<IncluirBeneficioColaboradorCommand, Response<ColaboradorBeneficioViewModel>>
{
    private readonly IBeneficioColaboradorRepository _repository;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="IncluirBeneficioColaboradorHandler"/>.
    /// </summary>
    /// <param name="repository">O repositório responsável pelas operações relacionadas aos benefícios do colaborador.</param>
    public IncluirBeneficioColaboradorHandler(IBeneficioColaboradorRepository repository)
    {
        _repository = repository;
    }

    /// <summary>
    /// Processa o comando para incluir benefícios a um colaborador.
    /// </summary>
    /// <param name="request">O comando contendo os dados do colaborador e os benefícios a serem incluídos.</param>
    /// <param name="cancellationToken">Token para cancelar a operação, se necessário.</param>
    /// <returns>Uma resposta contendo o modelo de visualização do colaborador com os benefícios incluídos.</returns>
    public async Task<Response<ColaboradorBeneficioViewModel>> Handle(IncluirBeneficioColaboradorCommand request, CancellationToken cancellationToken)
    {
        // Cria uma instância do colaborador com os dados fornecidos no comando
        var colaborador = new Colaborador(request.Colaborador.IdColaborador, request.Colaborador.Nome, request.Colaborador.Cargo, request.Beneficios);

        // Inclui os benefícios ao colaborador no repositório
        var result = await _repository.IncluirBeneficiosAoColaborador(colaborador, cancellationToken);

        // Cria o modelo de visualização do colaborador com os benefícios incluídos
        var colaboradorBeneficioViewModel = new ColaboradorBeneficioViewModel(result, result.Beneficios);

        // Retorna a resposta com o modelo de visualização
        return new(colaboradorBeneficioViewModel);
    }
}
