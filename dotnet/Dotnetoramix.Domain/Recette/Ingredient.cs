namespace Dotnetoramix.Domain.Recette;

public record Ingredient(string Nom, int Quantite)
{
    public static Ingredient Vide => new("ingredient vide", 0);
}
