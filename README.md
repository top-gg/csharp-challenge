# Developer Story

## What I could finish:

- Created an API endpoint with the required param names: lat, long, start_date, end_date.
- Consumed a CSV Data Source with the given CSV file (actually I have downloaded the up-to-date version of that from the USGS website.)
- Consumed the USGS Earthquake Catalog API with real-time data (https://earthquake.usgs.gov/fdsnws/event/1/)
- Created very simple unit tests to demonstrate some TDD skills for both data contexts.
- README file.

## Performance:

    1. CSV Data Source: 

    I have reduced the re-reading file with every single request I have encapsulated the CSVDataContext in a singleton lifetime via dependency injection. Even though in-memory solutions are very fast in terms of retrieving data, still a data-caching solution would have made it faster. Scalability could be another concern here.

    2. USGS Data Source:

    I have used a HttpClient for connecting the USGS Earthquake Catalog in a singleton concept and created a disposable one. Because USGS only filters data by maxradiuskm or maxradius I had to fetch all data within maximum travel distance from specific coordinates (I have added a comment for this). This led me to fetch more than necessary (over-fetching) and I had to filter it again with the (magnitude * 100) formula. Here again, I could have cached the retrieved data. We depend on the USGS API’s availability here. This creates an availability concern.


## Scalability:

    If we experience some scalability issues, (since we have only one simple method partitioning the API horizontally is not a viable solution) we can partition the API vertically and use multiple servers. 

    Keeping all CSV data in RAM would not be the most elegant solution here in terms of scalability. If we partition the API vertically, we may need to use a more central data persisting solution. But for now, we only have READ operations, a single API back-end, and we use a singleton object for those READ operations we are fine.

## Availability:

    The major concern here would be querying USGS Earthquake Catalog API. We depend on their availability. We could mitigate the issue with caching.

## What I could have improved:

- Try-catches are mostly empty and in very simple forms. But exception bubbling practices can be seen as demonstrated. More specific exception types or AggregateException could be used within some blocks. Some more try-catches could have been used.
- More asynchronous operations could have been used.
- I have strictly tried to follow OCP and CQS but there are minor violations.
- More testing could have been written for the alternative scenarios and validations.
