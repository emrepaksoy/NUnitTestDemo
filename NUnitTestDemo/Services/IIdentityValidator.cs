namespace NUnitTestDemo.Services
{
    public interface IIdentityValidator
    {
        bool IsValid(string identityNumber);
        bool CheckConnectionToRemoteServer();
        string Country { get; }

        ICountryProvider CountryProvider { get; }
    }

    public interface ICountry
    {
         string Country { get;}
    }

    public interface ICountryProvider
    {
        ICountry CountryData { get; }
    }
}