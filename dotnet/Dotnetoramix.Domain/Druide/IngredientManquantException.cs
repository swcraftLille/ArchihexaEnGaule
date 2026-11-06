namespace Dotnetoramix.Domain.Druide;

public class IngredientManquantException : Exception
{
    public IngredientManquantException()
        : base("Un ingrédient est manquant pour la recette.")
    {
    }
}
