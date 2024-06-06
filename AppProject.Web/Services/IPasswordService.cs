namespace AppProject.Web.Services
{
    public interface IPasswordService
    {
        Task<string> GeneratePasswordAsync(int length, bool includeLowercase, bool includeUppercase, bool includeNumbers, bool includeSymbols);
    }

    public class PasswordService : IPasswordService
    {
        private readonly Random random;

        private const string lowercaseCharacters = "abcdefghijklmnopqrstuvwxyz";
        private const string uppercaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string numbers = "0123456789";
        private const string symbols = "!@#$%^&*()_-+={}[]|;':\"<>,.?/~";

        public PasswordService()
        {
            random = new Random();
        }

        public async Task<string> GeneratePasswordAsync(int length, bool includeLowercase, bool includeUppercase, bool includeNumbers, bool includeSymbols)
        {
            var characters = "";

            if (includeLowercase)
            {
                characters += lowercaseCharacters;
            }

            if (includeUppercase)
            {
                characters += uppercaseCharacters;
            }

            if (includeNumbers)
            {
                characters += numbers;
            }

            if (includeSymbols)
            {
                characters += symbols;
            }

            var password = "";
            for (int i = 0; i < length; i++)
            {
                password += characters[random.Next(characters.Length)];
            }

            return await Task.FromResult(password);
        }
    }
}