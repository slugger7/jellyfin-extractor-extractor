#!/bin/bash
dotnet build Jellyfin.Plugin.Extractor.sln;
docker stop jellyfin-dev;
rm ~/Documents/jellyfinconfig/data/plugins/Extractor/Jellyfin.Plugin.Extractor.dll;
cp Jellyfin.Plugin.Extractor/bin/Debug/netstandard2.1/Jellyfin.Plugin.Extractor.dll ~/Documents/jellyfinconfig/data/plugins/Extractor/Jellyfin.Plugin.Extractor.dll;
docker start jellyfin-dev;
docker logs -f jellyfin-dev;