using System.Globalization;
using System.Text;
using MQTTnet;
using qlocktwo;

namespace ClockFromWords;

public class MQTT
{
    private string host;
    private int port;
    private string topic;
    private Action<MqttApplicationMessageReceivedEventArgs, bool> parser; 
    
    //constructor
    public MQTT(string host, string topic, Action<MqttApplicationMessageReceivedEventArgs, bool> parser,
        int port = 1883)
    {
        this.host = host;
        this.topic = topic;
        this.port = port;
        this.parser = parser;
    }
    public async void Handle_Received_Application_Message()
    {
        
        // wait 10s on startup to make sure network is up and running
        await Task.Delay(10000);

        var mqttFactory = new MqttClientFactory();

        using var mqttClient = mqttFactory.CreateMqttClient();
        var mqttClientOptions = new MqttClientOptionsBuilder().WithTcpServer(host, port).Build();

        // Setup message handling before connecting so that queued messages
        // are also handled properly. When there is no event handler attached all
        // received messages get lost.
        mqttClient.ApplicationMessageReceivedAsync += e =>
        {
            if (true) DumpToConsole(e); // add a config-bit for console outputs
            
            parser(e, true);
            
            return Task.CompletedTask;
        };

        await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

        var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder().WithTopicFilter(topic).Build();

        await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

        Console.WriteLine("MQTT client subscribed to topic.");
        
    }

    private void DumpToConsole(MqttApplicationMessageReceivedEventArgs message)
    {
        Console.WriteLine("Received application message.");
            
        Console.WriteLine($"Topic: {message.ApplicationMessage.Topic}");
        Console.WriteLine($"Payload: {Encoding.UTF8.GetString(message.ApplicationMessage.Payload)}");
        Console.WriteLine($"QoS: {message.ApplicationMessage.QualityOfServiceLevel}");
        Console.WriteLine($"Retain: {message.ApplicationMessage.Retain}");
        Console.WriteLine($"ClientId: {message.ClientId}");
        
    }
    
    
    #region Parsers
    // different methods for parsing different strings from different topics
    
    public static void ParseClimateData(MqttApplicationMessageReceivedEventArgs message, bool protActive)
    {
        string roomtemperature = Encoding.UTF8.GetString(message.ApplicationMessage.Payload);
        DataExchange.instance.roomtemperature = roomtemperature;
        
        if (protActive)
        {
            string path = DataExchange.instance.pathProt;
            // write received string to file
            FileUtility.Append($"{path}room_{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}.txt", roomtemperature);
        }
        
    }

    #endregion

    
}