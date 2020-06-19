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
        database: "E:\\NHUB\\src\\projects\\TradeServer\\databases\\dumpCodal.db",
        logging: false,
        entities: [Domain_1.Letter]
    });
};
class DumpRepository {
    constructor() {
    }
    async insert(letter) {
        let connection = await exports.getDumpCodalDbConnection();
        this.repository = connection.getRepository(Domain_1.Letter);
        let result = await this.repository.save(letter);
        return result;
    }
    async createArray(letters) {
        let generatedResponse = [];
        for (let letter of letters) {
            try {
                let insertResponse = await this.insert(letter);
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
//# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiRGF0YUFjY2Vzcy5qcyIsInNvdXJjZVJvb3QiOiIiLCJzb3VyY2VzIjpbIi4uLy4uL3NyYy9kYWwvRGF0YUFjY2Vzcy50cyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiOzs7QUFBQSxxQ0FBaUU7QUFDakUsNkNBQThDO0FBR2pDLFFBQUEsd0JBQXdCLEdBQUcsS0FBSyxJQUFJLEVBQUU7SUFDL0MsT0FBTyxNQUFNLDBCQUFnQixDQUFDO1FBQzFCLElBQUksRUFBRSxRQUFRO1FBQ2QsUUFBUSxFQUFFLDhCQUE4QjtRQUN4QyxPQUFPLEVBQUUsS0FBSztRQUNkLFdBQVcsRUFBRSxJQUFJO1FBQ2pCLFFBQVEsRUFBRSxDQUFDLGFBQUksQ0FBQztLQUNuQixDQUFDLENBQUM7QUFDUCxDQUFDLENBQUE7QUFFWSxRQUFBLHdCQUF3QixHQUFHLEtBQUssSUFBSSxFQUFFO0lBQy9DLE9BQU8sTUFBTSwwQkFBZ0IsQ0FBQztRQUMxQixJQUFJLEVBQUUsUUFBUTtRQUNkLFFBQVEsRUFBRSwrREFBK0Q7UUFDekUsT0FBTyxFQUFFLEtBQUs7UUFDZCxRQUFRLEVBQUUsQ0FBQyxlQUFNLENBQUM7S0FDckIsQ0FBQyxDQUFDO0FBQ1AsQ0FBQyxDQUFBO0FBRUQsTUFBYSxjQUFjO0lBR3ZCO0lBQ0EsQ0FBQztJQUVELEtBQUssQ0FBQyxNQUFNLENBQUMsTUFBYztRQUN2QixJQUFJLFVBQVUsR0FBRyxNQUFNLGdDQUF3QixFQUFFLENBQUM7UUFDbEQsSUFBSSxDQUFDLFVBQVUsR0FBRyxVQUFVLENBQUMsYUFBYSxDQUFDLGVBQU0sQ0FBQyxDQUFDO1FBQ25ELElBQUksTUFBTSxHQUFHLE1BQU0sSUFBSSxDQUFDLFVBQVUsQ0FBQyxJQUFJLENBQVMsTUFBTSxDQUFDLENBQUM7UUFDeEQsT0FBTyxNQUFNLENBQUM7SUFDbEIsQ0FBQztJQUVELEtBQUssQ0FBQyxXQUFXLENBQUMsT0FBc0I7UUFHcEMsSUFBSSxpQkFBaUIsR0FBRyxFQUFFLENBQUM7UUFDM0IsS0FBSyxJQUFJLE1BQU0sSUFBSSxPQUFPLEVBQUU7WUFDeEIsSUFBSTtnQkFDQSxJQUFJLGNBQWMsR0FBRyxNQUFNLElBQUksQ0FBQyxNQUFNLENBQUMsTUFBTSxDQUFDLENBQUE7Z0JBQzlDLGlCQUFpQixDQUFDLElBQUksQ0FBQyxjQUFjLENBQUMsQ0FBQTthQUN6QztZQUFDLE9BQU8sS0FBSyxFQUFFO2dCQUNaLDBDQUEwQztnQkFDMUMsT0FBTyxDQUFDLEdBQUcsQ0FBQyxnQkFBZ0IsR0FBRyxLQUFLLENBQUMsQ0FBQzthQUN6QztTQUNKO1FBQ0QsT0FBTyxpQkFBaUIsQ0FBQztJQUM3QixDQUFDO0lBRUQsS0FBSyxDQUFDLE1BQU07UUFDUixPQUFPLE1BQU0sSUFBSSxDQUFDLFVBQVUsQ0FBQyxJQUFJLEVBQUUsQ0FBQztJQUN4QyxDQUFDO0NBQ0o7QUFoQ0Qsd0NBZ0NDO0FBRUQsTUFBYSxjQUFjO0lBR3ZCLFlBQVksVUFBc0I7UUFDOUIsSUFBSSxDQUFDLFVBQVUsR0FBRyxVQUFVLENBQUMsYUFBYSxDQUFDLGFBQUksQ0FBQyxDQUFDO0lBQ3JELENBQUM7SUFFRCxLQUFLLENBQUMsTUFBTSxDQUFDLElBQVU7UUFDbkIsT0FBTyxJQUFJLENBQUMsVUFBVSxDQUFDLElBQUksQ0FBQyxJQUFJLENBQUMsQ0FBQztJQUN0QyxDQUFDO0lBRUQsS0FBSyxDQUFDLE1BQU07UUFDUixPQUFPLE1BQU0sSUFBSSxDQUFDLFVBQVUsQ0FBQyxJQUFJLEVBQUUsQ0FBQztJQUN4QyxDQUFDO0NBQ0o7QUFkRCx3Q0FjQyJ9