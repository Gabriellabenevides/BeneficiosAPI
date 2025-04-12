using System.ComponentModel.DataAnnotations;

namespace Beneficios.Domain.Shareds.Notifications;

/// <summary>
/// Classe responsável por gerenciar notificações de erros ou mensagens.
/// </summary>
public record class NotificationHandler
{
    private readonly List<Notification> _notifications;

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="NotificationHandler"/> sem notificações.
    /// </summary>
    public NotificationHandler()
    {
        _notifications = new List<Notification>();
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="NotificationHandler"/> com uma lista de notificações.
    /// </summary>
    /// <param name="notifications">As notificações a serem adicionadas.</param>
    public NotificationHandler(params Notification[] notifications) : this()
    {
        foreach (var error in notifications)
        {
            AddNotification(error.ErrorCode, error.ErrorMessage);
        }
    }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="NotificationHandler"/> com uma mensagem de erro.
    /// </summary>
    /// <param name="errorMessage">A mensagem de erro a ser adicionada.</param>
    public NotificationHandler(string errorMessage) : this()
    {
        AddNotification(errorMessage);
    }

    /// <summary>
    /// Obtém um valor que indica se há notificações.
    /// </summary>
    public bool HasNotifications => _notifications.Count != 0;

    /// <summary>
    /// Obtém a lista de notificações como uma coleção somente leitura.
    /// </summary>
    public IReadOnlyCollection<Notification> Notifications => _notifications;

    /// <summary>
    /// Adiciona uma ou mais mensagens de erro como notificações.
    /// </summary>
    /// <param name="errorMessage">As mensagens de erro a serem adicionadas.</param>
    public void AddNotification(params string[] errorMessage)
    {
        _notifications.AddRange(errorMessage.Select(x => new Notification(string.Empty, x)));
    }

    /// <summary>
    /// Adiciona uma notificação com um código de erro e uma mensagem de erro.
    /// </summary>
    /// <param name="errorCode">O código de erro.</param>
    /// <param name="errorMessage">A mensagem de erro.</param>
    public void AddNotification(string errorCode, string errorMessage)
    {
        _notifications.Add(new Notification(errorCode, errorMessage));
    }

    /// <summary>
    /// Adiciona notificações com base em um resultado de validação.
    /// </summary>
    /// <param name="validationResult">O resultado da validação contendo erros.</param>
    public void AddNotification(ValidationResult validationResult)
    {
        foreach (var error in validationResult.MemberNames)
        {
            AddNotification(error, validationResult.ErrorMessage);
        }
    }

    /// <summary>
    /// Converte implicitamente o <see cref="NotificationHandler"/> em uma string contendo as mensagens de erro.
    /// </summary>
    /// <param name="notificationHandler">A instância do <see cref="NotificationHandler"/>.</param>
    /// <returns>Uma string com as mensagens de erro separadas por quebras de linha.</returns>
    public static implicit operator string(NotificationHandler notificationHandler)
    {
        var result = string.Join(Environment.NewLine, notificationHandler.Notifications.Select(x => x.ErrorMessage));
        return result;
    }
}

