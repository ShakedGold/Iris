﻿using System;
using System.Collections.Generic;
using Iris.Config;
using Microsoft.Extensions.Logging;
using Telegram.Bot;
using Updates.Api;
using Updates.Configs;
using Updates.Twitter;
using Updates.Watcher;

namespace Iris
{
    internal class TelegramBot
    {
        private readonly ApplicationConfig _config;
        private readonly ILogger<TelegramBot> _logger;
        private readonly ITelegramBotClient _client;
        private readonly IUpdatesValidator _validator;

        public TelegramBot(
            ApplicationConfig config,
            ILogger<TelegramBot> logger,
            IUpdatesValidator validator)
        {
            _config = config;
            _logger = logger;

            _client = new TelegramBotClient(config.TelegramBotConfig.Token);
            _client.StartReceiving();

            _validator = validator;

            _logger.LogInformation("Starting to receive Telegram events");

            RegisterProducers();

            _logger.LogInformation("Completed construction");
        }

        private void RegisterProducers()
        {
            foreach ((IUpdatesProvider provider, IProviderConfig config) in GetProviders())
            {
                var usersWatcher = new UpdatesWatcher(
                    provider,
                    config,
                    _validator);

                usersWatcher.Updates
                    .Subscribe(OnProducerUpdate);

                _logger.LogInformation($"Subscribed to updates of the producer `{provider.GetType().Name}`");
            }
        }

        private async void OnProducerUpdate(IUpdate update)
        {
            _logger.LogInformation($"Caught new update: Id: {update.Id,-15} | Author: {update.Author.Name}");
            foreach (long chatId in _config.TelegramBotConfig.UpdateChatsIds)
            {
                await _client.SendTextMessageAsync(chatId, update.Url);
                _logger.LogInformation($"Posted new update: Id: {update.Id,-15} | ChatId: {update.Author.Name}");
            }
        }

        private Dictionary<IUpdatesProvider, IProviderConfig> GetProviders()
        {
            return new Dictionary<IUpdatesProvider, IProviderConfig>
            {
                {
                    new Twitter(_config.TwitterConfig), 
                    _config.TwitterConfig
                }
            };
        }
    }
}