using Dotnetoramix.Domain.Recette;

namespace Dotnetoramix.Domain.Druide;

public interface IStock
{
    bool VerifierDisponibilite(Ingredient ingredient);
    void StockerIngredient(Ingredient ingredient);
    Ingredient? RecupererIngredient(Ingredient ingrédientRecherché);
}
