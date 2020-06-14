import { createConnection, Connection } from "typeorm";
import { User } from "./domain/Domain";

export async function getConnection(): Promise<Connection> {
    var connection = await createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\data\\bina\\BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [User]
    });
    return connection;
}
