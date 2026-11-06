namespace Dotnetoramix.Domain.Recette;

public record Etape(
    string Nom,
    Ingredient Base,
    Ingredient? Avec,
    Action Action,
    string NomIngredientObtenu);
