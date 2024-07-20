namespace HealthMateApp
{
    public interface IHealthMateAppProvider
    {
        IHealthMateAppCore GetApp(string choice);
    }
}
