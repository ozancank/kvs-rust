FROM rust:slim-bookworm AS builder

WORKDIR /app
COPY . .

RUN cargo build --release

FROM debian:bookworm-slim

RUN apt-get update && apt-get install -y ca-certificates && rm -rf /var/lib/apt/lists/*

WORKDIR /app
COPY --from=builder /app/target/release/key-value-store-server /usr/local/bin/key-value-store-server
COPY .env .env

EXPOSE 5544

CMD ["key-value-store-server"]