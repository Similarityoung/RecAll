FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Contrib/MaskedTextList.Api/MaskedTextList.Api.csproj", "Contrib/MaskedTextList.Api/"]
RUN dotnet restore "Contrib/MaskedTextList.Api/MaskedTextList.Api.csproj"
COPY . .
WORKDIR "/src/Contrib/MaskedTextList.Api"
RUN dotnet build "MaskedTextList.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MaskedTextList.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MaskedTextList.Api.dll"]
