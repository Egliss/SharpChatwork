using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SharpChatwork.Client.Exceptions;
using SharpChatwork.Query;

namespace SharpChatwork;

public interface IChatworkClient
{
#pragma warning disable CA1716
    public IMeQuery me { get; }
#pragma warning restore CA1716
    public IRoomQuery room { get; }
    public IContactQuery contact { get; }
    public IIncomingRequestQuery incomingRequest { get; }
}

public abstract class ChatworkClient : IChatworkClient
{
    public ChatworkClient()
    {
        this.me = new MeQuery(this);
        this.contact = new ContactQuery(this);
        this.room = new RoomQuery(this);
        this.incomingRequest = new IncomingRequestQuery(this);
    }

    public abstract string clientName { get; }

    public IMeQuery me { get; }
    public IRoomQuery room { get; }
    public IContactQuery contact { get; }
    public IIncomingRequestQuery incomingRequest { get; }
    public abstract ValueTask<ResponseWrapper> QueryAsync(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default);

    public async ValueTask<T> QueryAsync<T>(Uri uri, HttpMethod method, HttpContent content, CancellationToken cancellation = default)
    {
        var wrapper = await this.QueryAsync(uri, method, content, cancellation);
        if(wrapper.statusCode >= 300)
            throw new ChatworkClientException(wrapper);
        return JsonSerializer.Deserialize<T>(wrapper.content);
    }

    public async ValueTask<ResponseWrapper> QueryAsync(Uri uri, HttpMethod method, IReadOnlyDictionary<string, string> data, CancellationToken cancellation = default)
    {
        HttpContent content = null;
        if(data.Count != 0)
            content = new FormUrlEncodedContent(data);
        return await this.QueryAsync(uri, method, content, cancellation);
    }

    public async ValueTask<T> QueryAsync<T>(Uri uri, HttpMethod method, IReadOnlyDictionary<string, string> data, CancellationToken cancellation = default)
    {
        var wrapper = await this.QueryAsync(uri, method, data, cancellation);
        if(wrapper.statusCode >= 300)
            throw new ChatworkClientException(wrapper);
        return JsonSerializer.Deserialize<T>(wrapper.content);
    }
}
