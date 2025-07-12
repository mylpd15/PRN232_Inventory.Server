﻿using Microsoft.Extensions.Configuration;

namespace TeachMate.Services;
public class ConfigService : IConfigService
{
    private readonly IConfiguration _config;

    public ConfigService(IConfiguration config)
    {
        _config = config;
    }
    public string GetString(string key)
    {
        var value = _config[key];

        return value is not null
            ? value
            : throw new Exception($"Value of {key} has not been set in appsettings.json file.");
    }
    public int GetInt(string key)
    {
        var value = GetString(key);

        try
        {
            return Convert.ToInt32(value);
        }
        catch (Exception)
        {
            throw new Exception($"Value of {key} is not a valid integer.");
        }
    }
}
