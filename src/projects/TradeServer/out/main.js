"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRepository = exports.RunService = void 0;
const DataService_1 = require("./Service/DataService");
const typeorm_1 = require("typeorm");
const Domain_1 = require("./domain/Domain");
async function RunService() {
    let someUser = new Domain_1.User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    var connection = await DataService_1.createDbConnection();
    var manager = typeorm_1.getManager();
    await manager.create(Domain_1.User, someUser);
    // let repo = new UserRepository();
    // await repo.create(someUser);
}
exports.RunService = RunService;
class UserRepository {
    constructor() {
    }
    async create(user) {
        let con = DataService_1.createDbConnection();
        let entityManager = typeorm_1.getManager();
        entityManager.create(Domain_1.User, user);
        await entityManager.save(user);
    }
}
exports.UserRepository = UserRepository;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLHVEQUF5RDtBQUN6RCxxQ0FBb0U7QUFDcEUsNENBQXFDO0FBRTlCLEtBQUssVUFBVSxVQUFVO0lBRTVCLElBQUksUUFBUSxHQUFHLElBQUksYUFBSSxDQUFDLEVBQUMsUUFBUSxFQUFFLFVBQVUsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFFLE1BQU0sRUFBRSxRQUFRLEVBQUMsQ0FBQyxDQUFDO0lBQ25GLElBQUksVUFBVSxHQUFHLE1BQU0sZ0NBQWtCLEVBQUUsQ0FBQztJQUM1QyxJQUFJLE9BQU8sR0FBRyxvQkFBVSxFQUFFLENBQUM7SUFDM0IsTUFBTSxPQUFPLENBQUMsTUFBTSxDQUFDLGFBQUksRUFBRSxRQUFRLENBQUMsQ0FBQztJQUVyQyxtQ0FBbUM7SUFDbkMsK0JBQStCO0FBQ25DLENBQUM7QUFURCxnQ0FTQztBQUdELE1BQWEsY0FBYztJQUN2QjtJQUVBLENBQUM7SUFFRCxLQUFLLENBQUMsTUFBTSxDQUFDLElBQVU7UUFDbkIsSUFBSSxHQUFHLEdBQUcsZ0NBQWtCLEVBQUUsQ0FBQztRQUMvQixJQUFJLGFBQWEsR0FBRyxvQkFBVSxFQUFFLENBQUE7UUFDaEMsYUFBYSxDQUFDLE1BQU0sQ0FBQyxhQUFJLEVBQUUsSUFBSSxDQUFDLENBQUM7UUFDakMsTUFBTSxhQUFhLENBQUMsSUFBSSxDQUFDLElBQUksQ0FBQyxDQUFDO0lBQ25DLENBQUM7Q0FDSjtBQVhELHdDQVdDIn0=