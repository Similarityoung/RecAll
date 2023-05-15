using Microsoft.EntityFrameworkCore;
using RecAll.Contrib.MaskedTextList.Api.Services;
using RecAll.Core.List.Domain.AggregateModels;
using RecAll.Infrastructure.EventBus.Abstractions;

namespace RecAll.Contrib.MaskedTextList.Api.IntegrationEvents; 

public class ItemIdAssignedIntegrationEventHandler :
    IIntegrationEventHandler<ItemIdAssignedIntegrationEvent> {
    private readonly MaskedTextListContext _textListContext;
    private readonly ILogger<ItemIdAssignedIntegrationEventHandler> _logger;

    public ItemIdAssignedIntegrationEventHandler(
        MaskedTextListContext MaskedTextListContext,
        ILogger<ItemIdAssignedIntegrationEventHandler> logger) {
        _textListContext = MaskedTextListContext ??
            throw new ArgumentNullException(nameof(MaskedTextListContext));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task Handle(ItemIdAssignedIntegrationEvent @event) {
        if (@event.TypeId != ListType.Text.Id) {
            return;
        }

        _logger.LogInformation(
            "----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})",
            @event.Id, InitialFunctions.AppName, @event);

        var textItem = await _textListContext.MaskedTextItems.FirstOrDefaultAsync(p =>
            p.Id == int.Parse(@event.ContribId));

        if (textItem is null) {
            _logger.LogWarning("Unknown TextItem id: {ItemId}", @event.ItemId);
            return;
        }

        textItem.ItemId = @event.ItemId;
        await _textListContext.SaveChangesAsync();

        _logger.LogInformation(
            "----- Integration event handled: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})",
            @event.Id, InitialFunctions.AppName, @event);
    }
}