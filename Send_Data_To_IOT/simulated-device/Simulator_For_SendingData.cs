using System;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace simulated_device
{
    class Simulator_For_SendingDatad
    {
        private static DeviceClient s_deviceClient;

        private readonly static string s_connectionString = "HostName=ArIOTHub.azure-devices.net;DeviceId=Device1;SharedAccessKey=msRVEiPCepnxYI1IcE+GlTATe2wek4NAoB40A9fNVI8=";

        private static async void SendDeviceToCloudMessagesAsync()
        {
            // Initial telemetry values
            int minTemperature = 20;
            int minHumidity = 60;
            int minload = 50;
            //int minDeviceId = 1;
           // int minCoolant = 10;
            int minPressure = 5;
            Random rand = new Random();

            while (true)
            {
                int currentTemperature = minTemperature + rand.Next(1, 6) * 15;
                int currentHumidity = minHumidity + rand.Next(1, 6) * 20;
                int currentload = minload + rand.Next(1, 6) * 5;
               // int currentDeviceId = minDeviceId + rand.Next(1, 6) * 10;
                string currentDeviceId = "M101";
                string currentName = "Throttler";
                DateTime currentDate = DateTime.Now;

                //int currentCoolant = minCoolant + rand.Next(1, 6) * 20;
                int currentPressure = minPressure + rand.Next(1, 6) * 5;
                // Create JSON message
                var telemetryDataPoint = new
                {
                    //Columns of the table should be
                    //  ID,DeviceId,DeviceName,Temperature,Humidity,Load,Pressure,Date

                    DeviceId= currentDeviceId,
                    DeviceName=currentName,
                    Temperature = currentTemperature,
                    Humidity = currentHumidity,
                    Load = currentload,
                    //coolant = currentCoolant,
                    Pressure =currentPressure,
                    Date= currentDate.ToString("dd MMMM yyyy HH:mm:ss")
                   //date= currentDate
                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                // Send the telemetry message
                await s_deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }
        private static void Main(string[] args)
        {
            Console.WriteLine("IoT Hub - Simulated device. Ctrl-C to exit.\n");

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();
            Console.ReadLine();
        }
    }
}
