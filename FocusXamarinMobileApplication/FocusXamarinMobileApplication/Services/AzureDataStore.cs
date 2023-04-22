﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using FocusXamarinMobileApplication.Models;
using FocusXamarinMobileApplication.Services.Interfaces;
using MethodTimer;
using Xamarin.Essentials;

namespace FocusXamarinMobileApplication.Services;

public class AzureDataStore : IDataStore<Item>
{
    private readonly HttpClient client;
    private IEnumerable<Item> items;

    public AzureDataStore()
    {
        client = new HttpClient();
        //client.BaseAddress = new Uri($"{App.AzureBackendUrl}/");

        items = new List<Item>();
    }

    private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

    [Time]
    public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
    {
        if (forceRefresh && IsConnected)
        {
            var json = await client.GetStringAsync("api/item");
            items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<Item>>(json));
        }

        return items;
    }

    [Time]
    public async Task<Item> GetItemAsync(string id)
    {
        if (id != null && IsConnected)
        {
            var json = await client.GetStringAsync($"api/item/{id}");
            return await Task.Run(() => JsonConvert.DeserializeObject<Item>(json));
        }

        return null;
    }

    [Time]
    public async Task<bool> AddItemAsync(Item item)
    {
        if (item == null || !IsConnected)
            return false;

        var serializedItem = JsonConvert.SerializeObject(item);

        var response = await client.PostAsync("api/item",
            new StringContent(serializedItem, Encoding.UTF8, "application/json"));

        return response.IsSuccessStatusCode;
    }

    [Time]
    public async Task<bool> UpdateItemAsync(Item item)
    {
        if (item == null || item.Id == null || !IsConnected)
            return false;

        var serializedItem = JsonConvert.SerializeObject(item);
        var buffer = Encoding.UTF8.GetBytes(serializedItem);
        var byteContent = new ByteArrayContent(buffer);

        var response = await client.PutAsync(new Uri($"api/item/{item.Id}"), byteContent);

        return response.IsSuccessStatusCode;
    }

    [Time]
    public async Task<bool> DeleteItemAsync(string id)
    {
        if (string.IsNullOrEmpty(id) && !IsConnected)
            return false;

        var response = await client.DeleteAsync($"api/item/{id}");

        return response.IsSuccessStatusCode;
    }
}