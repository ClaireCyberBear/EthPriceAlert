##NAME: 
Ethereum Price Alert

##DESCRIPTION:
The purpose of this project is a simple console program that calls the coinmarketcap api and sends an alert whenever Ethereum hits a certain price threshold within a set time frame

##INSTALLATION:
1.) Clone this repository
2.) Open up your terminal and cd to the project. Example: C:*Your-Directory*>cd *DIRECTORY-TO-PROJECT*
3.) Now type in dotnet build
4.) Get your CoinMarketCap API key from here: https://pro.coinmarketcap.com/
5.) Finally set up the config file. Place your API key inbetween the qoutes inside of the config file
6.) Rename config.template to config and copy it to bin\Debug\net7.0

##CONFIG:
There is a template config for you to use.

  *YOUR API KEY GOES HERE*
  "ApiKey": "REPLACE KEY HERE BETWEEN QOUTES",
  
  *HOW OFTEN PRICE IS CHECKED IN MINUTES, DEFAULT IS 30*
  "CheckFrequency": 30,
  
  *PERCENT PRICE CHANGE THRESHOLD IN ORDER TO TRIGGER ALERT*
  "PriceChangeThreshold": 5

##Plans:
More extensive configuration options including comparing prices daily, weekly, etc

##CONTRIBUTIONS:
Clairecyberbear@gmail.com
