using Beneficios.Domain.Entities.ViewModel;
using Beneficios.Domain.Shareds.Notifications;
using MediatR;

namespace Beneficios.Domain.Entities.Commands;

public record class IncluirBeneficioColaborador(Colaborador Colaborador, List<Beneficio>? Beneficios) : IRequest<Response<ColaboradorBeneficioViewModel>>;
