using System.Text.Json.Serialization;

namespace Beneficios.Domain.Shareds.Notifications;

/// <summary>
/// Representa uma notificação contendo informações sobre erros ou mensagens.
/// </summary>
public record class Notification
{
    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Notification"/> com uma mensagem de erro.
    /// </summary>
    /// <param name="erroMessage">A mensagem de erro associada à notificação.</param>
    public Notification(string erroMessage)
    {
        ErrorCode = string.Empty;
        ErrorMessage = erroMessage;
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="Notification"/> com um código de erro e uma mensagem de erro.
    /// </summary>
    /// <param name="errorCode">O código de erro associado à notificação.</param>
    /// <param name="erroMessage">A mensagem de erro associada à notificação.</param>
    [JsonConstructor]
    public Notification(string errorCode, string erroMessage)
    {
        ErrorCode = errorCode;
        ErrorMessage = erroMessage;
    }

    /// <summary>
    /// Obtém ou define o código de erro associado à notificação.
    /// </summary>
    public string ErrorCode { get; set; }

    /// <summary>
    /// Obtém ou define a mensagem de erro associada à notificação.
    /// </summary>
    public string ErrorMessage { get; set; }
}