# Earthquake Challenge

## Was that an Earthquake?

Earthquakes are always in the news. Many people have friends/family members who live near earthquake zones and they would like to know if a friend/family member was affected by the earthquake Thanks to USGS we have up to date data on all earthquakes that have happened across the globe.
## The Challenge

Because friends/family want to make sure the people they care about are safe you will develop an API that uses the CSV provided(all_month.csv) in the repo or you can download the latest CSV from [USGS](https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_month.csv).

You will develop a C# API that allows the user query an endpoint with the following parameters: `lat`, `long`, `start_date`, `end_date`.

Using the data from USGS we will assume that all earthquakes have a consistent travel distance calculated by `magnitude * 100`

*Hint: Because the earth is spherical you will need to use Haversine formula*

### Requirements

- The endpoint should return the 10 latest earthquakes for a given lat/long for a date period in newest to oldest order.
- If there are no earthquakes for  the parameters the endpoint should return a 404 error

#### Exta Credit

- Write tests for your endpoint
- Use realtime data from USGS instead of a CSV file
- Add document about how to improve performance and scale of the API

This challenge is designed to be completed within a few hours and is intended to see how you structure code and ensure you can follow specs. Our company values respect work/home separation so please dont spend a long time trying to solve the problem.