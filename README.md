# Simulator_SendingTelemetryData
simulator for sending the random generated telemetry data to azure iot hub



                       CONSOLE APPILCATION SENDING THE DATA TO AZURE IOT HUB:-

Important topics on this are:-
1.	Device client azure 
2.	Random class
3.	Newtonsoft.json for serialization
4.	Encoding
5.	System.Threading.Tasks



Procedure:- 
1.	First create the connection with the azure iot hub using device client connection string.
2.	Generate some random values of the telemetry data such as temperature, load etc.
3.	Put this data inside a class as 
4.	Serialize the object of the class
5.	Encode it 
6.	Send it.
