using Dotnetoramix.Domain;
using Dotnetoramix.Domain.Druide;
using Dotnetoramix.Domain.Recette;
using Dotnetoramix.Infrastructure;
using Xunit;

namespace Dotnetoramix.Tests.Domain;

public class DotnetoramixTest
{
    private static readonly Recette RecetteDesFraifles = new(
        "Recette des fraifles",
        new List<Ingredient>
        {
            new("trèfle à 4 feuilles", 8),
            new("fraise", 12)
        },
        null,
        new List<Etape>
        {
            new(
                "Mélanger les trèfles et les fraises",
                new Ingredient("trèfle à 4 feuilles", 8),
                new Ingredient("fraise", 12),
                Dotnetoramix.Domain.Recette.Action.Melanger,
                "fraifle")
        });

    [Fact]
    public void PreparerDesFraifles()
    {
        var stock = new StockDouble(new List<Ingredient>
        {
            new("fraise", 12),
            new("trèfle à 4 feuilles", 8)
        });

        var cuiseur = new CuiseurDouble();
        IDruide dotnetoramix = new Dotnetoramix.Domain.Druide.Dotnetoramix(stock, cuiseur);

        var platAttendu = new Plat("fraifle", 20);

        var platObtenu = dotnetoramix.Preparer(RecetteDesFraifles);

        Assert.Equal(platAttendu, platObtenu);
        Assert.Empty(stock.Contenu);
    }

    [Fact]
    public void PreparerDesFraiflesIngredientAbsent()
    {
        var stock = new StockDouble(new List<Ingredient>
        {
            new("fraise", 12)
        });

        var cuiseur = new CuiseurDouble();
        IDruide dotnetoramix = new Dotnetoramix.Domain.Druide.Dotnetoramix(stock, cuiseur);

        Assert.Throws<IngredientManquantException>(() => dotnetoramix.Preparer(RecetteDesFraifles));
    }

    private sealed class StockDouble : GardeManger
    {
        public StockDouble(List<Ingredient> contenu) : base(contenu)
        {
        }


        // public bool VerifierDisponibilite(Ingredient ingredient)
        // {
        //     return _contenu.Any(i => i.Nom == ingredient.Nom && i.Quantite >= ingredient.Quantite);
        // }
        //
        // public void StockerIngredient(Ingredient ingredient)
        // {
        //     var existing = _contenu.FirstOrDefault(i => i.Nom == ingredient.Nom);
        //     if (existing is null)
        //     {
        //         _contenu.Add(ingredient);
        //         return;
        //     }
        //
        //     existing = existing with { Quantite = ingredient.Quantite };
        // }
        //
        // public Ingredient? RecupererIngredient(Ingredient ingredient)
        // {
        //     return _contenu.FirstOrDefault(i => i.Nom == ingredient.Nom);
        // }
    }

    private sealed class CuiseurDouble : IAppareilDeCuisson
    {
        public void Prechauffer(Temperature temperature) { }

        public Temperature VerifierTemperature() => new(0);

        public void Cuire(Ingredient ingredient, string nomIngredientObtenu) { }

        public Ingredient Prelever() => new("ingredient cuit", 0);
    }
}
