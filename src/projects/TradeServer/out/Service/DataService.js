"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const typeorm_1 = require("typeorm");
const Domain_1 = require("../domain/Domain");
// createConnection method will automatically read connection options
// from your ormconfig file or environment variables
exports.createDbConnection = async () => {
    var connection = await typeorm_1.createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\data\\sqLiteDBs\\TSETMC.db",
        entities: [Domain_1.User]
    });
    return connection;
};
//# sourceMappingURL=DataService.js.map