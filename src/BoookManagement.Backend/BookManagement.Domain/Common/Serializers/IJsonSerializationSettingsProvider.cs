﻿using Newtonsoft.Json;

namespace BookManagement.Domain.Common.Serializers;

public interface IJsonSerializationSettingsProvider
{
    JsonSerializerSettings Configure(JsonSerializerSettings settings);

    JsonSerializerSettings ConfigureForEventBus(JsonSerializerSettings settings);


    JsonSerializerSettings Get(bool clone = false);

    JsonSerializerSettings GetForEventBus(bool clone = false);
}
