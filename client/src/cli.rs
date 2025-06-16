use clap::{Parser, Subcommand};

#[derive(Parser)]
#[command(
    name = "KeyValueStore Client",
    version = "1.0",
    author = "OCK",
    about = "A simple key-value store client."
)]
pub struct Cli {
    #[command(subcommand)]
    pub arguments: Arguments,
}

#[derive(Subcommand)]
pub enum Arguments {
    #[command(name = "set", about = "Set a key-value pair")]
    Set { key: String, value: String },
    #[command(name = "get", about = "Get a key-value pair")]
    Get { key: String },
    #[command(name = "remove", about = "Delete a key-value pair")]
    Remove { key: String },
    #[command(name = "ping", about = "Ping the server for health check")]
    Ping,
    #[command(name = "list", about = "List all keys in the store")]
    List,
}
