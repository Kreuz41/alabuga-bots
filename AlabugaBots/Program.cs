using AlabugaBots.States;
using MinimalTelegramBot;
using MinimalTelegramBot.Builder;
using MinimalTelegramBot.Handling;
using MinimalTelegramBot.Handling.Filters;
using MinimalTelegramBot.Localization.Abstractions;
using MinimalTelegramBot.Localization.Extensions;
using MinimalTelegramBot.StateMachine.Abstractions;
using MinimalTelegramBot.StateMachine.Extensions;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

var builder = BotApplication.CreateBuilder(args);

builder.Services.AddStateMachine();
builder.Services.AddSingleLocale(new Locale("ru"), locale => locale.EnrichFromFile("Localization/ru.yaml"));


var app = builder.Build();

app.UseStateMachine();
app.UsePolling();
app.UseCallbackAutoAnswering();

var replyChatId = long.Parse(builder.Configuration["ChatId"]
                            ?? throw new NullReferenceException("Reply chat not found"));

app.HandleCommand("/start", async (BotRequestContext context, IStateMachine stateMachine, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Hello"]);
    await Task.Delay(1000);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["GetName"]);
    stateMachine.SetState(new UserNameState());
});

app.Handle(async (BotRequestContext context, IStateMachine stateMachine, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["WhenOnboarding"]);
    
    await context.Client.ForwardMessageAsync(
        chatId: replyChatId,
        fromChatId: context.ChatId,
        messageId:  context.Update.Message!.MessageId);
    stateMachine.SetState(new OnboardingState());
}).FilterState(state => state == new UserNameState()).FilterMessageText(_ => true);

app.Handle(async (BotRequestContext context, IStateMachine stateMachine, ILocalizer localizer) =>
{
    await context.Client.ForwardMessageAsync(
        chatId: replyChatId,
        fromChatId: context.ChatId,
        messageId:  context.Update.Message!.MessageId);
    stateMachine.DropState();
    
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["ThanksForAnswers"]);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["ItsImportant"]);

    await SendMenu(context, localizer);
}).FilterState(state => state == new OnboardingState()).FilterMessageText(_ => true);

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Docs"]);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Employers"]);

    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "docs");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Transport.Text"],
        replyMarkup: new InlineKeyboardMarkup(new []
        {
            InlineKeyboardButton.WithCallbackData(localizer["Transport.Car"], "car"),
            InlineKeyboardButton.WithCallbackData(localizer["Transport.Trip"], "trip")
        }));
}).FilterCallbackData(data => data == "transport");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Transport.CarDesc"]);
    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "car");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Transport.TripDesc"]);
    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "trip");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Pass"]);
    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "pass");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Location"]);
    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "location");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Complete"]);
    await SendMenu(context, localizer);
}).FilterCallbackData(data => data == "complete");

app.Handle(async (BotRequestContext context, IStateMachine stateMachine, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Ask"]);
    stateMachine.SetState(new AskState());
}).FilterCallbackData(data => data == "ask");

app.Handle(async (BotRequestContext context, IStateMachine stateMachine, ILocalizer localizer) =>
{
    await context.Client.ForwardMessageAsync(
        chatId: replyChatId,
        fromChatId: context.ChatId,
        messageId:  context.Update.Message!.MessageId);
    stateMachine.DropState();
    
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["ThanksForAsk"]);

    await SendMenu(context, localizer);
}).FilterState(state => state == new AskState()).FilterMessageText(_ => true);

app.Run();

return 0;

async Task SendMenu(BotRequestContext context, ILocalizer localizer)
{
    var neededDocs = localizer["Menu.NeededDocs"];
    var transport = localizer["Menu.Transport"];
    var pass = localizer["Menu.Pass"];
    var location = localizer["Menu.Location"];
    var complete = localizer["Menu.Complete"];
    var ask = localizer["Menu.Ask"];
    
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Menu.Text"],
        replyMarkup: new InlineKeyboardMarkup(new []
        {
            new [] { InlineKeyboardButton.WithCallbackData(neededDocs, "docs") },
            new [] { InlineKeyboardButton.WithCallbackData(transport, "transport") },
            new [] { InlineKeyboardButton.WithCallbackData(pass, "pass") },
            new [] { InlineKeyboardButton.WithCallbackData(location, "location") },
            new [] { InlineKeyboardButton.WithCallbackData(complete, "complete") },
            new [] { InlineKeyboardButton.WithCallbackData(ask, "ask") },
        }));
}