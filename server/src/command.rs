use log::{error, info};

#[derive(Debug)]
pub enum Command {
    Set { key: String, value: String },
    Get { key: String },
    Remove { key: String },
    List,
    Ping,
    Invalid(String),
}

impl Command {
    pub fn parse(input: &str) -> Self {
        let mut parts = input.split_whitespace();
        let cmd = parts.next().unwrap_or("").to_uppercase();

        match cmd.as_str() {
            "SET" => {
                let key = parts.next().unwrap_or("").to_string();
                let value = parts.collect::<Vec<&str>>().join(" ");

                if key.is_empty() || value.is_empty() {
                    info!("Key or value cannot be empty");
                    return Command::Invalid("Key or value cannot be empty".to_string());
                }
                Command::Set { key, value }
            }
            "GET" | "REMOVE" => {
                let key = parts.next().unwrap_or("").to_string();

                if cmd == "GET" {
                    Command::Get { key }
                } else {
                    Command::Remove { key }
                }
            }
            "LIST" => Command::List,
            "PING" => {
                info!("PONG");
                Command::Ping
            }
            _ => {
                error!("Invalid command: {}", cmd);
                Command::Invalid(format!("Unknown command: {}", cmd))
            }
        }
    }
}
