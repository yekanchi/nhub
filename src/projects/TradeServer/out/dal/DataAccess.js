"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.UserRepository = exports.DumpRepository = exports.getDumpCodalDbConnection = exports.getBinaCodalDbConnection = void 0;
const typeorm_1 = require("typeorm");
const Domain_1 = require("../domain/Domain");
exports.getBinaCodalDbConnection = async () => {
    return await typeorm_1.createConnection({
        type: "sqlite",
        database: "../../databases/BinaCodal.db",
        logging: false,
        synchronize: true,
        entities: [Domain_1.User]
    });
};
exports.getDumpCodalDbConnection = async () => {
    return await typeorm_1.createConnection({
        type: "sqlite",
        database: "../../databases/",
        logging: false,
        synchronize: true,
        entities: [Domain_1.Letter]
    });
};
class DumpRepository {
    constructor() {
        let connection;
        exports.getDumpCodalDbConnection().then(res => {
            connection = res;
        });
        this.repository = connection.getRepository(Domain_1.Letter);
    }
    async create(letter) {
        return await this.repository.save(letter);
    }
    async createArray(letters) {
        let generatedResponse = [];
        for (let letter of letters) {
            try {
                let insertResponse = await this.create(letter);
                generatedResponse.push(insertResponse);
            }
            catch (error) {
                //todo: m.talebi: log using global logger;
                console.log('insert error: ' + error);
            }
        }
        return generatedResponse;
    }
    async getAll() {
        return await this.repository.find();
    }
}
exports.DumpRepository = DumpRepository;
class UserRepository {
    constructor(connection) {
        this.repository = connection.getRepository(Domain_1.User);
    }
    async create(user) {
        return this.repository.save(user);
    }
    async getAll() {
        return await this.repository.find();
    }
}
exports.UserRepository = UserRepository;
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YUFjY2Vzcy5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uLy4uL3NyYy9kYWwvRGF0YUFjY2Vzcy50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxxQ0FBaUU7QUFDakUsNkNBQThDO0FBR2pDLFFBQUEsd0JBQXdCLEdBQUcsS0FBSyxJQUFJLEVBQUU7SUFDL0MsT0FBTyxNQUFNLDBCQUFnQixDQUFDO1FBQzFCLElBQUksRUFBRSxRQUFRO1FBQ2QsUUFBUSxFQUFFLDhCQUE4QjtRQUN4QyxPQUFPLEVBQUUsS0FBSztRQUNkLFdBQVcsRUFBRSxJQUFJO1FBQ2pCLFFBQVEsRUFBRSxDQUFDLGFBQUksQ0FBQztLQUNuQixDQUFDLENBQUM7QUFDUCxDQUFDLENBQUE7QUFFWSxRQUFBLHdCQUF3QixHQUFHLEtBQUssSUFBSSxFQUFFO0lBQy9DLE9BQU8sTUFBTSwwQkFBZ0IsQ0FBQztRQUMxQixJQUFJLEVBQUUsUUFBUTtRQUNkLFFBQVEsRUFBRSxrQkFBa0I7UUFDNUIsT0FBTyxFQUFFLEtBQUs7UUFDZCxXQUFXLEVBQUUsSUFBSTtRQUNqQixRQUFRLEVBQUUsQ0FBQyxlQUFNLENBQUM7S0FDckIsQ0FBQyxDQUFDO0FBQ1AsQ0FBQyxDQUFBO0FBRUQsTUFBYSxjQUFjO0lBR3ZCO1FBQ0ksSUFBSSxVQUFVLENBQUM7UUFDZixnQ0FBd0IsRUFBRSxDQUFDLElBQUksQ0FBQyxHQUFHLENBQUMsRUFBRTtZQUM5QixVQUFVLEdBQUcsR0FBRyxDQUFDO1FBQ3JCLENBQUMsQ0FDSixDQUFBO1FBRUQsSUFBSSxDQUFDLFVBQVUsR0FBRyxVQUFVLENBQUMsYUFBYSxDQUFDLGVBQU0sQ0FBQyxDQUFDO0lBQ3ZELENBQUM7SUFFRCxLQUFLLENBQUMsTUFBTSxDQUFDLE1BQWM7UUFDdkIsT0FBTyxNQUFNLElBQUksQ0FBQyxVQUFVLENBQUMsSUFBSSxDQUFDLE1BQU0sQ0FBQyxDQUFDO0lBQzlDLENBQUM7SUFFRCxLQUFLLENBQUMsV0FBVyxDQUFDLE9BQXNCO1FBQ3BDLElBQUksaUJBQWlCLEdBQUcsRUFBRSxDQUFDO1FBQzNCLEtBQUssSUFBSSxNQUFNLElBQUksT0FBTyxFQUFFO1lBQ3hCLElBQUk7Z0JBQ0EsSUFBSSxjQUFjLEdBQUcsTUFBTSxJQUFJLENBQUMsTUFBTSxDQUFDLE1BQU0sQ0FBQyxDQUFBO2dCQUM5QyxpQkFBaUIsQ0FBQyxJQUFJLENBQUMsY0FBYyxDQUFDLENBQUE7YUFDekM7WUFBQyxPQUFPLEtBQUssRUFBRTtnQkFDWiwwQ0FBMEM7Z0JBQzFDLE9BQU8sQ0FBQyxHQUFHLENBQUMsZ0JBQWdCLEdBQUcsS0FBSyxDQUFDLENBQUM7YUFDekM7U0FDSjtRQUNELE9BQU8saUJBQWlCLENBQUM7SUFDN0IsQ0FBQztJQUVELEtBQUssQ0FBQyxNQUFNO1FBQ1IsT0FBTyxNQUFNLElBQUksQ0FBQyxVQUFVLENBQUMsSUFBSSxFQUFFLENBQUM7SUFDeEMsQ0FBQztDQUNKO0FBbENELHdDQWtDQztBQUVELE1BQWEsY0FBYztJQUd2QixZQUFZLFVBQXNCO1FBQzlCLElBQUksQ0FBQyxVQUFVLEdBQUcsVUFBVSxDQUFDLGFBQWEsQ0FBQyxhQUFJLENBQUMsQ0FBQztJQUNyRCxDQUFDO0lBRUQsS0FBSyxDQUFDLE1BQU0sQ0FBQyxJQUFVO1FBQ25CLE9BQU8sSUFBSSxDQUFDLFVBQVUsQ0FBQyxJQUFJLENBQUMsSUFBSSxDQUFDLENBQUM7SUFDdEMsQ0FBQztJQUVELEtBQUssQ0FBQyxNQUFNO1FBQ1IsT0FBTyxNQUFNLElBQUksQ0FBQyxVQUFVLENBQUMsSUFBSSxFQUFFLENBQUM7SUFDeEMsQ0FBQztDQUNKO0FBZEQsd0NBY0MifQ==