FROM openjdk:8-slim AS base
WORKDIR /app

FROM maven:3.8.4-jdk-8 AS build
WORKDIR /build

COPY pom.xml ./
RUN mvn dependency:go-offline

FROM build AS publish
COPY src ./src
RUN mvn -f pom.xml clean package -DskipTests

FROM base AS final
WORKDIR /app
COPY --from=publish /build/target/*.jar ./app.jar

ENTRYPOINT ["java", "-jar", "/app/app.jar"]