using Dotnetoramix.Domain.Druide;
using Dotnetoramix.Domain.Recette;

namespace Dotnetoramix.Infrastructure;

public class Thermomix : IAppareilDeCuisson
{
    private int _temperature;
    private Ingredient? _ingredientCuit;

    public void Prechauffer(Temperature temperature)
    {
        _temperature = temperature.Degre;
    }

    public Temperature VerifierTemperature()
    {
        return new Temperature(_temperature);
    }

    public void Cuire(Ingredient ingredient, string nomIngredientObtenu)
    {
        _ingredientCuit = new Ingredient(nomIngredientObtenu, ingredient.Quantite);
    }

    public Ingredient Prelever()
    {
        if (_ingredientCuit is null)
        {
            throw new InvalidOperationException("Aucun ingrédient n’a encore été cuit.");
        }

        return _ingredientCuit;
    }
}
