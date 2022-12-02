using MQTTnet.Client;

namespace CMQTT;

using MQTTnet;

public class Cmqtt : IDisposable
{
    public bool Connected { get; private set; }
    private bool _disposed = false;
    private IMqttClient _mqttClient;
    private readonly MqttFactory _mqttFactory = new();

    public delegate void NewMqttMessageEventHandler(object sender, MqttApplicationMessage e);

    public event NewMqttMessageEventHandler NewMqttMessageEvent;

    public async Task<bool> Connect(string address, string username = "", string password = "")
    {
        //Make sure we are not connected
        //todo: test if neccesary
        await Disconnect();

        _mqttClient = _mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(address)
            .WithCredentials(username, password).Build();

        // This will throw an exception if the server is not available.
        // The result from this message returns additional data which was sent 
        // from the server. Please refer to the MQTT protocol specification for details.
        try
        {
            var response = await _mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);
            Console.WriteLine($"MQTT connected to address {address}");
            _mqttClient.ApplicationMessageReceivedAsync += OnMessageReceived;
            Connected = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return Connected;
    }

    /// <summary>
    /// Subscribe to a mqtt topic. When a message arrives a event is invoked. See Connect for mor
    /// information.
    /// </summary>
    /// <param name="topic"></param>
    /// <returns></returns>
    public async Task<bool> Subscribe(string topic)
    {
        try
        {
            var mqttSubscribeOptions = _mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(
                    f => { f.WithTopic(topic); })
                .Build();

            await _mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }

    public async Task Publish(string topic, string message)
    {
        var applicationMessage = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(message)
            .Build();
        await _mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
    }

    public async Task Disconnect()
    {
        if (Connected)
        {
            // Send a clean disconnect to the server by calling _DisconnectAsync_. Without this the TCP connection
            // gets dropped and the server will handle this as a non clean disconnect (see MQTT spec for details).
            var mqttClientDisconnectOptions = _mqttFactory.CreateClientDisconnectOptionsBuilder().Build();
            await _mqttClient.DisconnectAsync(mqttClientDisconnectOptions, CancellationToken.None);
            Connected = false;
        }
    }

    private async Task<Task> OnMessageReceived(MqttApplicationMessageReceivedEventArgs e)
    {
        Console.WriteLine($"Received application message. {e.ApplicationMessage}");
        NewMqttMessageEvent.Invoke(this, e.ApplicationMessage);
        return Task.CompletedTask;
    }

    protected void Dispose(bool disposing)
    {
        if (!this._disposed)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                _mqttClient.Dispose();
            }

            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}