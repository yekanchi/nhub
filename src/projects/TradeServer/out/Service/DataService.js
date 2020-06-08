"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const typeorm_1 = require("typeorm");
const Domain_1 = require("../domain/Domain");
// createConnection method will automatically read connection options
// from your ormconfig file or environment variables
exports.createDbConnection = async () => {
    var connection = await typeorm_1.createConnection({
        type: "sqlite",
        database: "E:/NHUB/data/TSETMC.db",
        entities: [Domain_1.User]
    });
    return connection;
};
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YVNlcnZpY2UuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi9zcmMvU2VydmljZS9EYXRhU2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOztBQUFBLHFDQUFxRDtBQUNyRCw2Q0FBd0M7QUFFeEMscUVBQXFFO0FBQ3JFLG9EQUFvRDtBQUN2QyxRQUFBLGtCQUFrQixHQUFHLEtBQUssSUFBSSxFQUFFO0lBQ3pDLElBQUksVUFBVSxHQUFHLE1BQU0sMEJBQWdCLENBQUM7UUFDcEMsSUFBSSxFQUFFLFFBQVE7UUFDZCxRQUFRLEVBQUUsd0JBQXdCO1FBQ2xDLFFBQVEsRUFBRSxDQUFDLGFBQUksQ0FBQztLQUNuQixDQUFDLENBQUM7SUFDSCxPQUFPLFVBQVUsQ0FBQztBQUN0QixDQUFDLENBQUEifQ==