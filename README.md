# Petar Parushev 

## Notes and thoughts
Took me a while longer that I had hoped it would but I am generally pleased with how this task turned out. My C# is super rusty and I also forgot all of VS's shortcuts.
I developed it mostly using TDD and I have added unit tests for more than the controller classes. I do not have proper endpoint tests - I would add a in-memory API that I call using actual HTTP calls to check routes, parameters, validation and etc.
I am missing validation in controllers. I should add attributres on the query parameters to make sure that they are not null and that the start date should be less than the end date

## API performance and scale
I converted the API to asynchronos execution in one of the last commits which should make it easier on the threadpool. 
The solution is stateless so it can scale indefinetely behind a load balancer. 
I am making a call to USGS on every request which can be improved by setting up some sort of cache-ing. Surely there is an earthquake every second but most of them are insignificant and the use case of this system means that we don't need that fresh of data.



# Earthquake Challenge

## Was that an Earthquake?

Earthquakes are always in the news. Many people have friends/family members who live near earthquake zones and they would like to know if a friend/family member was affected by the earthquake Thanks to USGS we have up to date data on all earthquakes that have happened across the globe.
## The Challenge

Because friends/family want to make sure the people they care about are safe you will develop an API that uses the CSV provided(all_month.csv) in the repo or you can download the latest CSV from [USGS](https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_month.csv).

You will develop a C# API that allows the user query an endpoint with the following parameters: `lat`, `long`, `start_date`, `end_date`.

Using the data from USGS we will assume that all earthquakes have a consistent travel distance calculated by `magnitude * 100`

The radius of the earth can be considered a constant of 3959mi.

*Hint: Because the earth is spherical you will need to use Haversine formula*

### Requirements

- The endpoint should return the 10 latest earthquakes for a given lat/long for a date period in newest to oldest order.
- If there are no earthquakes for  the parameters the endpoint should return a 404 error

#### Exta Credit

- Write tests for your endpoint
- Use realtime data from USGS instead of a CSV file
- Add README that describes to improve performance and scale of the API

This challenge is designed to be completed within a few hours and is intended to see how you structure code and ensure you can follow specs. Our company values respect work/home separation so please dont spend a long time trying to solve the problem.