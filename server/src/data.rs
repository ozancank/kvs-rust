use std::collections::HashMap;
use std::sync::Arc;
use tokio::sync::Mutex;

#[derive(Clone)]
pub struct DataStore {
    context: Arc<Mutex<HashMap<String, String>>>,
}

impl DataStore {
    pub fn new() -> Self {
        DataStore {
            context: Arc::new(Mutex::new(HashMap::new())),
        }
    }

    pub async fn set(&self, key: &str, value: &str) {
        let mut context = self.context.lock().await;
        context.insert(key.to_string(), value.to_string());
    }

    pub async fn get(&self, key: &str) -> Option<String> {
        let context = self.context.lock().await;
        context.get(key).cloned()
    }

    pub async fn remove(&self, key: &str) -> bool {
        let mut context = self.context.lock().await;
        context.remove(key).is_some()
    }

    pub async fn keys(&self) -> Vec<String> {
        let context = self.context.lock().await;
        context.keys().cloned().collect()
    }

    pub async fn is_empty(&self) -> bool {
        let context = self.context.lock().await;
        context.is_empty()
    }
}
