# -------- BUILD STAGE --------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
RUN dotnet publish -c Release -o /app/publish

# -------- RUNTIME STAGE --------
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Install native dependencies required for SkiaSharp, Excel, PDF
RUN apt-get update && apt-get install -y \
    libfontconfig1 \
    libfreetype6 \
    libx11-6 \
    libx11-dev \
    libxrender1 \
    libxext6 \
    libice6 \
    libsm6 \
    libc6-dev \
    libgdiplus \
    fonts-dejavu-core \
    fonts-liberation \
    && rm -rf /var/lib/apt/lists/*

# IMPORTANT: enable System.Drawing + SkiaSharp
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "PropertyManagement.API.dll"]