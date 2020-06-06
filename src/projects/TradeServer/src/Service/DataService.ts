import {createConnection, Connection} from "typeorm";
import { User } from "../domain/Domain";

// createConnection method will automatically read connection options
// from your ormconfig file or environment variables
export const createDbConnection = async () => {
    var connection = await createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\data\\sqLiteDBs\\TSETMC.db",
        entities: [User]
    });
    return connection;
}