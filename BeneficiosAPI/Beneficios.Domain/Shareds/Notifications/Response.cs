using System.Net;

namespace Beneficios.Domain.Shareds.Notifications;

/// <summary>
/// Representa uma resposta genérica que pode conter dados, notificações de erro e um código de status HTTP.
/// </summary>
/// <typeparam name="TResponse">O tipo de dado contido na resposta.</typeparam>
public record class Response<TResponse>
{
    private readonly NotificationHandler? _details;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Response{TResponse}"/> com dados e um código de status HTTP.
    /// </summary>
    /// <param name="data">Os dados da resposta.</param>
    /// <param name="httpStatusCode">O código de status HTTP. O padrão é <see cref="HttpStatusCode.OK"/>.</param>
    public Response(TResponse? data, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
    {
        _details = null;
        Data = data;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Response{TResponse}"/> com uma lista de notificações e um código de status HTTP.
    /// </summary>
    /// <param name="notifications">As notificações associadas à resposta.</param>
    /// <param name="httpStatusCode">O código de status HTTP. O padrão é <see cref="HttpStatusCode.BadRequest"/>.</param>
    public Response(IEnumerable<Notification> notifications, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        _details = new NotificationHandler(notifications.ToArray());
        Data = default;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Response{TResponse}"/> com uma mensagem de erro e um código de status HTTP.
    /// </summary>
    /// <param name="errorMessage">A mensagem de erro associada à resposta.</param>
    /// <param name="httpStatusCode">O código de status HTTP. O padrão é <see cref="HttpStatusCode.BadRequest"/>.</param>
    public Response(string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        _details = new NotificationHandler(errorMessage);
        Data = default;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Response{TResponse}"/> com um código de erro, uma mensagem de erro e um código de status HTTP.
    /// </summary>
    /// <param name="errorCode">O código de erro associado à resposta.</param>
    /// <param name="errorMessage">A mensagem de erro associada à resposta.</param>
    /// <param name="httpStatusCode">O código de status HTTP. O padrão é <see cref="HttpStatusCode.BadRequest"/>.</param>
    public Response(string errorCode, string errorMessage, HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest)
    {
        var notification = new Notification(errorCode, errorMessage);
        _details = new NotificationHandler(notification);
        Data = default;
        HttpStatusCode = httpStatusCode;
    }

    /// <summary>
    /// Obtém ou define os dados da resposta.
    /// </summary>
    public TResponse? Data { get; set; }

    /// <summary>
    /// Obtém ou define o código de status HTTP da resposta.
    /// </summary>
    public HttpStatusCode HttpStatusCode { get; set; }

    /// <summary>
    /// Indica se a resposta foi bem-sucedida com base na ausência de notificações e no código de status HTTP.
    /// </summary>
    public bool IsSuccess => _details is null || !_details.HasNotifications || ((int)HttpStatusCode >= 200 && (int)HttpStatusCode < 300);
}
