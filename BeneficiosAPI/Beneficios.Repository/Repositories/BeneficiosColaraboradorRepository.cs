using Beneficios.Domain.Entities;
using Beneficios.Domain.Repositories;

namespace Beneficios.MongoDB.Repositories;

/// <summary>
/// Repositório para gerenciar a inclusão e atualização de benefícios associados a colaboradores.
/// </summary>
public class BeneficiosColaraboradorRepository : IBeneficioColaboradorRepository
{
    private readonly IRepository<Colaborador> _repository;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="BeneficiosColaraboradorRepository"/>.
    /// </summary>
    /// <param name="repository">O repositório genérico para operações com a entidade <see cref="Colaborador"/>.</param>
    public BeneficiosColaraboradorRepository(IRepository<Colaborador> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Colaborador> IncluirBeneficiosAoColaborador(Colaborador colaborador, CancellationToken cancellationToken)
    {
        await _repository.InsertOnAsync(colaborador, cancellationToken);
        return colaborador;
    }

    /// <inheritdoc />
    public async Task<Colaborador?> ObterColaboradorComBeneficiosPorId(long idColaborador, CancellationToken cancellationToken)
    {
        return await _repository.FindByIdAsync(idColaborador, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Colaborador> AtualizarBeneficiosDoColaborador(long idColaborador, List<Beneficio> beneficios, CancellationToken cancellationToken)
    {
        var colaborador = await _repository.FindByIdAsync(idColaborador, cancellationToken);
        if (colaborador == null)
        {
            throw new KeyNotFoundException($"Colaborador com ID {idColaborador} não encontrado.");
        }

        colaborador.Beneficios = beneficios;
        await _repository.ReplaceOneAsync(colaborador, cancellationToken);
        return colaborador;
    }

    /// <inheritdoc />
    public async Task<Colaborador> RemoverBeneficioDoColaborador(long idColaborador, string nomeBeneficio, CancellationToken cancellationToken)
    {
        var colaborador = await _repository.FindByIdAsync(idColaborador, cancellationToken);
        if (colaborador == null)
        {
            throw new KeyNotFoundException($"Colaborador com ID {idColaborador} não encontrado.");
        }

        colaborador.Beneficios = colaborador.Beneficios?.Where(b => b.Nome != nomeBeneficio).ToList();
        await _repository.ReplaceOneAsync(colaborador, cancellationToken);
        return colaborador;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Colaborador>> ObterTodosColaboradoresComBeneficios(CancellationToken cancellationToken)
    {
        return await _repository.GetAllAsync(cancellationToken);
    }
}
