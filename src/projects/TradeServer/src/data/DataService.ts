import {createConnection, Connection} from "typeorm";
import { User } from "../domain/Domain";

// createConnection method will automatically read connection options
// from your ormConfig file or environment variables
export const createDbConnection = async () => {
    var connection = await createConnection({
        type: "sqlite",
        database: "E:/NHUB/data/bina/BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [User]
    });
    return connection;
}