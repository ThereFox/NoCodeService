using System.Net.Http.Json;
using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.Attributes;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(4)]
public class TelegramSendMessageAction : INodeAction
{
    private readonly HttpClient _client;
    private readonly TelegramConfig _telegramConfig;

    public TelegramSendMessageAction(HttpClient client, TelegramConfig config)
    {
        _client = client;
        _telegramConfig = config;
    }

    public async Task<Result> Handle(IExecutionContext context)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://api.telegram.org/bot{_telegramConfig.BotToken}/sendMessage"
        );

        var chatId = _telegramConfig.ChatId.StartsWith("${") ? context.GetValue(_telegramConfig.ChatId) : _telegramConfig.ChatId;
        
        request.Content = JsonContent.Create(
            new
            {
                text = _telegramConfig.Content,
                parse_mode = "MarkdownV2",
                chat_id = chatId
            }
        );
        var response = await _client.SendAsync(request);

        return Result.Success();
    }
}