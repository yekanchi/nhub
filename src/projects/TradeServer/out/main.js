"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7O0FBQUEsdURBQXlEO0FBQ3pELHFDQUFrRDtBQUNsRCw0Q0FBcUM7QUFFOUIsS0FBSyxVQUFVLFVBQVU7SUFFNUIsSUFBSSxRQUFRLEdBQUcsSUFBSSxhQUFJLENBQUMsRUFBQyxRQUFRLEVBQUUsVUFBVSxFQUFFLElBQUksRUFBRSxTQUFTLEVBQUUsTUFBTSxFQUFFLFFBQVEsRUFBQyxDQUFDLENBQUM7SUFDbkYsSUFBSSxJQUFJLEdBQUcsSUFBSSxjQUFjLEVBQUUsQ0FBQztJQUVoQyxNQUFNLElBQUksQ0FBQyxNQUFNLENBQUMsUUFBUSxDQUFDLENBQUM7QUFDaEMsQ0FBQztBQU5ELGdDQU1DO0FBR0QsTUFBYSxjQUFjO0lBRXZCO1FBQ0ksSUFBSSxHQUFHLEdBQUcsZ0NBQWtCLEVBQUUsQ0FBQztRQUMvQixJQUFJLENBQUMsYUFBYSxHQUFHLG9CQUFVLEVBQUUsQ0FBQztJQUN0QyxDQUFDO0lBRUQsS0FBSyxDQUFDLE1BQU0sQ0FBQyxJQUFVO1FBQ25CLElBQUksQ0FBQyxhQUFhLENBQUMsTUFBTSxDQUFDLGFBQUksRUFBRSxJQUFJLENBQUMsQ0FBQztRQUN0QyxNQUFNLElBQUksQ0FBQyxhQUFhLENBQUMsSUFBSSxDQUFDLElBQUksQ0FBQyxDQUFDO0lBQ3hDLENBQUM7Q0FDSjtBQVhELHdDQVdDIn0=