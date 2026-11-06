namespace Dotnetoramix.Domain.Druide;

public class Dotnetoramix : IDruide
{
    private readonly IStock _stock;
    private readonly IAppareilDeCuisson _cuiseur;

    public Dotnetoramix(IStock stock, IAppareilDeCuisson cuiseur)
    {
        _stock = stock;
        _cuiseur = cuiseur;
    }

    public Recette.Plat Preparer(Recette.Recette recetteAPreparer)
    {
        Console.WriteLine($"Préparation de la recette : {recetteAPreparer.Nom}");

        VerifierDisponibiliteIngredients(recetteAPreparer);
        Prechauffer(recetteAPreparer);

        Recette.Ingredient dernierIngredientObtenu = Recette.Ingredient.Vide;
        foreach (var etape in recetteAPreparer.Deroule)
        {
            Console.WriteLine($"Étape : {etape.Nom}");

            if (etape.Action == Recette.Action.Melanger)
            {
                dernierIngredientObtenu = Melanger(etape);
            }

            if (etape.Action == Recette.Action.Bouillir)
            {
                dernierIngredientObtenu = Bouillir(etape);
            }

            _stock.StockerIngredient(dernierIngredientObtenu);
            Console.WriteLine($"Réservation de {dernierIngredientObtenu}");
        }

        return Servir(dernierIngredientObtenu);
    }

    private void VerifierDisponibiliteIngredients(Recette.Recette recetteAPreparer)
    {
        Console.WriteLine("Vérification de la disponibilité des ingrédients");
        var ingredientsDisponibles = recetteAPreparer.Ingredients.All(_stock.VerifierDisponibilite);

        if (!ingredientsDisponibles)
        {
            throw new IngredientManquantException();
        }
    }

    private void Prechauffer(Recette.Recette recetteAPreparer)
    {
        if (recetteAPreparer.Prechauffage is null)
        {
            return;
        }

        var temperatureSouhaitee = recetteAPreparer.Prechauffage;
        _cuiseur.Prechauffer(temperatureSouhaitee);

        while (_cuiseur.VerifierTemperature().Degre < temperatureSouhaitee.Degre)
        {
            Console.WriteLine($"Préchauffage du cuiseur en cours : {_cuiseur.VerifierTemperature().Degre} °C -> {temperatureSouhaitee.Degre} °C");
            Thread.Sleep(50);
        }
    }

    private Recette.Ingredient Melanger(Recette.Etape etape)
    {
        if (etape.Avec is null)
        {
            throw new InvalidOperationException("Une étape de mélange doit avoir un ingrédient associé.");
        }

        Console.WriteLine($"Mélanger {etape.Base} avec {etape.Avec}");

        if (!_stock.VerifierDisponibilite(etape.Base) || !_stock.VerifierDisponibilite(etape.Avec))
        {
            throw new IngredientManquantException();
        }

        var ingredientDeBase = _stock.RecupererIngredient(etape.Base) ?? throw new IngredientManquantException();
        var ingredientAvec = _stock.RecupererIngredient(etape.Avec) ?? throw new IngredientManquantException();

        return new Recette.Ingredient(etape.NomIngredientObtenu, ingredientDeBase.Quantite + ingredientAvec.Quantite);
    }

    private Recette.Ingredient Bouillir(Recette.Etape etape)
    {
        Console.WriteLine($"Mise à bouillir de {etape.Base}");

        if (!_stock.VerifierDisponibilite(etape.Base))
        {
            throw new IngredientManquantException();
        }

        var ingredientDeBase = _stock.RecupererIngredient(etape.Base) ?? throw new IngredientManquantException();
        _cuiseur.Cuire(ingredientDeBase, etape.NomIngredientObtenu);

        return _cuiseur.Prelever();
    }

    private Recette.Plat Servir(Recette.Ingredient ingredient)
    {
        var ingredientAServir = _stock.RecupererIngredient(ingredient) ?? throw new IngredientManquantException();
        var plat = new Recette.Plat(ingredientAServir.Nom, ingredientAServir.Quantite);
        Console.WriteLine($"Le plat {plat} est prêt !");
        return plat;
    }
}
