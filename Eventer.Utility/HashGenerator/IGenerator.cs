namespace Eventer.Utility.HashGenerator
{
    public interface IGenerator
    {
        string GenerateGuid();
        string GenerateClientSecret();
        string GetHash(string input);
    }
}