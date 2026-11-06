# Dotnetoramix

Version .NET du kata d’architecture hexagonale inspiré du monde d’Astérix, avec le jeu de mots entre Panoramix et le framework .NET.

## Objectif

Créer une application qui prépare la fameuse potion magique de Panoramix, en séparant clairement :

- le domaine métier,
- les ports abstraits,
- les adaptateurs d’infrastructure,
- l’API REST.

## Structure

- Dotnetoramix.Domain : logique métier pure
- Dotnetoramix.Infrastructure : garde-manger et thermomix
- Dotnetoramix.Api : API ASP.NET Core exposant l’endpoint `/potion-magique`
- Dotnetoramix.Tests : tests fonctionnels sur la préparation de la potion

## Lancer l’API

```bash
dotnet run --project Dotnetoramix.Api
```

Ensuite :

```bash
curl -X POST http://localhost:5000/potion-magique
```

ou, selon la configuration locale :

```bash
curl -X POST http://localhost:5150/potion-magique
```

## Lancer les tests

```bash
dotnet test
```
