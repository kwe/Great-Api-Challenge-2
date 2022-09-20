use actix_web::{web, App, HttpServer, Responder};
use std::io;

pub fn general_routes(cfg: &mut web::ServiceConfig) {
    cfg.service(web::resource("/").route(web::get().to(index)));
}

async fn index() -> impl Responder {
    "Hello world!"
}

#[actix_rt::main]
async fn main() -> io::Result<()> {
    let app = move || App::new().configure(general_routes);

    HttpServer::new(app).bind("127.0.0.1:8080")?.run().await
}
