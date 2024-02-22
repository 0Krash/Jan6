FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /webapi

EXPOSE 3000

#Copy projects files
COPY ./*.csproj ./
RUN dotnet restore

#Copy everything else
COPY . .
RUN dotnet publish -c Release -o out

#Build image
FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /webapi
COPY --from=build /webapi/out .
ENTRYPOINT [ "dotnet", "Jan6.dll" ]