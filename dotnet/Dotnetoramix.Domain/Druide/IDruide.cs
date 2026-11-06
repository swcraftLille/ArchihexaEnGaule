namespace Dotnetoramix.Domain.Druide;

public interface IDruide
{
    Recette.Plat Preparer(Recette.Recette recette);
}
