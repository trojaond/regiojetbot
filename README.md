# Regiojetbot
This console application serves as a bot for testing ticket availability for RegioJet company lines. Its purpose is to notify the user by email when the free space is released.


Prerequsities
-------------

- [.NET Core 2.1][1]


Usage
-----
Get a copy of this repository and launch terminal in RJ folder. To run application simply run the following command

```
dotnet run
``` 



###Destination and departure
Proper setting is achieved in **Program.cs** where the departure and destination location is set. Note that the location must match list from 
```https://jizdenky.regiojet.cz/m/``` 

```csharp
const string from = "Praha";
const string to = "Ostrava";
static DateTime time = new DateTime(2018, 8, 22, 11, 48, 00); //yyyy, month, day, hour, min, sec

```

###Email delivery
Mails a delivered through my demo gmail account. It can be changed in **EmailSender.cs** but it must be a gmail account with allowed [Less secure app acces] [3] more [info][2] 

```csharp
client.Credentials = new NetworkCredential("YourEmail", "YourPassword");

```




[1]: https://www.microsoft.com/net/learn/get-started-with-dotnet-tutorial
[2]: https://support.google.com/a/answer/176600?hl=en
[3]: https://support.google.com/a/answer/6260879?hl=en
