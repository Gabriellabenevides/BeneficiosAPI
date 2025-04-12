using Beneficios.Domain.Entities;

namespace Beneficios.Domain.Repositories;

/// <summary>
/// Interface para o repositório de operações relacionadas à inclusão e gerenciamento de benefícios para um colaborador.
/// </summary>
public interface IBeneficioColaboradorRepository
{
    /// <summary>
    /// Inclui benefícios a um colaborador associado.
    /// </summary>
    /// <param name="colaborador">O colaborador ao qual os benefícios serão associados.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>O colaborador atualizado com os benefícios incluídos.</returns>
    Task<Colaborador> IncluirBeneficiosAoColaborador(Colaborador colaborador, CancellationToken cancellationToken);

    /// <summary>
    /// Obtém um colaborador pelo seu identificador, incluindo seus benefícios.
    /// </summary>
    /// <param name="idColaborador">O identificador do colaborador.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>O colaborador encontrado ou <c>null</c> se não encontrado.</returns>
    Task<Colaborador?> ObterColaboradorComBeneficiosPorId(long idColaborador, CancellationToken cancellationToken);

    /// <summary>
    /// Atualiza os benefícios de um colaborador.
    /// </summary>
    /// <param name="idColaborador">O identificador do colaborador.</param>
    /// <param name="beneficios">A lista de benefícios atualizada.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>O colaborador atualizado com os novos benefícios.</returns>
    Task<Colaborador> AtualizarBeneficiosDoColaborador(long idColaborador, List<Beneficio> beneficios, CancellationToken cancellationToken);

    /// <summary>
    /// Remove um benefício específico de um colaborador.
    /// </summary>
    /// <param name="idColaborador">O identificador do colaborador.</param>
    /// <param name="nomeBeneficio">O nome do benefício a ser removido.</param>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>O colaborador atualizado após a remoção do benefício.</returns>
    Task<Colaborador> RemoverBeneficioDoColaborador(long idColaborador, string nomeBeneficio, CancellationToken cancellationToken);

    /// <summary>
    /// Obtém todos os colaboradores e seus benefícios.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar a operação.</param>
    /// <returns>Uma lista de colaboradores com seus benefícios.</returns>
    Task<IEnumerable<Colaborador>> ObterTodosColaboradoresComBeneficios(CancellationToken cancellationToken);
}

