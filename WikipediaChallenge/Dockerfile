#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["WikipediaChallenge/WikipediaChallenge.ConsoleApp.csproj", "WikipediaChallenge/"]
COPY ["WikipediaChallenge.Infrastructure/WikipediaChallenge.Infrastructure.csproj", "WikipediaChallenge.Infrastructure/"]
COPY ["WikipediaChallenge.Domain/WikipediaChallenge.Domain.csproj", "WikipediaChallenge.Domain/"]
COPY ["WikipediaChallenge.Application/WikipediaChallenge.Application.csproj", "WikipediaChallenge.Application/"]
RUN dotnet restore "WikipediaChallenge/WikipediaChallenge.ConsoleApp.csproj"
COPY . .
WORKDIR "/src/WikipediaChallenge"
RUN dotnet build "WikipediaChallenge.ConsoleApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WikipediaChallenge.ConsoleApp.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WikipediaChallenge.ConsoleApp.dll"]