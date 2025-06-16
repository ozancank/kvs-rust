use std::env;

mod command;
mod data;
mod handler;
mod server;

#[tokio::main]
async fn main() -> tokio::io::Result<()> {
    dotenv::dotenv().ok();
    env_logger::init();

    let address = env::var("LISTEN_ADDRESS").unwrap_or_else(|_| "127.0.0.1:5544".to_owned());
    server::run(&address).await
}
