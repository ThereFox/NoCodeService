using System.Net.Http.Json;
using CSharpFunctionalExtensions;
using NoCodeConstructor.Domain.Abstactions;
using NoCodeConstructor.Domain.Configs;
using NodeBuilder.Attributes;
using ExecutionContext = NoCodeConstructor.Domain.DTOs.ExecutionContext;

namespace NoCodeConstructor.Domain.Actions;

[ActionCode(4)]
public class TelegramSendMessageAction : INodeAction
{
    private readonly HttpClient _client;
    private readonly SendTelegramMessageConfig _sendTelegramMessageConfig;

    public TelegramSendMessageAction(HttpClient client, SendTelegramMessageConfig messageConfig)
    {
        _client = client;
        _sendTelegramMessageConfig = messageConfig;
    }

    public async Task<Result> Handle(ExecutionContext context)
    {
        var filledConfig = context.Configuration.GetConfiguration(_sendTelegramMessageConfig);
        
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            $"https://api.telegram.org/bot{filledConfig.BotToken}/sendMessage"
        );
        request.Content = JsonContent.Create(
            new
            {
                text = filledConfig.Content,
                parse_mode = "MarkdownV2",
                chat_id = filledConfig.ChatId
            }
        );
        var response = await _client.SendAsync(request);

        return Result.Success();
    }
}