namespace AppProject.Web.Services
{
    public interface ITipCalculatorService
    {
        Task<decimal> CalculateTipAsync(decimal billAmount, int tipPercentage);
    }

    public class TipCalculatorService : ITipCalculatorService
    {
        public async Task<decimal> CalculateTipAsync(decimal billAmount, int tipPercentage)
        {
            // Simulamos una operación asíncrona
            return await Task.FromResult(billAmount * tipPercentage / 100);
        }
    }
}
