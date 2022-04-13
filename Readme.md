# Market Price
## Market Price Ingestor Service


# Publish Nuget Package

- Update Project Version (Events.csproj)
	<Version>X.X.X</Version>
- Publish package:
	dotnet nuget push "bin/Release/MarketPriceEvents.1.0.0.nupkg" --source "github"


# Add Nuget Package to get binance event

https://nuget.pkg.github.com/igorrebeche/index.json