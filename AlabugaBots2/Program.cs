using AlabugaBots2;
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

app.HandleCommand("/start", async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Hello"]);
    await Task.Delay(1000);
    await SendSimpleMenu(context, localizer);
});

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Simple.Complete"]);
    await Task.Delay(1000);
    await SendSimpleMenu(context, localizer);
}).FilterCallbackData(data => data == "complete");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Simple.Workspace"]);
    await Task.Delay(1000);
    await SendSimpleMenu(context, localizer);
}).FilterCallbackData(data => data == "workspace");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Simple.Community"]);
    await Task.Delay(1000);
    await SendSimpleMenu(context, localizer);
}).FilterCallbackData(data => data == "community");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Simple.About"]);
    await Task.Delay(1000);
    await SendSimpleMenu(context, localizer);
}).FilterCallbackData(data => data == "about");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "next");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Addresses"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "addresses");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Weekend"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "weekend");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Seek"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "seek");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Notes"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "notes");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Dms"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "dms");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Sales"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "sales");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Salary"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "salary");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Schedule"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "schedule");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Pass"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "pass");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Comps"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "comps");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.House"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "house");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Study"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "study");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Trip"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "trip");

app.Handle(async (BotRequestContext context, ILocalizer localizer) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Military"]);
    await Task.Delay(1000);
    await SendDifficultMenu(context, localizer);
}).FilterCallbackData(data => data == "military");

app.Handle(async (BotRequestContext context, ILocalizer localizer, IStateMachine stateMachine) =>
{
    await context.Client.DeleteMessageAsync(
        chatId: context.ChatId,
        messageId: context.Update.CallbackQuery!.Message!.MessageId);
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Difficult.Other"]);
    stateMachine.SetState(new OtherState());
}).FilterCallbackData(data => data == "other");

app.Handle(async (BotRequestContext context, ILocalizer localizer, IStateMachine stateMachine) =>
{
    stateMachine.DropState();
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["ThanksForAsk"]);
    await context.Client.ForwardMessageAsync(
        chatId: replyChatId,
        fromChatId: context.ChatId,
        messageId:  context.Update.Message!.MessageId);
    await SendDifficultMenu(context, localizer);
}).FilterState(state => state == new OtherState()).FilterMessageText(_ => true);

app.Run();

return 0;

async Task SendSimpleMenu(BotRequestContext context, ILocalizer localizer)
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["ForNew"],
        replyMarkup: new InlineKeyboardMarkup(new []
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("1", "complete"),
                InlineKeyboardButton.WithCallbackData("2", "workspace"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("3", "community"),
                InlineKeyboardButton.WithCallbackData("4", "about"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData(localizer["Complete"], "next")
            },
        }));
}

async Task SendDifficultMenu(BotRequestContext context, ILocalizer localizer)
{
    await context.Client.SendTextMessageAsync(
        chatId: context.ChatId,
        text: localizer["Next"],
        replyMarkup: new InlineKeyboardMarkup(new []
        {
            new []
            {
                InlineKeyboardButton.WithCallbackData("1", "addresses"),
                InlineKeyboardButton.WithCallbackData("2", "weekend"),
                InlineKeyboardButton.WithCallbackData("3", "seek"),
                InlineKeyboardButton.WithCallbackData("4", "notes"),
                InlineKeyboardButton.WithCallbackData("5", "dms"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("6", "sales"),
                InlineKeyboardButton.WithCallbackData("7", "salary"),
                InlineKeyboardButton.WithCallbackData("8", "schedule"),
                InlineKeyboardButton.WithCallbackData("9", "pass"),
                InlineKeyboardButton.WithCallbackData("10", "comps"),
            },
            new []
            {
                InlineKeyboardButton.WithCallbackData("11", "house"),
                InlineKeyboardButton.WithCallbackData("12", "study"),
                InlineKeyboardButton.WithCallbackData("13", "trip"),
                InlineKeyboardButton.WithCallbackData("14", "military"),
                InlineKeyboardButton.WithCallbackData("15", "other"),
            }
        }));
}