using Dotnetoramix.Domain.Druide;
using Dotnetoramix.Domain.Recette;

namespace Dotnetoramix.Infrastructure;

public class GardeManger : IStock
{
    private readonly List<Ingredient> _contenu;

    public GardeManger(IEnumerable<Ingredient>? contenu = null)
    {
        _contenu = contenu is null ? new List<Ingredient>() : contenu.ToList();
    }

    public List<Ingredient> Contenu => _contenu;

    public bool VerifierDisponibilite(Ingredient ingredient)
    {
        var trouve = _contenu.FirstOrDefault(i => i.Nom == ingredient.Nom);
        return trouve is not null && trouve.Quantite >= ingredient.Quantite;
    }

    public void StockerIngredient(Ingredient ingredient)
    {
        var existant = _contenu.FirstOrDefault(i => i.Nom == ingredient.Nom);
        if (existant is null)
        {
            _contenu.Add(ingredient);
            return;
        }

        var index = _contenu.IndexOf(existant);
        _contenu[index] = ingredient;
    }

    public Ingredient? RecupererIngredient(Ingredient ingrédientRecherché)
    {
        if (!this.VerifierDisponibilite(ingrédientRecherché)) {
            return null;
        }

        for (int i = 0; i < _contenu.Count; i++) {
            var ingrédientCourant = _contenu[i];
            if (string.CompareOrdinal(ingrédientCourant.Nom, ingrédientRecherché.Nom) != 0) continue;
            _contenu.Remove(ingrédientCourant);
            var nouvelleQuantité = ingrédientCourant.Quantite - ingrédientRecherché.Quantite;
            if (nouvelleQuantité > 0) {
                _contenu.Add(new Ingredient(ingrédientCourant.Nom, nouvelleQuantité));
            }
            return ingrédientCourant;
        }

        return null;
    }
}
