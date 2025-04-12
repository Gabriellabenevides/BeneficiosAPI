namespace Beneficios.Domain.Entities.ViewModel;

public class ColaboradorBeneficioViewModel
{
    public ColaboradorBeneficioViewModel() { }

    public ColaboradorBeneficioViewModel(Colaborador colaborador, List<Beneficio>? beneficios)
    {
        Colaborador!.IdColaborador = colaborador.IdColaborador;
        Colaborador.Nome = colaborador.Nome;
        Colaborador.Cargo = colaborador.Cargo;
        Beneficios = beneficios;
    }

    public Colaborador Colaborador { get; set; }
    public IEnumerable<Beneficio>? Beneficios { get; set; }
}
