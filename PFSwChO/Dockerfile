# Etap bazowy - u�ywamy gotowego obrazu aspnet:7.0 jako podstawy
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

# Ustawiamy katalog roboczy na /app
WORKDIR /app

# Dodajemy informacj� o autorze
LABEL maintainer="Grzegorz Grzegorczyk"

# Deklarujemy ekspozycj� portu 80
EXPOSE 80

# Etap budowania - u�ywamy obrazu sdk:7.0 do budowy aplikacji
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Ustawiamy katalog roboczy na /src
WORKDIR /src

# Kopiujemy plik projektu do katalogu /src/PFSwChO/
COPY "PFSwChO/PFSwChO.csproj" "PFSwChO/"

# Przywracamy zale�no�ci aplikacji
RUN dotnet restore "PFSwChO/PFSwChO.csproj"

# Kopiujemy reszt� kodu �r�d�owego aplikacji
COPY . .

# Ustawiamy katalog roboczy na /src
WORKDIR /src

# Kompilujemy aplikacj� w trybie Release i zapisujemy wyniki w katalogu /app/build
RUN dotnet build "PFSwChO/PFSwChO.csproj" -c Release -o /app/build

# Etap publikacji - z obrazu build kopiujemy skompilowane pliki do /app/publish
FROM build AS publish
RUN dotnet publish "PFSwChO/PFSwChO.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etap finalny - bazujemy na obrazie base, ustawiamy katalog roboczy na /app
FROM base AS final
WORKDIR /app

# Kopiujemy skompilowane pliki z obrazu publish do /app w obrazie final
COPY --from=publish /app/publish .

# Ustalamy punkt wej�cia dla kontenera na PFSwChO.dll
ENTRYPOINT ["dotnet", "PFSwChO.dll"]

# Definiujemy polecenie Health Check, kt�re sprawdza zdrowie aplikacji
HEALTHCHECK --interval=10s --timeout=3s --retries=3 \
  CMD curl --fail http://localhost:80/health || exit 1
