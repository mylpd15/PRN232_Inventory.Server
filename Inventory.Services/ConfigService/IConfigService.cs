﻿namespace WareSync.Services;

public interface IConfigService
{
    int GetInt(string key);
    string GetString(string key);
}