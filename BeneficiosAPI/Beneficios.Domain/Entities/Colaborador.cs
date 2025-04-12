using Beneficios.Domain.Shareds;

namespace Beneficios.Domain.Entities;

public class Colaborador : Entity
{
    public long IdColaborador { get; set; }
    public string? Nome { get; set; }
    public string? Cargo { get; set; }
    
    public Colaborador(long idColaborador, string? nome, string? cargo)
    {
        IdColaborador = idColaborador;
        Nome = nome;
        Cargo = cargo;
    }

    public Colaborador() {}
}
