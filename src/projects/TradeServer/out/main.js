"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRepository = exports.RunService = void 0;
const Domain_1 = require("./domain/Domain");
const getConnection_1 = require("./getConnection");
async function RunService() {
    console.log(25 / 0);
    var userRepository = new UserRepository(await getConnection_1.getConnection());
    console.log(await userRepository.getAll());
    // while (true) {
    //     let someUser = new User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    //     await userRepository.create(someUser);
    // }
}
exports.RunService = RunService;
class UserRepository {
    constructor(connection) {
        this.repository = connection.getRepository(Domain_1.User);
    }
    async create(user) {
        return this.repository.save(user);
        ;
    }
    async getAll() {
        return await this.repository.find();
    }
}
exports.UserRepository = UserRepository;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUNBLDRDQUF1QztBQUN2QyxtREFBZ0Q7QUFFekMsS0FBSyxVQUFVLFVBQVU7SUFFNUIsT0FBTyxDQUFDLEdBQUcsQ0FBQyxFQUFFLEdBQUcsQ0FBQyxDQUFDLENBQUM7SUFHcEIsSUFBSSxjQUFjLEdBQUcsSUFBSSxjQUFjLENBQUMsTUFBTSw2QkFBYSxFQUFFLENBQUMsQ0FBQztJQUUvRCxPQUFPLENBQUMsR0FBRyxDQUFDLE1BQU0sY0FBYyxDQUFDLE1BQU0sRUFBRSxDQUFDLENBQUM7SUFDM0MsaUJBQWlCO0lBQ2pCLDRGQUE0RjtJQUM1Riw2Q0FBNkM7SUFDN0MsSUFBSTtBQUVSLENBQUM7QUFiRCxnQ0FhQztBQUdELE1BQWEsY0FBYztJQUV2QixZQUFZLFVBQXNCO1FBQzlCLElBQUksQ0FBQyxVQUFVLEdBQUcsVUFBVSxDQUFDLGFBQWEsQ0FBQyxhQUFJLENBQUMsQ0FBQztJQUNyRCxDQUFDO0lBQ0QsS0FBSyxDQUFDLE1BQU0sQ0FBQyxJQUFVO1FBQ25CLE9BQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDLENBQUM7UUFBQSxDQUFDO0lBQ3ZDLENBQUM7SUFFRCxLQUFLLENBQUMsTUFBTTtRQUNSLE9BQU8sTUFBTSxJQUFJLENBQUMsVUFBVSxDQUFDLElBQUksRUFBRSxDQUFDO0lBQ3hDLENBQUM7Q0FDSjtBQVpELHdDQVlDIn0=