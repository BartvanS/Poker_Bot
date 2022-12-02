using MQTTnet.Exceptions;

namespace Tests;
using CMQTT;

[TestFixture]
// [Parallelizable(ParallelScope.All)]
public class TestCMqtt
{
    Cmqtt _mqtt = new();

    [SetUp]
    public void Setup()
    {
        _mqtt = new Cmqtt();
    }

    [TearDown]
    public void TearDown()
    {
        _mqtt.Dispose();
    }

    [Test]
    public async Task ConnectToBroker()
    {
        bool isConnected = await _mqtt.Connect("localhost");
        Assert.IsTrue(_mqtt.Connected);
        Assert.IsTrue(isConnected);
    }

    [Test]
    public async Task ConnectToOfflineBroker()
    {
        Assert.ThrowsAsync<MqttCommunicationException>((async () => await _mqtt.Connect("FakeAddress")));
        Assert.IsFalse(_mqtt.Connected);
    }

    [Test]
    public async Task Subscribe()
    {
        await _mqtt.Connect("localhost");
        bool subscribed = await _mqtt.Subscribe("test");
        Assert.IsTrue(subscribed);
    }

    [Test]
    public async Task Publish()
    {
        await _mqtt.Connect("localhost");
        //subscribe to make sure the message was sent.
        await _mqtt.Subscribe("test");
        string sendMsg = "testMsg";
        string receivedMessage = "";
        _mqtt.NewMqttMessageEvent += (sender, message) =>
        {
            string msg = System.Text.Encoding.Default.GetString(message.Payload);
            receivedMessage = msg;
        };
        await _mqtt.Publish("test", sendMsg);
        Thread.Sleep(1000);
        Assert.AreEqual(receivedMessage, sendMsg);
    }
}