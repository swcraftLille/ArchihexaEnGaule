namespace Dotnetoramix.Domain.Recette;

public record Recette(
    string Nom,
    List<Ingredient> Ingredients,
    Temperature? Prechauffage,
    List<Etape> Deroule)
{
    public static Recette PotionMagique => new(
        "Recette de la potion magique",
        new List<Ingredient>
        {
            new("trèfle à 4 feuilles", 8),
            new("fraise", 12),
            new("once de lait de sanglier", 4),
            new("pincée de curcuma", 10),
            new("feuille de gui coupée à la serpe d'or", 1)
        },
        new Temperature(90),
        new List<Etape>
        {
            new(
                "Mélanger les trèfles et les fraises",
                new Ingredient("trèfle à 4 feuilles", 8),
                new Ingredient("fraise", 12),
                Action.Melanger,
                "fraifle"),
            new(
                "Préparer le lait doré",
                new Ingredient("once de lait de sanglier", 4),
                new Ingredient("pincée de curcuma", 10),
                Action.Melanger,
                "lait doré"),
            new(
                "Faire bouillir les fraifles",
                new Ingredient("fraifle", 20),
                null,
                Action.Bouillir,
                "fraifle bouillie"),
            new(
                "Ajouter le gui",
                new Ingredient("fraifle bouillie", 20),
                new Ingredient("feuille de gui coupée à la serpe d'or", 1),
                Action.Melanger,
                "fraifle au gui"),
            new(
                "Verser le lait doré",
                new Ingredient("fraifle au gui", 21),
                new Ingredient("lait doré", 14),
                Action.Melanger,
                "précurseur de potion magique"),
            new(
                "Faire bouillir le précurseur",
                new Ingredient("précurseur de potion magique", 35),
                null,
                Action.Bouillir,
                "potion magique")
        });
}
