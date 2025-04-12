using Beneficios.Domain.Shareds;

namespace Beneficios.Domain.Entities;

/// <summary>
/// Representa os benefícios associados a um colaborador.
/// </summary>
public class BeneficioColaborador : Entity, IEntity
{
    public BeneficioColaborador(Colaborador colaborador, List<Beneficio>? beneficios)
    {
        Colaborador = colaborador;
        Beneficios = beneficios;
    }

    public Colaborador Colaborador { get; init; }
    public List<Beneficio>? Beneficios { get; init; }
}
