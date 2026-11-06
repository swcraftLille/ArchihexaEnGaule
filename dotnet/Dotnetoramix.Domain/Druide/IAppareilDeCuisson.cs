using Dotnetoramix.Domain.Recette;

namespace Dotnetoramix.Domain.Druide;

public interface IAppareilDeCuisson
{
    void Prechauffer(Temperature temperature);
    Temperature VerifierTemperature();
    void Cuire(Ingredient ingredient, string nomIngredientObtenu);
    Ingredient Prelever();
}
