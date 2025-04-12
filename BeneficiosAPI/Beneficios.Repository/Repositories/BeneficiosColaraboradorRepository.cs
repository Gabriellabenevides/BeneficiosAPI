using Beneficios.Domain.Entities;
using Beneficios.Domain.Repositories;

namespace Beneficios.MongoDB.Repositories;

/// <summary>
/// Repositório para gerenciar a inclusão e atualização de benefícios associados a colaboradores.
/// </summary>
public class BeneficiosColaraboradorRepository : IBeneficioColaboradorRepository
{
    private readonly IRepository<BeneficioColaborador> _repository;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="BeneficiosColaraboradorRepository"/>.
    /// </summary>
    /// <param name="repository">O repositório genérico para operações com a entidade <see cref="BeneficioColaborador"/>.</param>
    public BeneficiosColaraboradorRepository(IRepository<BeneficioColaborador> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<BeneficioColaborador> IncluirBeneficiosAoColaborador(BeneficioColaborador beneficioColaborador, CancellationToken cancellationToken)
    {
        // Insere o objeto BeneficioColaborador no repositório
        await _repository.InsertOnAsync(beneficioColaborador, cancellationToken);
        return beneficioColaborador;
    }

    /// <inheritdoc />
    public async Task<BeneficioColaborador?> ObterColaboradorComBeneficiosPorId(long idColaborador, CancellationToken cancellationToken)
    {
        // Busca o BeneficioColaborador pelo ID do colaborador
        return await _repository.SingleOrDefaultAsync(
            b => b.Colaborador.IdColaborador == idColaborador, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<BeneficioColaborador> AtualizarBeneficiosDoColaborador(long idColaborador, List<Beneficio> beneficios, CancellationToken cancellationToken)
    {
        // Busca o BeneficioColaborador pelo ID do colaborador
        var beneficioColaborador = await _repository.SingleOrDefaultAsync(
            b => b.Colaborador.IdColaborador == idColaborador, cancellationToken);

        if (beneficioColaborador == null)
        {
            throw new KeyNotFoundException($"Colaborador com ID {idColaborador} não encontrado.");
        }

        // Cria uma nova instância com os benefícios atualizados
        var atualizado = new BeneficioColaborador(
            beneficioColaborador.Colaborador,
            beneficios
        );

        await _repository.ReplaceOneAsync(atualizado, cancellationToken);

        return atualizado;
    }

    /// <inheritdoc />
    public async Task<BeneficioColaborador> RemoverBeneficioDoColaborador(long idColaborador, string nomeBeneficio, CancellationToken cancellationToken)
    {
        // Busca o BeneficioColaborador pelo ID do colaborador
        var beneficioColaborador = await _repository.SingleOrDefaultAsync(
            b => b.Colaborador.IdColaborador == idColaborador, cancellationToken);

        if (beneficioColaborador == null)
        {
            throw new KeyNotFoundException($"Colaborador com ID {idColaborador} não encontrado.");
        }

        // Cria uma nova instância com os benefícios atualizados
        var beneficiosAtualizados = beneficioColaborador.Beneficios?.Where(b => b.Nome != nomeBeneficio).ToList();
        var atualizado = new BeneficioColaborador(
            beneficioColaborador.Colaborador,
            beneficiosAtualizados
        );

        await _repository.ReplaceOneAsync(atualizado, cancellationToken);

        return atualizado;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<BeneficioColaborador>> ObterTodosColaboradoresComBeneficios(CancellationToken cancellationToken)
    {
        // Obtém todos os BeneficioColaborador
        return await _repository.GetAllAsync(cancellationToken);
    }
}
