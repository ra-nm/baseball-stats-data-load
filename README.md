# Baseball Stats Sample Web Application Data Load

This utility is a C# console application that loads a CSV file of baseball player salary data into Azure Tables using the Azure Storage SDK.

The Visual Studio project contains the CSV file of salary data, so if you would like to run it to load data into an existing Azure Storage account, all you need to do is update the following AppSettings in the app.config file and run the program in Visual Studio.

```
  <appSettings>
    <add key="storageAccountName" value="{storageaccountname}"/>
    <add key="storageAccountKey" value="{storageaccountkey}"/>
  </appSettings>
```

Data Source: [Lahman's Baseball Database](http://www.seanlahman.com/baseball-archive/statistics/)