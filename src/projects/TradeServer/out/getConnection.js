"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.getConnection = void 0;
const typeorm_1 = require("typeorm");
const Domain_1 = require("./domain/Domain");
async function getConnection() {
    var connection = await typeorm_1.createConnection({
        type: "sqlite",
        database: "E:\\NHUB\\data\\bina\\BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [Domain_1.User]
    });
    return connection;
}
exports.getConnection = getConnection;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiZ2V0Q29ubmVjdGlvbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9nZXRDb25uZWN0aW9uLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLHFDQUF1RDtBQUN2RCw0Q0FBdUM7QUFFaEMsS0FBSyxVQUFVLGFBQWE7SUFDL0IsSUFBSSxVQUFVLEdBQUcsTUFBTSwwQkFBZ0IsQ0FBQztRQUNwQyxJQUFJLEVBQUUsUUFBUTtRQUNkLFFBQVEsRUFBRSxvQ0FBb0M7UUFDOUMsT0FBTyxFQUFFLEtBQUs7UUFDZCxXQUFXLEVBQUUsSUFBSTtRQUNqQixRQUFRLEVBQUUsQ0FBQyxhQUFJLENBQUM7S0FDbkIsQ0FBQyxDQUFDO0lBQ0gsT0FBTyxVQUFVLENBQUM7QUFDdEIsQ0FBQztBQVRELHNDQVNDIn0=