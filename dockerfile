FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine-amd64

EXPOSE 8080

RUN mkdir /app
WORKDIR /app
COPY /linux-musl-x64/. ./

RUN chmod +x ./WebApiTest
CMD ["./WebApiTest", "--urls", "http://0.0.0.0:8080"]