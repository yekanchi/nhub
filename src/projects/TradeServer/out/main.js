"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.RunService = void 0;
const getConnection_1 = require("./getConnection");
const DataAccess_1 = require("./dal/DataAccess");
async function RunService() {
    let userRepository = new DataAccess_1.UserRepository(await getConnection_1.getConnection());
    console.log(await userRepository.getAll());
    // while (true) {
    //     let someUser = new User({ userName: "m.talebi", name: "morteza", family: "talebi" });
    //     await userRepository.create(someUser);
    // }
}
exports.RunService = RunService;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibWFpbi5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uL3NyYy9tYWluLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7OztBQUVBLG1EQUE4QztBQUM5QyxpREFBZ0Q7QUFJekMsS0FBSyxVQUFVLFVBQVU7SUFDNUIsSUFBSSxjQUFjLEdBQUcsSUFBSSwyQkFBYyxDQUFDLE1BQU0sNkJBQWEsRUFBRSxDQUFDLENBQUM7SUFDL0QsT0FBTyxDQUFDLEdBQUcsQ0FBQyxNQUFNLGNBQWMsQ0FBQyxNQUFNLEVBQUUsQ0FBQyxDQUFDO0lBQzNDLGlCQUFpQjtJQUNqQiw0RkFBNEY7SUFDNUYsNkNBQTZDO0lBQzdDLElBQUk7QUFFUixDQUFDO0FBUkQsZ0NBUUMifQ==