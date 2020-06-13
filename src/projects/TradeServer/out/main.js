"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRepository = exports.RunService = void 0;
const DataService_1 = require("./Service/DataService");
const typeorm_1 = require("typeorm");
const Domain_1 = require("./domain/Domain");
async function RunService() {
    let someUser = new Domain_1.User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    let repo = new UserRepository();
    await repo.create(someUser);
}
exports.RunService = RunService;
class UserRepository {
    constructor() {
        let con = DataService_1.createDbConnection();
        this.entityManager = typeorm_1.getManager();
    }
    async create(user) {
        this.entityManager.create(Domain_1.User, user);
        await this.entityManager.save(user);
    }
}
exports.UserRepository = UserRepository;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUFBLHVEQUF5RDtBQUN6RCxxQ0FBa0Q7QUFDbEQsNENBQXFDO0FBRTlCLEtBQUssVUFBVSxVQUFVO0lBRTVCLElBQUksUUFBUSxHQUFHLElBQUksYUFBSSxDQUFDLEVBQUMsUUFBUSxFQUFFLFVBQVUsRUFBRSxJQUFJLEVBQUUsU0FBUyxFQUFFLE1BQU0sRUFBRSxRQUFRLEVBQUMsQ0FBQyxDQUFDO0lBQ25GLElBQUksSUFBSSxHQUFHLElBQUksY0FBYyxFQUFFLENBQUM7SUFFaEMsTUFBTSxJQUFJLENBQUMsTUFBTSxDQUFDLFFBQVEsQ0FBQyxDQUFDO0FBQ2hDLENBQUM7QUFORCxnQ0FNQztBQUdELE1BQWEsY0FBYztJQUV2QjtRQUNJLElBQUksR0FBRyxHQUFHLGdDQUFrQixFQUFFLENBQUM7UUFDL0IsSUFBSSxDQUFDLGFBQWEsR0FBRyxvQkFBVSxFQUFFLENBQUM7SUFDdEMsQ0FBQztJQUVELEtBQUssQ0FBQyxNQUFNLENBQUMsSUFBVTtRQUNuQixJQUFJLENBQUMsYUFBYSxDQUFDLE1BQU0sQ0FBQyxhQUFJLEVBQUUsSUFBSSxDQUFDLENBQUM7UUFDdEMsTUFBTSxJQUFJLENBQUMsYUFBYSxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsQ0FBQztJQUN4QyxDQUFDO0NBQ0o7QUFYRCx3Q0FXQyJ9