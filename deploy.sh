#!/bin/bash
jellyfinConfig=~/.config/jellyfin
dotnet build Jellyfin.Plugin.Extractor.sln && \
docker stop jellyfin-test && \
rm $jellyfinConfig/data/plugins/Extractor/Jellyfin.Plugin.Extractor.dll && \
cp Jellyfin.Plugin.Extractor/bin/Debug/netstandard2.1/Jellyfin.Plugin.Extractor.dll $jellyfinConfig/data/plugins/Extractor/Jellyfin.Plugin.Extractor.dll &&\
docker start jellyfin-test && \
docker logs -f jellyfin-test;