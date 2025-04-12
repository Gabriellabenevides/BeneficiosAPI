namespace Beneficios.Domain.Entities;

public class Colaborador
{
    public long IdColaborador { get; set; }
    public string? Nome { get; set; }
    public string? Cargo { get; set; }
    public List<Beneficio>? Beneficios { get; set; }
    
}
