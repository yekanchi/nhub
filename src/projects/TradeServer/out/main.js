"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const DataService_1 = require("./Service/DataService");
const typeorm_1 = require("typeorm");
const Domain_1 = require("./domain/Domain");
async function RunService() {
    var con = await DataService_1.createDbConnection();
    console.log(con);
    var someuser = new Domain_1.User();
    someuser.name = "some user";
    const entityManager = typeorm_1.getManager(); // you can also get it via getConnection().manager
    const user = await entityManager.create(Domain_1.User, someuser);
    entityManager.save(someuser);
}
exports.RunService = RunService;
//# sourceMappingURL=main.js.map