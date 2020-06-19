"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.createDbConnection = void 0;
const typeorm_1 = require("typeorm");
const Domain_1 = require("../domain/Domain");
// createConnection method will automatically read connection options
// from your ormConfig file or environment variables
exports.createDbConnection = async () => {
    var connection = await typeorm_1.createConnection({
        type: "sqlite",
        database: "E:/NHUB/data/bina/BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [Domain_1.User]
    });
    return connection;
};
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YVNlcnZpY2UuanMiLCJzb3VyY2VSb290IjoiIiwic291cmNlcyI6WyIuLi8uLi9zcmMvZGF0YS9EYXRhU2VydmljZS50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxxQ0FBcUQ7QUFDckQsNkNBQXdDO0FBRXhDLHFFQUFxRTtBQUNyRSxvREFBb0Q7QUFDdkMsUUFBQSxrQkFBa0IsR0FBRyxLQUFLLElBQUksRUFBRTtJQUN6QyxJQUFJLFVBQVUsR0FBRyxNQUFNLDBCQUFnQixDQUFDO1FBQ3BDLElBQUksRUFBRSxRQUFRO1FBQ2QsUUFBUSxFQUFFLGdDQUFnQztRQUMxQyxPQUFPLEVBQUUsS0FBSztRQUNkLFdBQVcsRUFBRSxJQUFJO1FBQ2pCLFFBQVEsRUFBRSxDQUFDLGFBQUksQ0FBQztLQUNuQixDQUFDLENBQUM7SUFDSCxPQUFPLFVBQVUsQ0FBQztBQUN0QixDQUFDLENBQUEifQ==