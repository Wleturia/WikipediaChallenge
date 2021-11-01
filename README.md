# Coding Assessment

The Wikimedia Foundation provides all pageviews for Wikipedia site since 2015 in machine-readable format. The pageviews can be downloaded in gzip format and are aggregated per hour per page. Each hourly dump is approximately 50MB in gzipped text file and is somewhere between 100MB and 250MB in size unzipped.
-	[File’s location](https://dumps.wikimedia.org/other/pageviews/)
-	[Sample file](https://dumps.wikimedia.org/other/pageviews/2015/2015-05/pageviews-20150501-010000.gz)
-	[Technical documentation](https://wikitech.wikimedia.org/wiki/Analytics/Data_Lake/Traffic/Pageviews)


## Task 
-	Create a command line application 
-	Get data for last 5 hours 
-	Calculate by the code the following SQL statement 
-	ALL_HOURS table represent all files
-	SQL statement use just to provide you requirements do not use database in your solution.  


## Building with
- .Net Core 5.0 (C#)

## Unit Testing
- xUnit


## Libraries
- [ConsoleTables](https://www.nuget.org/packages/ConsoleTables)

## Principles
- Solid

## Architecture 
- [Mix of DDD, Clean, Hexagonal](https://herbertograca.com/2017/11/16/explicit-architecture-01-ddd-hexagonal-onion-clean-cqrs-how-i-put-it-all-together/)


## Application Layers
- Console
- Application
- Domain
- Infrastructure
