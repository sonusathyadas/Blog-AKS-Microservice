FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["*.csproj", "./"]
RUN dotnet restore

#Copy remaining files
COPY . .
RUN dotnet build -c Release -o /app
RUN dotnet publish -c Release -o /app

#Create image using runtime base image
FROM microsoft/dotnet:2.1-aspnetcore-runtime AS final
WORKDIR /app
EXPOSE 80
ENV Jwt:Issuer="http://localhost:5000"
ENV Jwt:Key="SomeSampleKeyWhichisLarge"
COPY --from=build /app .
ENTRYPOINT ["dotnet", "BlogUserService.dll"]