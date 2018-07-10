FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 11433
EXPOSE 44398

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY RiverPuzzle/RiverPuzzle.csproj RiverPuzzle/
RUN dotnet restore RiverPuzzle/RiverPuzzle.csproj
COPY . .
WORKDIR /src/RiverPuzzle
RUN dotnet build RiverPuzzle.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish RiverPuzzle.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "RiverPuzzle.dll"]
