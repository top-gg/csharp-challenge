namespace Earthquake.Data.USGS
{
    // only purpose of this abstraction is using two different
    // data contexts with the API.
    public interface IUsgsDataContext : IDataContext
    {
    }
}