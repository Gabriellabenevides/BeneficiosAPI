namespace Beneficios.Domain.Entities.ViewModel;

public class ColaboradorBeneficioViewModel
{

    public Colaborador Colaborador { get; set; }
    public IEnumerable<Beneficio>? Beneficios { get; set; }

    public ColaboradorBeneficioViewModel(Colaborador colaborador, List<Beneficio>? beneficios)
    {
        Colaborador = colaborador;
        Beneficios = beneficios;
    }
}
